Screen Reader API .NET Wrapper
by Craig Brett

1. Legal Jibba Jabba
2. Introduction
3. Using the API wrapper
4. Closing

1. Legal Jibba Jabba

This software is Copyright © Craig Brett 2012. This software is provided as is without warrantee of any kind, expressed or implied. Craig Brett will not be held responsible for any damages or inconveniences caused by the use or misuse of this software. 

You may use this API in any project you so desire, commercial or otherwise, so long as the original Screen Reader API license terms (see below) are met. You may freely redistribute this software, provided that the included readme file (and this legal section of the readme) are included, with no alterations at least to the legal section.

Original Screen Reader API Copyright © 2011-2012, Quentin Cosendey http://quentinc.net/. You can use the screen reader API DLL for free in your own software  as long as it is also distributed for free. If you wish to use it in a commercial product, please contact me for arrangements.

2. Introduction

Hello, and thank you for taking the time to download this API wrapper. This was a project I was originally using for my own netharious purposes, but decided to release into the wild world. 

There are many .NET developers out there and some of them even care about accessibility. For those who do, this API might well help in cases where you would like to communicate directly, either through people's screen readers or to the SAPI engine, built into most modern versions of Windows. Use of this API does not mean that your programs will be accessible, however, that is up to you. This just might give you the tools to help where there is no other easy alternative.

Communicating with screen readers is currently supported in desktop and Windows 8 applications. Since communicating with a screen reader requires some knowledge of the host computer, it will not work as part of a web project.

If you have any questions, please feel free to contact me. My contact details as well as other information and trinkets may be found at my website: http://www.craigbrett.zymichost.com

3. Using the API Wrapper

This guide assumes that you are using Visual Studio. If you are using the command line, please apply relevant alternatives to achieve the same effect.

These steps are needed if you have downloaded a .zip file from my website. If you got hold of the NuGet package, skip over this bit.

1. Bring up the add reference dialog. This can either be found in the Project Menu as Add Reference or by right clicking/pressing the applications key on References or a project in the Solution Explorer. Which ever way is preferable for you. 
2. Go to the browse tab and find wherever you have stored the ScreenReaderAPIWrapper.dll. Select it to import it into your project. 
3. In the solution explorer, create a folder called "Lib"(case sensative). This is where we'll store the original ScreenReaderAPI files the wrapper interacts with. 
4. Right click/applications key on the new "Lib" folder and select add, then existing item. In the dialog that appears, change the files of type setting to all files. Then navigate to the location of the ScreenReaderAPIWrapper and go into the "Lib" folder in there. Select all files inside and hit the add button, or press enter.
5. Finally, back in Solution Explorer, go into the "Lib" folder, select all the files in there. Go to the properties panel (by pressing F4) and check that the "Build Action" is set to content and change the "Copy To Output Directory" to either Always or If Newer.

At the top of a class file which will be using the functionality, above the namespace, put the following using statement. 

using ScreenReaderAPIWrapper;

Then, use the static ScreenReader class to call the various methods of the API.

e.g. 

ScreenReader.SayString("Hello world!");

4. Closing

Thanks for reading this readme file for the ScreenReaderAPIWrapper. I hope you find this work useful in some way or another. I'd be happy to hear about your experiences. Please use the contact details on my website: http://www.craigbrett.zymichost.com

There are still a few improvements I'd like to make to this library, which may come in the future. Check back occasionally to see if it's done. 

In closing, I'd like to thank Quentin Cosendey for making the API in the first place. This would be pretty pointless without your work. 

Finally, I'd like to thank you for reading and using this work. May it help you in your various projects, tasks, experiments or other shinanigans.

Copyright © Craig Brett 2013. All rights reserved, all wrongs reversed.