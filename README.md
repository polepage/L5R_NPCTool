# NPC Generator for FFG's L5R RPG

This is an application used to create and manage NPCs stat blocks for [Fantasy Flight Game's](https://www.fantasyflightgames.com/) [Legend of the Five Rings RPG](https://www.fantasyflightgames.com/en/legend-of-the-five-rings-roleplaying-game/).

## For Game Masters

*Currently only working on Windows*

**Download:** Follow this [link](https://drive.google.com/open?id=1V4ykGw9KqLr8Lj8yzXNw3CabNH0lLkxD) and download the latest version.

**Installation:** There is no installer, just unzip the file somewhere. Start the application using NPC.Windows.exe. You should make a shortcut because the exe is lost in a large folder.

### Usage Notes

Application usage is relatively straightforward. Here is some quirks that may be confusing.

* Data is saved locally. Whenever you save something it goes in a special folder in the NPC folder, don't try to move anything from that folder, it loads automatically.
* To move data between 2 different installation, use the Import and Export feature, that use .l5r file to move data around.
* There is no hard link between anything. If you create and advantage, and then use that advantage in a character, it will be copied, not referenced, so any modification will not impact the other. This choice has been made because a lot in Five Ring's NPCs is not made to compute nicely like player character stats and abilities.
* In composite objects (Characters and Templates) there is two buttons on each component. One is to open and copy an existing item, the other is to open the current item in a new tab so it can be saved and reused.
* The large button near the top of Character editor opens a wizard to Select a template and it's options and apply it to the current Character.

### Ability Language

The abilities utilize a small wiki-like language to add rich formatting to ability text. Use the tool bar to test it, but the main markers are:
* Text between asterisks is bold. ** Bold **
* Text between slashs is italic. //Italic//
* If a line starts with a pipe, it will be indented by the number of pipes. |New indented Line
* Some words in curly braces insert a symbol. See the tool bar. {Strife}

### Maintenance

If you encounter any issues or have any feature suggestions, please look at the Issue tracker before submiting anything new. Also note that I have a game to prep (and my players are waiting because i nerd-snipped myself with that little project) so there may not be a lot of support for new features in the short term. However, I will be using this tool extensively in the coming weeks, I will fix issues mostly as I encounter them.

### Change Log

#### 1.3

* Fixed crash when opening About dialog.

#### 1.2

* Removed virtualization in TabControl. It fixes the issue where Skills and Spheres for advantages and disadvantages disapear when you open a new tab.
* General code clean up.

#### 1.1

* General bug fixes.

#### 1.0

* Initial Release.

## For Contributors

If you are fluent in C# and want to contribute, feel free to send a Pull Request.

Here are some things that are good targets for peope that want to help:
* Issues: If you want to fix a bug or implement a feature that is in the issue tracker, please speak up in the specific issue so we don't overlap.
* Port: Adapt the application for Mac, Linux and/or Mobile (see below).

### State of the code

The code base is not bad, I think. However, the UI dll is a bit messy (specifically the code that renders "Read-Only" elements such as the print preview), and obviously code documentation is lacking. I will try to progress on that front as soon as possible, and will be more motivated to do so if some people seem interested in contributing.

### Portability

Excluding the UI dll (that is in WPF, thus Windows only) and the Application (also Windows only, as it contains WPF linkage), all dll in this solution are built upon netstandard 2.0. So if someone plans to help porting the app on other desktops, it require only a new UI and Application project. For mobile, it may be a bit more work, since the Data project may require more in depth changes to save the data.

If anybody create a ported version it will be my pleasure to host it alongside the Windows version.

### Structure

Here is a small rundown on what is in the solution:
* CS.Utils (netstandard): Helper classes.
* NPC.Common (netstandard): Common elements (enums).
* NPC.Parser (netstandard): Formatter/Parser for Ability text.
* NPC.Data.Interface (netstandard): Interface for the data layer (responsible for loading and saving objects and storing current data). This interface should not move unless new features are added.
* NPC.Data (netstandard): Implementation of the data layer. This implementation uses XML to save information on disk.
* NPC.Presenter.Interface (netstandard): Interface for the presenter layer (responsible for object availables to the UI and non-data functionalities (Duplicate objects, etc)). This interface should not move unless new features are added.
* NPC.Presenter (netstandard): Implementation of the presenter layer.
* NPC.Presenter.Windows (WPF): WPF UI. Will only work on windows.
* NPC.Windows (WPF): Application. Will only work on windows.

The application uses Prism 7 container interfaces to abstract the DI container. Each implementation layer exposes a single public static class with an Initialize method that will use the container interface to register anything required for it to work.

In the current solution, the dependency chain looks like this:
* Everybody knows Common and CS.Utils.
* Parser is only used by the UI.
* Data only knows its interface.
* Presenter knows its interface and Data's interface.
* UI only knows Presenter interface.
* Application knows the Data, Presenter and Parser, to call the Initialization method.

