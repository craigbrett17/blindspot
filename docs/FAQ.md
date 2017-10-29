---
title: Blindspot Frequently Asked Questions (FAQ)
---

## Frequently Asked Questions

Below are some of the most frequently asked (or anticipated to be some of the most frequently asked) questions about Blindspot. If you have some of your own, please add them to the discussion forum and it may end up here.

### Q: Why do I need a Spotify premium account?

Most unfortunately, use of applications built against the libspotify library (such as this one) require Spotify premium. This is a requirement we can't avoid, no matter how much we'd like to. So far, requests to allow us to bypass this restriction have been denied.

Note: You may get a free month's worth of premium as a trial by going here: [https://www.spotify.com/freetrial/](https://www.spotify.com/freetrial/). Your account will not be charged unless you continue the subscription beyond the trial.

### Q: The app logs me in, but the shortcut keys aren't responding to me. Help!

It seems that different versions of Windows and different graphics cards, etc, will steal hotkeys for their own nefarious purposes. To test if it's this, try different shortcut keys, {"alt+shift+n"} and {"alt+shift+d"} aren't often hijacked, so they should still work, even if moving around doesn't. If it is this, you will need to change your keyboard shortcuts. See the question below for how to do this. If it doesn't seem to be that, this might be a bug (oh no), or else something we haven't considered yet. In either case, let us know!

### Q: How do I change my keyboard shortcuts?

There are a couple of options.

1. Manually edit the hotkeys.txt file

You need to change the hotkeys.txt file in your Blindspot settings folder. The simplest way to do this is to open up the options screen (alt+shift+O on the standard and modern keyboard) and go to Edit Hotkeys. You can change it within reason, but this file is read by the application, so be aware that errors might cause things to behave strangely or stop working altogether. A typical line in this file might look like this:

{"command=control+shift+C"}

Where command is what the application will do. Note that you can also shorten control and windows modifier keys to ctrl and win, respectively. 

2. Copy and paste in another keyboard layout and rename it to hotkeys.txt

This is not yet possible in the app, but is easy to do in Windows/File Explorer. First, browse to your local application data. This may typically be found in your user area (i.e click John Doe in the start menu), and go into AppData, then Local, then go into the Blindspot folder there. Alternatively, go into the run dialog (hold windows and press R), then copy the following: {"%localappdata%\Blindspot"}. Then hit enter and it should take you straight to the right folder.

In the keyboard layouts folder, you will find different keyboard layouts. If you don't want to manually edit the hotkeys file and one of the keyboard layouts here will suffice, you may simply copy it into the Settings folder, delete the pre-existing hotkeys.txt file and rename the file you have just copied to hotkeys.txt. 

Whichever method you pick, once you are done, restart the application and your changes should take effect.

### Q: Oops, things didn't go well when I was trying to change my hotkeys. Now what?

No problem. In the Blindspot application data folder (where the Settings folder is located), you'll also find a Keyboard Layouts folder. This contains text files for different keyboard layouts. Take a look at them if you like, decide which one you want to use and then copy it into the Settings folder. I advise copying as opposed to cutting, as you might want a copy just in case. Once you have copied the file, delete the pre-existing hotkeys.txt file and rename the file you have just copied to hotkeys.txt. Now, restart the application and things should work as normal again. 