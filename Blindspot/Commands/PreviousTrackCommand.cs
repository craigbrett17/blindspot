﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Blindspot.Core;
using Blindspot.Core.Models;
using Blindspot.Helpers;
using Blindspot.ViewModels;
using ScreenReaderAPIWrapper;

namespace Blindspot.Commands
{
    public class PreviousTrackCommand : HotkeyCommandBase
    {
        private BufferListCollection buffers;
        private PlaybackManager playbackManager;

        public PreviousTrackCommand(BufferListCollection buffersIn, PlaybackManager pbManagerIn)
        {
            buffers = buffersIn;
            playbackManager = pbManagerIn;
        }

        public override string Key
        {
            get { return "previous_track"; }
        }

        public override void Execute(object sender, HandledEventArgs e)
        {
            var playQueue = buffers[0];
            ClearCurrentlyPlayingTrack();

            if (playbackManager.HasPreviousTracks)
            {
                var nextTrack = playbackManager.GetPreviousTrack();
                playQueue.Insert(0, nextTrack);
                PlayNewTrackBufferItem(nextTrack);
                playQueue.CurrentItemIndex = 0;
            }
        }

        private void PlayNewTrackBufferItem(TrackBufferItem item)
        {
            var response = Session.LoadPlayer(item.Model.TrackPtr);
            if (response.IsError)
            {
                var output = OutputManager.Instance;
                output.OutputMessage(StringStore.UnableToPlayTrack + response.Message, false);
                return;
            }
            Session.Play();
            playbackManager.PlayingTrackItem = item;
            playbackManager.fullyDownloaded = false;
            playbackManager.Play();
        }

        private void ClearCurrentlyPlayingTrack()
        {
            if (playbackManager.PlayingTrackItem != null)
            {
                playbackManager.Stop();
                Session.UnloadPlayer();
                playbackManager.PlayingTrackItem = null;
            }
        }

    }
}