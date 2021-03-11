# NukeeperConfigUI

*A simple WPF UI to enable the user to build a config item which can be used to call [nukeeper](https://github.com/NuKeeperDotNet/NuKeeper) to update a project.*

## Usage

Use the `NugetUpdateConfigView` to call it as a Dialog through the prism `IDialogService`. Remember to register the view as a Dialog beforehand.

Configure the `NukeeperHelper` to point to the nuget.exe available on the target machine. The nuget.exe needs to be placed into the PATH variable.

## Important

This package is nothing special and can be build be everyone with WPF Knowledge within a day. But as it was build to call the publicly available nukeeper we decided to make it public as well. It is only a simple UserControl which drops a config containing the config to call nukeeper.exe.

