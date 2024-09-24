# SimpleS7OpcClient
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT) [![build and test](https://github.com/philipp2604/SimpleS7OpcClient/actions/workflows/build-and-test.yml/badge.svg)](https://github.com/philipp2604/SimpleS7OpcClient/actions/workflows/build-and-test.yml) ![GitHub Release](https://img.shields.io/github/v/release/philipp2604/SimpleS7OpcClient) [![NuGet Version](https://img.shields.io/nuget/v/philipp2604.SimpleS7OpcClient)](https://www.nuget.org/packages/philipp2604.SimpleS7OpcClient/)




## Description 
This library wraps [nauful's LibUA library](https://github.com/nauful/LibUA/) to provide easy communication with Siemens S7 controllers using their built-in OPC server.

**This library is still WIP and not complete yet.**

**The usage in production environments is strongly discouraged!**

**Only anonymous access without login and encryption has been tested yet.**

## Quick Start
Please have a look at the included example application.

1. Create a new instance of `S7OpcClient` with the application specific parameters (application description, connection settings, ...)
2. Create a new instance of `S7OpcClientService` using the prior created `S7OpcClient` instance.
3. Read and write tags from tag tables:
    a. Read a tag by using `S7OpcClientService.ReadSingleTableTag()`.
    b. Write to a tag by using `S7OpcClientService.WriteSingleTableTag()`.
4. Read and write DataBlock variables:
    a. Read a variable by using `S7OpcClientService.ReadSingleDbVar()`.
    a. Write to a variable by using `S7OpcClientService.WriteSingleDbVar()`.

## State of implementation
* Anything but anonymous, unencrypted access has not been tested yet.
* Custom PLC data types are not supported yet, but all of the 'everyday' types are implemented.

## Download
You can acquire this library either directly via the NuGet package manager or by downloading it from the [NuGet Gallery](https://www.nuget.org/packages/philipp2604.SimpleS7OpcClient/).

## Questions? Problems?
**Feel free to reach out!**

## Ideas / TODO
* Implement custom PLC data types
* Tests!

## Third Party Software / Packages
Please have a look at [THIRD-PARTY-LICENSES](./THIRD-PARTY-LICENSES.md) for all the awesome packages used in this library.

## License
This library is [MIT licensed](./LICENSE.txt).