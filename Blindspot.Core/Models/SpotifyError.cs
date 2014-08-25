using libspotifydotnet;
using System;

namespace Blindspot.Core.Models
{
    public class SpotifyError
    {
        public string ErrorType { get; private set; }
        public string Message { get; private set; }
        public bool IsError { get { return this.ErrorType != "OK"; } }

        public static explicit operator SpotifyError(libspotify.sp_error error)
        {
            return new SpotifyError
            {
                ErrorType = error.ToString(),
                Message = libspotify.sp_error_message(error)
            };
        }
    }
}
