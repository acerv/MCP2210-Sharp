
// Copyright(c) 2016 Andrea Cervesato <sawk.ita@gmail.com>

// Permission is hereby granted, free of charge, to any person
// obtaining a copy of this software and associated documentation
// files (the "Software"), to deal in the Software without
// restriction, including without limitation the rights to use,
// copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following
// conditions:

// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES
// OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
// WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
// OTHER DEALINGS IN THE SOFTWARE.

namespace MCP2210 {
    // command codes
    internal static class CommandCodes {
        public const byte WriteChipParameters = 0x60;
        public const byte ReadChipParameters = 0x61;
        public const byte GetGpioCurrentChipSettings = 0x20;
        public const byte SetGpioCurrentChipSettings = 0x21;
        public const byte SetGpioValueSettings = 0x30;
        public const byte GetGpioValueSettings = 0x31;
        public const byte SetGpioCurrentDirectionSettings = 0x32;
        public const byte GetGpioCurrentDirectionSettings = 0x33;
        public const byte SetSpiTransferSettings = 0x40;
        public const byte GetSpiTransferSettings = 0x41;
        public const byte AccessPassword = 0x70;
        public const byte ReadCurrentSpiStatus = 0x10;
        public const byte CancelCurrentSpiTransfer = 0x11;
        public const byte TransferSpiData = 0x42;
        public const byte RequestSpiBusRelease = 0x80;
        public const byte EEPROMRead = 0x50;
        public const byte EEPROMWrite = 0x51;
        public const byte CurrentNumberOfEvents = 0x12;
    }

    // sub-command codes: used by NVRAM module
    internal static class SubCommandCodes {
        public const byte SpiPowerUpTransferSettings = 0x10;
        public const byte ChipSettingsPowerUpDefault = 0x20;
        public const byte UsbPowerUpKeyParameters = 0x30;
        public const byte UsbProductName = 0x40;
        public const byte UsbManufacturerName = 0x50;
    }

    // error codes
    internal static class ReplyStatusCodes {
        public const byte CompletedSuccessfully = 0x00;
        public const byte UnkownCommand = 0xF9;
        public const byte BlockedAccess = 0xFB;
        public const byte AccessRejected = 0xFC;
        public const byte PasswordDoesntMatchChipOnLockedError = 0xFB;
        public const byte PasswordDoesntMatchChipOnError = 0xFD;
        public const byte SpiBusNotAvailable = 0xF7;
        public const byte SpiTransferInProgress = 0xF8;
        public const byte SpiBusNotReleased = 0xF8;
        public const byte SPITransferFinished = 0x10;
        public const byte SPITransferStarted = 0x20;
        public const byte SPITransferNotFinished = 0x30;
        public const byte UsbTransferInProgress = 0xF8;
        public const byte EEPROMWriteFailure = 0xFA;
        public const byte EEPROMIsPasswordProtectedOrPermanentlyLocked = 0xFB;
    }
}
