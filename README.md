# AdamServices.CheckingAvailability
[![.NET Build And Publish Release](https://github.com/Adam-Software/AdamServices.CheckingAvailability/actions/workflows/dotnet.yml/badge.svg)](https://github.com/Adam-Software/AdamServices.CheckingAvailability/actions/workflows/dotnet.yml)    
![GitHub License](https://img.shields.io/github/license/Adam-Software/AdamServices.CheckingAvailability)
![GitHub Release](https://img.shields.io/github/v/release/Adam-Software/AdamServices.CheckingAvailability)

A service for checking the availability of the Adam robot.

Use the shared [wiki](https://github.com/Adam-Software/AdamServices.Utilities.Managment/wiki) to find information about the project.

## For users
### Permanent links to releases
* **Windows [x64]**
  ```
  https://github.com/Adam-Software/AdamServices.CheckingAvailability/releases/latest/download/CheckingAvailability.win64.portable.zip

  ```
* **Linux [arm64]**
  ```
  https://github.com/Adam-Software/AdamServices.CheckingAvailability/releases/latest/download/CheckingAvailability.arm64.portable.zip

  ```

### Install
* **Windows [x64]**
  * Download using the [permalink](#permanent-links-to-releases)
  * Unzip and run CheckingAvailability.exe

* **Linux [arm64]**
  * Download using the [permalink](#permanent-links-to-releases)
    ```bash
    wget https://github.com/Adam-Software/AdamServices.CheckingAvailability/releases/latest/download/CheckingAvailability.arm64.portable.zip
    ```
  * Unzip and make the Management file executable
    ```bash
    unzip CheckingAvailability.arm64.portable.zip -d CheckingAvailability && chmod +x CheckingAvailability/CheckingAvailability
    ```
  * Run CheckingAvailability
    ```bash
    cd CheckingAvailability && ./CheckingAvailability
    ```

### Optional command line arguments
```cmd
-o, --old-port    The old local port that the service checking availability
                  listens on
-n, --new-port    The new local port that the service checking availability
                  listens on
--help            Display this help screen.
--version         Display version information.

```

### Setting file
Use `AppSettingsOptions` section in the settings file `appsettings.json` to change the port numbers at the next startup.
```json
"AppSettingsOptions": {
    "OldPortCheckingAvailability": 15000,
    "NewPortCheckingAvailability": 16000
  }
```

**Important!.** The [arguments](#optional-command-line-arguments) have a primary meaning before the settings. That is, when using arguments, the settings file will not be used!