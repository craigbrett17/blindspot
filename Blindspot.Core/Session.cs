﻿/*-
 * Copyright (c) 2012 Software Development Solutions, Inc.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions
 * are met:
 * 1. Redistributions of source code must retain the above copyright
 *    notice, this list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright
 *    notice, this list of conditions and the following disclaimer in the
 *    documentation and/or other materials provided with the distribution.
 *
 * THIS SOFTWARE IS PROVIDED BY THE AUTHOR AND CONTRIBUTORS ``AS IS'' AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED.  IN NO EVENT SHALL THE AUTHOR OR CONTRIBUTORS BE LIABLE
 * FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
 * DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS
 * OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT
 * LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY
 * OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF
 * SUCH DAMAGE.
 */

using System;
using System.IO;
using System.Runtime.InteropServices;
using libspotifydotnet;
using Blindspot.Core.Models;

// namespace Jamcast.Plugins.Spotify.API // original API
namespace Blindspot.Core
{
    public static class Session
    {
        private static IntPtr _sessionPtr;
        
        private delegate void connection_error_delegate(IntPtr sessionPtr, libspotify.sp_error error);
        private delegate void end_of_track_delegate(IntPtr sessionPtr);
        private delegate void get_audio_buffer_stats_delegate(IntPtr sessionPtr, IntPtr statsPtr);
        private delegate void log_message_delegate(IntPtr sessionPtr, string message);
        private delegate void logged_in_delegate(IntPtr sessionPtr, libspotify.sp_error error);
        private delegate void logged_out_delegate(IntPtr sessionPtr);
        private delegate void message_to_user_delegate(IntPtr sessionPtr, string message);
        private delegate void metadata_updated_delegate(IntPtr sessionPtr);
        private delegate int music_delivery_delegate(IntPtr sessionPtr, IntPtr formatPtr, IntPtr framesPtr, int num_frames);        
        private delegate void notify_main_thread_delegate(IntPtr sessionPtr);
        private delegate void offline_status_updated_delegate(IntPtr sessionPtr);
        private delegate void play_token_lost_delegate(IntPtr sessionPtr);
        private delegate void start_playback_delegate(IntPtr sessionPtr);
        private delegate void stop_playback_delegate(IntPtr sessionPtr);
        private delegate void streaming_error_delegate(IntPtr sessionPtr, libspotify.sp_error error);
        private delegate void userinfo_updated_delegate(IntPtr sessionPtr);

        private static connection_error_delegate fn_connection_error_delegate = new connection_error_delegate(connection_error);
        private static end_of_track_delegate fn_end_of_track_delegate = new end_of_track_delegate(end_of_track);
        private static get_audio_buffer_stats_delegate fn_get_audio_buffer_stats_delegate = new get_audio_buffer_stats_delegate(get_audio_buffer_stats);
        private static log_message_delegate fn_log_message = new log_message_delegate(log_message);
        private static logged_in_delegate fn_logged_in_delegate = new logged_in_delegate(logged_in);
        private static logged_out_delegate fn_logged_out_delegate = new logged_out_delegate(logged_out);
        private static message_to_user_delegate fn_message_to_user_delegate = new message_to_user_delegate(message_to_user);
        private static metadata_updated_delegate fn_metadata_updated_delegate = new metadata_updated_delegate(metadata_updated);
        private static music_delivery_delegate fn_music_delivery_delegate = new music_delivery_delegate(music_delivery);
        private static notify_main_thread_delegate fn_notify_main_thread_delegate = new notify_main_thread_delegate(notify_main_thread);
        private static offline_status_updated_delegate fn_offline_status_updated_delegate = new offline_status_updated_delegate(offline_status_updated);
        private static play_token_lost_delegate fn_play_token_lost_delegate = new play_token_lost_delegate(play_token_lost);
        private static start_playback_delegate fn_start_playback = new start_playback_delegate(start_playback);
        private static stop_playback_delegate fn_stop_playback = new stop_playback_delegate(stop_playback);
        private static streaming_error_delegate fn_streaming_error_delegate = new streaming_error_delegate(streaming_error);
        private static userinfo_updated_delegate fn_userinfo_updated_delegate = new userinfo_updated_delegate(userinfo_updated);

        private static byte[] appkey = null;
        private static libspotify.sp_error _loginError = libspotify.sp_error.OK;
        private static bool _isLoggedIn = false;

        public static event Action<IntPtr> OnNotifyMainThread;        
        public static event Action<IntPtr> OnLoggedIn;
        public static event Action<byte[]> OnAudioDataArrived;
        public static event Action<object> OnAudioStreamComplete;

        public static IntPtr GetSessionPtr()
        {
            return _sessionPtr;
        }

        public static libspotify.sp_error LoginError
        {
            get { return _loginError; }
        }

        public static bool IsLoggedIn
        {
            get { return _isLoggedIn; }
        }
        
        public static void Login(object[] args)
        {
            // make the error back to normal
            _loginError = libspotify.sp_error.OK;
            Session.appkey = (byte[])args[0];
            if (_sessionPtr == IntPtr.Zero)
                _loginError = initSession();

            if (_loginError != libspotify.sp_error.OK)
                throw new ApplicationException(libspotify.sp_error_message(_loginError));

            if (_sessionPtr == IntPtr.Zero)
                throw new InvalidOperationException("Session initialization failed, session pointer is null.");

            libspotify.sp_session_login(_sessionPtr, args[1].ToString(), args[2].ToString(), false, null);
        }

        public static void Logout()
        {
            if (_sessionPtr == IntPtr.Zero)
                return;
            libspotify.sp_session_logout(_sessionPtr);
            _sessionPtr = IntPtr.Zero;
        }

        public static int GetUserCountry()
        {
            if (_sessionPtr == IntPtr.Zero)
                throw new InvalidOperationException("No session.");
            return libspotify.sp_session_user_country(_sessionPtr);            
        }
        
        public static SpotifyError LoadPlayer(IntPtr trackPtr)
        {
            return  (SpotifyError)libspotify.sp_session_player_load(_sessionPtr, trackPtr);
        }

        public static void Play()
        {
            libspotify.sp_session_player_play(_sessionPtr, true);
        }

        public static void Pause()
        {
            libspotify.sp_session_player_play(_sessionPtr, false);
        }

        public static void UnloadPlayer()
        {
            libspotify.sp_session_player_unload(_sessionPtr);
        }

        private static libspotify.sp_error initSession()
        {
            libspotify.sp_session_callbacks callbacks = new libspotify.sp_session_callbacks();
            callbacks.connection_error = Marshal.GetFunctionPointerForDelegate(fn_connection_error_delegate);
            callbacks.end_of_track = Marshal.GetFunctionPointerForDelegate(fn_end_of_track_delegate);
            callbacks.get_audio_buffer_stats = Marshal.GetFunctionPointerForDelegate(fn_get_audio_buffer_stats_delegate);
            callbacks.log_message = Marshal.GetFunctionPointerForDelegate(fn_log_message);        
            callbacks.logged_in = Marshal.GetFunctionPointerForDelegate(fn_logged_in_delegate);
            callbacks.logged_out = Marshal.GetFunctionPointerForDelegate(fn_logged_out_delegate);
            callbacks.message_to_user = Marshal.GetFunctionPointerForDelegate(fn_message_to_user_delegate);
            callbacks.metadata_updated = Marshal.GetFunctionPointerForDelegate(fn_metadata_updated_delegate);
            callbacks.music_delivery = Marshal.GetFunctionPointerForDelegate(fn_music_delivery_delegate);
            callbacks.notify_main_thread = Marshal.GetFunctionPointerForDelegate(fn_notify_main_thread_delegate);
            callbacks.offline_status_updated = Marshal.GetFunctionPointerForDelegate(fn_offline_status_updated_delegate);
            callbacks.play_token_lost = Marshal.GetFunctionPointerForDelegate(fn_play_token_lost_delegate);
            callbacks.start_playback = Marshal.GetFunctionPointerForDelegate(fn_start_playback);
            callbacks.stop_playback = Marshal.GetFunctionPointerForDelegate(fn_stop_playback);
            callbacks.streaming_error = Marshal.GetFunctionPointerForDelegate(fn_streaming_error_delegate);
            callbacks.userinfo_updated = Marshal.GetFunctionPointerForDelegate(fn_userinfo_updated_delegate);

            IntPtr callbacksPtr = Marshal.AllocHGlobal(Marshal.SizeOf(callbacks));
            Marshal.StructureToPtr(callbacks, callbacksPtr, true);

            libspotify.sp_session_config config = new libspotify.sp_session_config();
            config.api_version = libspotify.SPOTIFY_API_VERSION;
            config.user_agent = "BlindSpot";
            config.application_key_size = appkey.Length;
            config.application_key = Marshal.AllocHGlobal(appkey.Length);
            config.cache_location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Blindspot\\Libspotify");
            config.settings_location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Blindspot\\Libspotify");
            config.callbacks = callbacksPtr;
            config.compress_playlists = true;
            config.dont_save_metadata_for_playlists = false;
            config.initially_unload_playlists = false;

            Logger.WriteDebug("api_version={0}", config.api_version);
            Logger.WriteDebug("application_key_size={0}", config.application_key_size);
            Logger.WriteDebug("cache_location={0}", config.cache_location);
            Logger.WriteDebug("settings_location={0}", config.settings_location);
            
            Marshal.Copy(appkey, 0, config.application_key, appkey.Length);

            IntPtr sessionPtr;
            libspotify.sp_error err = libspotify.sp_session_create(ref config, out sessionPtr);

            if (err == libspotify.sp_error.OK)
            {
                _sessionPtr = sessionPtr;
                libspotify.sp_session_set_connection_type(sessionPtr, libspotify.sp_connection_type.SP_CONNECTION_TYPE_WIRED);
            }

            return err;
        }

        private static void connection_error(IntPtr sessionPtr, libspotify.sp_error error)
        {
            Logger.WriteDebug("Connection error: {0}", libspotify.sp_error_message(error));
        }

        private static void end_of_track(IntPtr sessionPtr)
        {
            if (Session.OnAudioStreamComplete != null)
                Session.OnAudioStreamComplete(null);
        }

        private static void get_audio_buffer_stats(IntPtr sessionPtr, IntPtr statsPtr)
        {

        }

        private static void log_message(IntPtr sessionPtr, string message)
        {
            if (message.EndsWith("\n"))
                message = message.Substring(0, message.Length - 1);
            Logger.WriteDebug("libspotify> " + message);
        }

        private static void logged_in(IntPtr sessionPtr, libspotify.sp_error error)
        {
            if (error == libspotify.sp_error.OK)
            {
                _isLoggedIn = true;
            } 
            _loginError = error;
            if (Session.OnLoggedIn != null)
                Session.OnLoggedIn(sessionPtr);
        }

        private static void logged_out(IntPtr sessionPtr)
        {
            _isLoggedIn = false;
        }

        private static void message_to_user(IntPtr sessionPtr, string message)
        {
            Logger.WriteDebug("libspotify> {0}", message);
        }

        private static void metadata_updated(IntPtr sessionPtr)
        {
            // Logger.WriteDebug("libspotify> metadata_updated");
        }

        private static int music_delivery(IntPtr sessionPtr, IntPtr formatPtr, IntPtr framesPtr, int num_frame)
        {
            // API 11 is firing this callback several times after the track ends.  num_frame is set to 22050,
            // which seems meaninful yet is way out of normal range (usually we get a couple hundred frames or less
            // at a time).  The buffers are all zeros, this adds a ton of extra silence to the end of the track for
            // no reason.  Docs don't talk about this new behavior, maybe related to gapless playback??
            // Workaround by ignoring any data received after the end_of_track callback; this ignore is done
            // in SpotifyTrackDataDataPipe.
            if (num_frame == 0)
                return 0;

            libspotify.sp_audioformat format = (libspotify.sp_audioformat)Marshal.PtrToStructure(formatPtr, typeof(libspotify.sp_audioformat));
            byte[] buffer = new byte[num_frame * sizeof(Int16) * format.channels];
            Marshal.Copy(framesPtr, buffer, 0, buffer.Length);

            if (Session.OnAudioDataArrived != null)
                Session.OnAudioDataArrived(buffer);          

            return num_frame;
        }        

        private static void notify_main_thread(IntPtr sessionPtr)
        {
            if (OnNotifyMainThread != null)
                OnNotifyMainThread(_sessionPtr);
        }

        private static void offline_status_updated(IntPtr sessionPtr)
        {
                        
        }

        private static void play_token_lost(IntPtr sessionPtr)
        {
            Logger.WriteDebug("play_token_lost");
        }

        private static void start_playback(IntPtr sessionPtr)
        {
            Logger.WriteDebug("libspotify> start_playback");
        }

        private static void stop_playback(IntPtr sessionPtr)
        {
            Logger.WriteDebug("libspotify> stop_playback");
        }

        private static void streaming_error(IntPtr sessionPtr, libspotify.sp_error error)
        {
            Logger.WriteDebug("Streaming error: {0}", libspotify.sp_error_message(error));
        }

        private static void userinfo_updated(IntPtr sessionPtr)
        {
            // Logger.WriteDebug("libspotify> userinfo_updated");
        }

    }
}