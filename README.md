[![Quality Gate Status](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_lib-nanoFramework.BluetoothHID&metric=alert_status)](https://sonarcloud.io/dashboard?id=nanoframework_lib-nanoFramework.BluetoothHID) [![Reliability Rating](https://sonarcloud.io/api/project_badges/measure?project=nanoframework_lib-nanoFramework.BluetoothHID&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=nanoframework_lib-nanoFramework.Bluetooth.Hid) [![License](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE) [![NuGet](https://img.shields.io/nuget/dt/nanoFramework.Bluetooth.Hid.svg?label=NuGet&style=flat&logo=nuge)](https://www.nuget.org/packages/nanoFramework.Bluetooth.Hid/) [![#yourfirstpr](https://img.shields.io/badge/first--timers--only-friendly-blue.svg)](https://github.com/nanoframework/Home/blob/main/CONTRIBUTING.md) [![Discord](https://img.shields.io/discord/478725473862549535.svg?logo=discord&logoColor=white&label=Discord&color=7289DA)](https://discord.gg/gCyBu8T)

![nanoFramework logo](https://raw.githubusercontent.com/nanoframework/Home/main/resources/logo/nanoFramework-repo-logo.png)

# nanoFramework Bluetooth HID

This library contains an implementation of Bluetooth Low Energy HID Keyboard and Mouse (more in the future).

## Build status

| Component | Build Status | NuGet Package |
|:-|---|---|
| nanoFramework.Bluetooth.Hid | [![Build Status](https://dev.azure.com/nanoframework/nanoFramework.Bluetooth.Hid/_apis/build/status%2Fnanoframework.nanoFramework.Bluetooth.Hid?branchName=main)](https://dev.azure.com/nanoframework/nanoFramework.Bluetooth.Hid/_build/latest?definitionId=105&branchName=main) |

## Hardware requirements

Currently only support on ESP32 devices running on of the following firmware images:

- ESP32_BLE_REV0
- ESP32_BLE_REV3
- ESP32_PSRAM_BLE_GenericGraphic_REV3
- ESP32_S3_BLE
- M5Core2
- LilygoTWatch2021
- ESP32_ETHERNET_KIT_1.2

The Bluetooth is not in every firmware due to a restriction in the IRAM memory space in the firmware image. For earlier revision 1 ESP32 devices, the PSRAM implementation required a large number of PSRAM library fixes which greatly reduces the available space in the IRAM area, so PSRAM is currently disabled for ESP32_BLE_REV0. With the revision 3 devices the Bluetooth and PSRAM are both available.

## Usage

### Keyboard

Start by initializing a `Keyboard` class:

```csharp
var kbd = new Keyboard(deviceName: "nF BLE Keyboard",
    deviceInfo: new DeviceInformation("nF", "BLEKBD1", "1", "01", "01", "01"),
    protocolMode: ProtocolMode.Report,
    plugAndPlayElements: new PnpElements(sig: 0x02, vid: 0xE502, pid: 0xA111, version: 0x210));
```

The `deviceName` parameter is the name shown to any device scanning for Bluetooth.

`deviceInfo` parameter takes a `DeviceInformation` object instance which contains information about the manufacturer and device (serial number, hardware/software revision, etc...).

`protocolMode` specifies which HID Protocol to use: `ReportMode` (Default, Supported) or `BootMode` (Currently unsupported/untested).

`plugAndPlayElements` field sets various plug and play attributes that the HID Host Operating System uses to recognize and interact with the device. These are product-unique values and you can find a database of PnP devices on [this site](https://the-sz.com/products/usbid/index.php?v=&p=&n=Keyboard).

Next, the `Keyboard` must be initialized and advertised on Bluetooth. This is done with these two lines:

```csharp
kbd.Initialize();
kbd.Advertise();
```

The keyboard should now be discoverable to nearby devices. To stop advertising, call `kbd.StopAdvertising()`.

Once the keyboard is paired with a host, key presses can be simulated:

#### Simulating key presses

`Keyboard` contains methods to simulate key presses and releases. Example usage:

```csharp
// open task manager
kbd.Press(Keys.Modifiers.LeftCtrl);
kbd.Press(Keys.Modifiers.LeftShift);
kbd.Press(Keys.Control.Escape);

kbd.ReleaseAll();
```

Alternatively, `Send` is a shortcut method that makes the code above shorter:

`kbd.Send(Keys.Modifiers.LeftCtrl, Keys.Modifiers.LeftShift, Keys.Control.Escape);`

When a key is pressed using `Press`, you can release only that key from the set of pressed keys using `Release(key)` and passing the key to release.

#### Simulating typing on a keyboard

This is done using the `KeyboardUtilities` class:

```csharp
KeyboardUtilities.TypeText(kbd, "Hello, World. I want to play a game.");
```

### Mouse

Initializing a `Mouse` is done in the same way as `Keyboard`:

```csharp
var mouse = new Mouse("nF BLE Mouse",
    deviceInfo: new DeviceInformation("nF", "BLEMOUSE1", "1", "01", "01", "01"),
    protocolMode: ProtocolMode.Report,
    plugAndPlayElements: new PnpElements(sig: 0x02, vid: 0xE502, pid: 0xA111, version: 0x210));

mouse.Initialize();
mouse.Advertise();
```

Once connected to a Host, basic mouse functions can be simulated.

#### Simulating Mouse Movement

```cshrap
// move to the right and bottom (diagonal)
mouse.Move(x: 5, y: 5);
```

The `X` & `Y` values are cumulative as the OS will increment the `X` & `Y` from previous `Move` calls.

#### Simulating Scrolling

```csharp
// scroll down
mouse.Scroll(-5);
```

#### Simulating Button Clicks

```csharp
// left click something
mouse.Click(MouseButton.Left);
```

## Samples

Sample projects using this library can be found in the `Samples` folder. They can be deployed directly to a device and used.

## Feedback and documentation

For documentation, providing feedback, issues and finding out how to contribute please refer to the [Home repo](https://github.com/nanoframework/Home).

Join our Discord community [here](https://discord.gg/gCyBu8T).

## Credits

The list of contributors to this project can be found at [CONTRIBUTORS](https://github.com/nanoframework/Home/blob/main/CONTRIBUTORS.md).

## License

The **nanoFramework** Class Libraries are licensed under the [MIT license](LICENSE.md).

## Code of Conduct

This project has adopted the code of conduct defined by the Contributor Covenant to clarify expected behavior in our community.
For more information see the [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## .NET Foundation

This project is supported by the [.NET Foundation](https://dotnetfoundation.org).