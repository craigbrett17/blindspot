using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using Blindspot.Core;
using System.Windows.Forms;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;

namespace Blindspot.Helpers
{
    /// <summary>
    /// Handles our auto-update process
    /// </summary>
    public class UpdateManager
    {
        public event EventHandler UpdateDownloaded, UpdateFailed, NewVersionDetected;
        public Version NewVersion { get; set; }
        private UpdateResponse _response;
        private UpdateResponseVersion _preferredResponse;
        private UserSettings.UpdateType _updateType;
        private string _downloadDestination;
        private string _installerFileName;
        private static UpdateManager _instance;
        public static UpdateManager Instance
        {
            get { return _instance ?? new UpdateManager(); }
            set { _instance = value; }
        }

        private UpdateManager()
        { }

        public void CheckForNewVersionAsync()
        {
            var task = Task.Factory.StartNew(() =>
            {
                CheckForNewVersion();
            });
        }

        public void CheckForNewVersion()
        {
            var settings = UserSettings.Instance;
            _updateType = settings.UpdatesInterestedIn;
            GetUpdateResponseFromServer();
            if (_response != null)
            {
                if (NewerVersionIsAvailable())
                {
                    if (NewVersionDetected != null) NewVersionDetected(NewVersion, EventArgs.Empty);
                }
            }
            else
            {
                Logger.WriteTrace("Update response from server is null. May not be castable, may be jibberish");
            }
        }

        private void GetUpdateResponseFromServer()
        {
            using (var client = new WebClient())
            {
                var jsonData = String.Empty;
                // attempt to download JSON data as a string
                try
                {
                    jsonData = client.DownloadString(_updateUrl);
                }
                catch (Exception ex)
                {
                    Logger.WriteTrace("Exception checking for new update. {0}: {1}", ex.GetType().ToString(), ex.Message);
                }
                _response = !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<UpdateResponse>(jsonData) : null;
            }
        }

        private UpdateResponseVersion GetRelevantUpdateResponseVersionForUser()
        {
            switch (_updateType)
            {
                case UserSettings.UpdateType.Stable:
                    return _response.stable;
                case UserSettings.UpdateType.Beta:
                    return _response.beta;
                case UserSettings.UpdateType.Dev:
                    return _response.dev;
                default:
                    return null;
            }
        }

        private bool NewerVersionIsAvailable()
        {
            var currentVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            _preferredResponse = GetRelevantUpdateResponseVersionForUser();
            NewVersion = new Version(_preferredResponse.version);
            return currentVersion < NewVersion;
        }
        
        private string _updateUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateVersionLocation"];
            }
        }

        private string _updateFileRoot
        {
            get
            {
                return ConfigurationManager.AppSettings["UpdateFileLocation"];
            }
        }

        public void DownloadNewVersionAsync()
        {
            var task = Task.Factory.StartNew(() =>
            {
                DownloadNewVersion();
            });
        }

        public void DownloadNewVersion()
        {
            try
            {
                _installerFileName = _preferredResponse.filename;
                // we're storing this file in temp
                _downloadDestination = Path.Combine(Path.GetTempPath(), _installerFileName);
                bool isGZip = _installerFileName.EndsWith(".gz");
                // .gz turned out to be more .NET friendly than .zip pre v4.5...
                // may consider changing to CSharpZipLib at a later date

                DownloadInstallerFromServer();
                if (isGZip)
                    ExtractInstallerFromArchive();

                if (this.UpdateDownloaded != null) UpdateDownloaded(this, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                if (UpdateFailed != null) UpdateFailed(ex, EventArgs.Empty);
            }
        }
        
        private void DownloadInstallerFromServer()
        {
            var url = _updateFileRoot + _installerFileName;
            using (var client = new WebClient())
            {
                client.DownloadFile(url, _downloadDestination);
            }
        }

        private void ExtractInstallerFromArchive()
        {
            string destinationPath = _downloadDestination.Substring(0, _downloadDestination.Length - 3);
            using (FileStream gzFile = new FileStream(_downloadDestination, FileMode.Open))
            {
                using (GZipStream gZipStream = new GZipStream(gzFile, CompressionMode.Decompress))
                {
                    using (FileStream extractionStream = new FileStream(destinationPath, FileMode.Create, FileAccess.Write))
                    {
                        byte[] tempBytes = new byte[4096];
                        int i;
                        while ((i = gZipStream.Read(tempBytes, 0, tempBytes.Length)) != 0)
                        {
                            extractionStream.Write(tempBytes, 0, i);
                        }
                    }
                }
            }
            _downloadDestination = destinationPath; // from now on we care about the extracted file
        }

        public void RunInstaller()
        {
            ProcessStartInfo processInfo = new ProcessStartInfo();
            processInfo.Verb = "runas";
            processInfo.UseShellExecute = true;
            processInfo.FileName = _downloadDestination;
            processInfo.Arguments = "/S";
            processInfo.WindowStyle = ProcessWindowStyle.Normal;
            processInfo.CreateNoWindow = true;

            Process process = new Process();
            process.StartInfo = processInfo;
            process.Start();
        }

        public class UpdateResponse
        {
            public UpdateResponseVersion stable { get; set; }
            public UpdateResponseVersion beta { get; set; }
            public UpdateResponseVersion dev { get; set; }
        }

        public class UpdateResponseVersion
        {
            public string version { get; set; }
            public string filename { get; set; }
        }
    }
}