
# Introduction
This is a C# (.NET 3.5) library that is built to interface with the Microchip MCP2210 USB to SPI device.

## License
The library is using the MIT License and it's open for commercial usage. 

## Dependences
The only dependency is HidSharp, a multi-platform library that supports Windows, MacOS, and Linux (hidraw).
Tou can find it at the following link: http://www.zer7.com/software/hidsharp.
The reason why I used this library, is related to its simplicity, documentation and stability. And, of course, the ISC license,
that is compatible with the MIT one.

## General view
The library supports all the features of the MCP2210, which are explained in the datasheet. 
The main interface is given by the IUsbToSpiDevice, that contains the modules used to interface with various parts of the device.
The modules are the following:

 * INonVoltatileRam: the non-volatile RAM module
 * IVolatileRam: the volatile RAM module
 * IExternalInterruptPin: the external interrupt pin module
 * ISpiDataTransfer: the SPI data transfer module
 * IEepromMemory: the EEPROM module
 
## Help & Debug
If you need any help, don't exitate to ask me any question.

## Documentation
The library is documented and there's not the wiki page yet.