NUnit-Gui 0.1 - April 8, 2016

General

This is the initial release of the NUnit GUI runner. This is pre-alpha code we are releasing
primarily for the purpose of getting feedback on the user interface itself. It is not suitable
for use in production.

Features

 * The left-hand panel displays the tests in a variety of ways.
   * As a traditional NUnit tree
   * As a list of fixtures
   * As a list of test cases

 * When displaying a list of fixtures or test cases, you may select various groupings. 
   This is similar to how the Visual Studio test explorer window works.

 * The right-hand panel displays information about the individual test selected in the 
   left-hand panel. It has two tabs.
   * Properties - shows the information about the test (upper pane) and it's result (lower pane)
   * XML - shows the XML representation for the test or test result

Issues Resolved

 * 17 Gui should highlight not-runnable and ignored tests immediately upon load
 * 19 CI Server for nunit-gui
 * 32 Load last test on startup
 * 33 Command-line with assemblies and projects specified
 * 34 Command-line run option
 * 35 Command-line noload option
 * 36 Gui Layout
 * 37 File Open Menu Item
 * 38 File Close Menu Item
 * 41 File Reload menu item
 * 44 Select Runtime Menu Item
 * 45 Test List Display
 * 46 Status Bar Content
 * 47 View Status Bar Menu Item
 * 48 Support checkboxes in the gui tree display
 * 49 Recent Files Menu
 * 55 Running tests from the gui
 * 58 Test details
 * 59 Invalid file (non-assembly) is displayed as an assembly.
 * 62 Group headings are not shown with correct icon
 * 66 Remove unsupported options from GuiOptions class
 * 68 Implement XML view of test details
 * 69 Reloading nunit project file causes engine error
 * 72 ReloadProject versus ReloadTests
 * 84 Add context menu to XmlView
 * 85 Display test result info in XmlView
