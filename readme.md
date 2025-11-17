# PocApp — README

## Requirements
- .NET SDK 10.0 (install from Microsoft)
- .NET MAUI workloads
- Java SDK (for Android)
- On macOS: Xcode (for iOS / Mac Catalyst) and Android Studio (optional for Android emulator)
- On Windows: Visual Studio 2022/2023 or later with the MAUI workload (if using Visual Studio)

Official install guide:  
https://learn.microsoft.com/dotnet/maui/get-started/installation?view=net-maui-10.0

## Quick setup (common)
1. Verify .NET:
   - dotnet --info
2. Install MAUI workloads (one-time):
   - dotnet workload install maui
3. Restore and build:
   - dotnet restore
   - dotnet build

## Change API base URL
Edit ApiConfig.cs and set BaseURL before running:
- Path: <project>/Services/ApiConfig.cs (or search for `ApiConfig`)

## Run targets (choose one)
Replace <TARGET_FRAMEWORK> with one of the MAUI targets used by the project, for example:
- net10.0-android
- net10.0-ios
- net10.0-maccatalyst
- net10.0-windows10.0 (WinUI)

Generic run command (CLI):
- dotnet build -t:Run -f <TARGET_FRAMEWORK>

Notes:
- For iOS simulator on macOS, select the simulator in Visual Studio or use the iOS target in VS Code / CLI.
- For Android, start an emulator (Android Studio AVD) or connect a device.
- For WinUI on Windows, run from Visual Studio or use the appropriate Windows target in CLI.

## macOS — run options
- Mac Catalyst (run as macOS app):
  - Open in Visual Studio for Mac or use CLI: dotnet build -t:Run -f net10.0-maccatalyst
- iOS Simulator:
  - Open solution in Visual Studio for Mac or select an iOS simulator from your IDE and run, or use CLI with the iOS target and simulator properties.
- Android:
  - Start an Android emulator, then run the Android target.

## Windows — run options

Option A — Visual Studio (recommended)
1. Open the solution (.sln) in Visual Studio.
2. Select the target platform (Windows / Android / etc.) from the run/debug dropdown.
3. Press Run (F5) or Debug.

Option B — Visual Studio Code
1. Ensure VS Code has C# and any recommended MAUI extensions configured.
2. Restore and build: dotnet restore && dotnet build
3. Run with CLI specifying a target framework: dotnet build -t:Run -f <TARGET_FRAMEWORK>
4. Use an Android emulator for Android targets, or run the WinUI target on Windows.

## Troubleshooting
- If a workload or SDK is missing, run: dotnet workload install maui
- Verify Java SDK and Android SDK paths when running Android
- For iOS builds, ensure Xcode/command-line tools are installed and simulator is available

## Useful commands (macOS/Windows Terminal)
- dotnet --info
- dotnet workload install maui
- dotnet restore
- dotnet build
- dotnet build -t:Run -f <TARGET_FRAMEWORK>