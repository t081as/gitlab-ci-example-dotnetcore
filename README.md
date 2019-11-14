![DiabLaunch](https://gitlab.com/tobiaskoch/DiabLaunch/raw/master/img/DiabLaunch.png)

# DiabLaunch

[![pipeline status](https://gitlab.com/tobiaskoch/DiabLaunch/badges/master/pipeline.svg)](https://gitlab.com/tobiaskoch/DiabLaunch/commits/master)
[![maintained: yes](https://tobiaskoch.gitlab.io/badges/maintained-yes.svg)](https://gitlab.com/tobiaskoch/DiabLaunch/commits/master)
[![donate: paypal](https://tobiaskoch.gitlab.io/badges/donate-paypal.svg)](https://www.tk-software.de/donate)

A [Diablo 2](https://www.blizzard.com/de-de/games/d2/) full screen launcher.

Using Microsoft Windows 10 Diablo 2 crashes with the following error message:

    UNHANDLED EXCEPTION: ACCESS_VIOLATION (C0000005)

It is possible to launch Diablo 2 in windowed mode with the following command:

    > Diablo II.exe -nofixaspect -w

DiabLaunch executes Diablo 2 in windowed mode, removes the window border and sets the window to full screen mode on your primary screen.

## Installation

* DiabLaunch requires the [.NET Core Runtime 3.0](https://dotnet.microsoft.com/download/dotnet-core/3.0/runtime)
* DiabLaunch supports [xcopy deployment](https://en.wikipedia.org/wiki/XCOPY_deployment) so there is no need for a dedicated installation
* The Diablo 2 installation path is detected automatically using the [Windows Registry](https://en.wikipedia.org/wiki/Windows_Registry) (if this does not work on your system just copy the application into your Diablo 2 directory)

### Option 0: Binaries
Binary packages are available [here](https://gitlab.com/tobiaskoch/DiabLaunch/-/tags).

### Option 1: Source
#### Requirements
The following sdk must be available in order to build DiabLaunch from source:

* [.NET Core SDK 3.0 (Windows)](https://dotnet.microsoft.com/download)

#### Source code
Get the source code using the following command:

    > git clone https://gitlab.com/tobiaskoch/DiabLaunch.git

#### Build
    > ./build.ps1 --configuration Release

The application will be located in the directory *./output* if the build succeeds. A zip package will be created in the root directory of the repository.

## Contributing
see [CONTRIBUTING.md](https://gitlab.com/tobiaskoch/DiabLaunch/blob/master/CONTRIBUTING.md)

## Contributors
see [AUTHORS.txt](https://gitlab.com/tobiaskoch/DiabLaunch/blob/master/AUTHORS.txt)

## Donating
Thanks for your interest in this project. You can show your appreciation and support further development by [donating](https://www.tk-software.de/donate).

## License
**DiabLaunch** © 2018-2019  [Tobias Koch](https://www.tk-software.de). Released under the [GPL](https://gitlab.com/tobiaskoch/DiabLaunch/blob/master/LICENSE.md).
