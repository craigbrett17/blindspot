## Contributing code to Blindspot

### Prerequisites

To be able to contribute to the Blindspot project, you will need at least some knowledge of the following:

* [C#](http://en.wikipedia.org/wiki/C_Sharp_(programming_language))
* [Git](http://git-scm.com/)
* [Spotify](http://www.spotify.com)

Knowing the following would also be beneficial:

* Windows Forms or WPF
* Native code interop

You will also (probably) need the following installed:

* [Visual Studio](http://www.visualstudio.com/en-us/products/visual-studio-express-vs.aspx) (2010 or later)
* [Git](http://git-scm.com/downloads)

Also (unfortunately), to run the application, you will need a Spotify premium subscription. See the [FAQ](FAQ) for more info.

### Fork

Firstly, you will need a Github account. If you haven't already, please [register for Github](https://github.com/join) to be able to contribute to the project.

Then, you will need your own copy project (or fork) of the Blindspot project. Whilst signed in, [Click here to fork the Blindspot project](https://github.com/craigbrett17/Blindspot#fork-destination-box). Or if you want to manually go there, go to source code, then fork, then click Create.

Once you've done this, you will be taken to your fork's source code page. And there's all the source code! You'll notice we have a couple of branches already (there may be more depending on what's being worked on). 

* The master branch is the branch covering the currently stable version of the product. Only hotfixes and critical updates should go here. When we update the production version, development will be merged in.
* The development branch is where most people's code changes will actually go. 

You may of course, make remote branches within your fork. This will only be necessary if you're making a tricky fork, possibly with other colaborators. You probably won't need to and development will do just fine. But some people prefer the use of feature branches. I'm fairly laid back on this for now.

Next, you'll need to clone your fork to your local computer. Hit the 'Clone' link and you will be given a command to enter into git to get a local copy. Fire up your command line client of choice and put in / copy and paste the command and give it a directory somewhere. i.e: git clone https://git01.codeplex.com/forks/craigbrett17/testfork "Blindspot Testfork"

You should now have your fork on your local machine. We've got something else to do in the command line. Move to your cloned fork with cd: cd "Blindspot Testfork"

Now, we want to checkout and track the development branch, so you're working with the latest code. Use the following command:

git checkout --track origin/development

Wait a few seconds or so, and voila! You will have the newest code ready to be changed. 

### Do stuff

Now, you'll want to fire up Visual Studio and open Blindspot in there. You should notice 3 projects:

* Blindspot: The main application project. With logic for managing buffers, playback and installs, among other things.
* Blindspot.Core: The models for interacting with LibspotifyDotNet which in turn interacts with Libspotify. This should try to be a facade layer.
* Blindspot.Tests: Unit tests for the Blindspot project. 

Depending on what you're changing, you may need to interact with all three of these. 

Do what you want to do to make your changes, then commit and push your changes to your fork.

#### Playing the strings

If you're familiar with internationalization in Winforms, feel free to skip this section. 

Blindspot supports (at the time of v1.1) 5 languages: English, French, Spanish, German and Swedish. As part of this, we need to make sure we do not use strings themselves in the application if it is either going to be read out or displayed on screen. 

Instead, for dynamic text, we use the StringStore static class. The StringStore (as you probably know) stores strings, in particular there are several versions of the store, 1 for each language, with the one simply named StringStore being the default language of the application (English). When needing an already stored string from the store, simply type StringStore followed by a dot to bring up all available strings.

To add a new string to the store, you will need to go to each of the StringStore files and add a common name (used in the program) followed by the language specific text. This should be inserted at the bottom of the StringStore file where there is a blank line. 

There is also globalizing of forms (i.e changing properties of forms depending on a user's chosen culture/language). This is covered in the following link.

Here is [an MSDN article](http://msdn.microsoft.com/en-us/library/y99d1cd3%28v=vs.100%29.aspx) covering most of what is being said here. Note, however, it doesn't go into using the automagically generated static class StringStore which is made for you, but it is as easy as described above. The rest is still relevant.

### Bringing it all together

Once you've pushed your changes up to your fork here on Codeplex, the last stage is to make a Pull Request. Go to the Source Code tab, then select Send a Pull Request. Pick your fork and fill out any remaining information. 

Once that's done, someone will come along and review the change. If it's deemed to be acceptible, your change will go into Blindspot!

### Contributing more permenantly

If you want to make regular contributions as a Blindspot developer, you will need to contact the project coordinator, craigbrett17. You may find his contact details available on his [OpenHatch user page](http://openhatch.org/people/craigbrett17/).