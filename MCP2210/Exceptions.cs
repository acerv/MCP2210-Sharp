
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

using System;

namespace MCP2210 {
    [Serializable]
    public class DeviceNotFoundException : Exception {
        public DeviceNotFoundException() :
            base("Device not found.") {
        }
    }

    [Serializable]
    public class PacketReplyFormatException : Exception {
        public PacketReplyFormatException() :
            base("The USB reply echoed back the wrong commands. Packet might be corrupted.") {
        }
    }

    [Serializable]
    public class AccessBlockedException : Exception {
        public AccessBlockedException() :
            base("The access is blocked.") {
        }
    }

    [Serializable]
    public class UsbtransferInProgressException : Exception {
        public UsbtransferInProgressException() :
            base("Settings not written.") {
        }
    }

    [Serializable]
    public class PasswordAccessRejectedException : Exception {
        public PasswordAccessRejectedException() :
            base("Password access rejected.") {
        }
    }

    [Serializable]
    public class PasswordDoesntMatchChipOnException : Exception {
        public PasswordDoesntMatchChipOnException() :
            base("Password doesn't match.") {
        }
    }

    [Serializable]
    public class PasswordMechanismIsLockedException : Exception {
        public PasswordMechanismIsLockedException() :
            base("Password doesn't match. No more attempts and device is locked.") {
        }
    }

    [Serializable]
    public class SpiBusNotAvailableException : Exception {
        public SpiBusNotAvailableException() :
            base("SPI bus not available (the external owner has control over it).") {
        }
    }

    [Serializable]
    public class SpiTransferInProgressException : Exception {
        public SpiTransferInProgressException() :
            base("SPI transfer in progress – cannot accept any data for the moment.") {
        }
    }

    [Serializable]
    public class SpiBusNotReleasedException : Exception {
        public SpiBusNotReleasedException() :
            base("SPI transfer in process.") {
        }
    }

    [Serializable]
    public class EEPROMIsPasswordProtectedOrPermanentlyLockedException : Exception {
        public EEPROMIsPasswordProtectedOrPermanentlyLockedException(byte address) :
            base("EEPROM is password protected or permanently locked.\n" +
                 "Failed to write in the EEPROM \'" + address + "\' address.") {
        }
    }

    [Serializable]
    public class EEPROMWriteFailureException : Exception {
        public EEPROMWriteFailureException(byte address) :
            base("Failed to write in the EEPROM \'" + address + "\' address.") {
        }
    }

    [Serializable]
    public class UnknownCommandException : Exception {
        public UnknownCommandException() :
            base("Unknown command.") {
        }
    }
}
