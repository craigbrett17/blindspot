# What's new in Blindspot

This page shows all published versions of Blindspot with their change logs.

## Blindspot v2.2 (Sep 2015)

### New in this release
* Playlist management: Add and remove tracks from existing playlists, or to a new playlist altogether. Accessible through the Blindspot context menu.
* Added a shuffle mode, activated with alt+shift+h by default, or through the Blindspot context menu. When active and you pick a track from an album or playlist, all other tracks in the album or playlist will be inserted into your play queue in a random order. 
* Ability to choose which device Blindspot uses for playback. Selectable under the options dialog.
* A skip unplayable tracks option, which will automatically skip over tracks that can't be played for whatever reason (akin to the behaviour in the main Spotify desktop client). Turned on by default, but can be toggled in the options dialog. 
* Added an add next in queue command (alt+shift+x by default), to add the currently selected track to play next.
* Much more simplified playback code. Please let us know of any issues arising.
* Created a what's new page and added a start menu item to take you to it.

### Bug fixes in this release
* Fixed a bug where Blindspot would not launch if the user launching the application wasn't the same as the one who installed it. Important for scenarios where people may have a separate administrator user for handling installs.
* Fixed an issue with empty buffers and the home/end commands crashing the app

## Blindspot v2.1 (Nov 2014)

### New in this version:
* Search by album: Select album from the search dialog and run a search to get a list of albums that meet your search criteria. Open an album to get access to the tracks inside and play them just like a playlist.
* Search by artist: Select artist from the search dialog and run a search to get artists that meet your search criteria. Go into the artist to see all their albums and albums featuring their tracks, and open and play these albums as normal.
* Added hotkeys and getting started guide to the context menu, under the new help submenu. The about option also appears under here.

### Bug fixes
* Fixed an important and elusive CPU performance issue with playback. This should give Blindspot a noticable smaller footprint, which is good for you and your processor. Thanks to everyone who reported this issue. 
* Various code tidy ups and work behind the scenes. More of benefit to those considering contributing to the project, as this should make things less confusing.

## Blindspot v2.0 (Jul 2014)

### New in this version:
* Visual popups as an optional alternative to screen reader output. Customisable in the options dialog. Also has a customisable length of time to be displayed.
* Contiguous playback: You can now pick a track in one of your playlists and Blindspot will go through and play the playlist for you from that track. You may also queue up new tracks to add on to your "play queue" with a new command (alt+shift+q by default)
* View the details of a track (alt+shift+v by default)
* Able to move forward and backward through your play queue (ctrl+alt+shift+left or right arrows by default)
* Support for media keys as shortcuts. You may play/pause and go to previous or next tracks with their corresponding media keys.
* A few options to adjust the behaviour of the screen reader, such as reading out track changes or whether or not to use SAPI as a fallback if there is no screen reader
* A task tray icon with a menu, currently leads to a few pre-existing features. This will have context sensative menu items added later on. The menu is also accessible with a keyboard shortcut (alt+shift+m by default)
* Automatic updates, configurable in the options dialog. You may either subscribe to stable or beta updates.
* A few new start menu shortcuts to take you to the FAQ, hotkeys list and the Blindspot App Data Directory.

### Bug fixes: 
* Blindspot is now removable from the add or remove programs dialog. 
* Fixed security problem with the usersettings.dat file. Its now an XML file and we're being more cautious because of that. Also makes human editing a possibility in case of problems. 
* Fixed weird bug with closing options after not tsetting keyboard settings.

## Blindspot v1.1 (Nov 2013)

### New features in this version:
* Languages 
	* English
	* French
	* German
	* Spanish
	* Swedish
* Installation transslations
* A dialog appearing on first launch to allow the user to select their language and default hotkey setup
* Paging on search buffers. Scroll to the bottom of a search buffer and hit the next buffer itemm hotkey to get the next batch of results. 
* Options dialog to change language, open hotkeys file and in future other settings.
* A new keyboard layout, named 'Modern'

### Bug fixes
* Fix for issue #6 where resuming playback would cause a torrant of errors for no apparent reason. If anyone notices a recurrance of this bug, please let us know.
* Fixed a small undocumented issue where playback would temporarily stop while a search was being run.
* Fix for issue #7 where it was possible to launch 2 instances of the program, causing an ugly crash. This should no longer be possible or necessary.
* Fix for issue #8 where the program would be less than helpful if we're unable to retrieve a user's playlists. Now, we give the user a chance to retry.

## Blindspot v1.0 (Aug 2013)

### New in this version
* Created the buffered 'Qube' style GUI
* Created settings storage and hotkeys management
* Log into spotify
* Display user's playlists
* Display playlist tracks
* Play selected tracks
* Search for tracks
* Created installer