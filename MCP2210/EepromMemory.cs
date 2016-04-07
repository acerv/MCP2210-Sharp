
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
    class EepromMemory : IEepromMemory {
        private readonly IHidCommunicationHandler _hidHandler;

        public EepromMemory(IHidCommunicationHandler handler) {
            _hidHandler = handler;
        }

        public byte ReadAddress(byte address) {
            // create the packet
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.EEPROMRead;
            packet[1] = address;

            // write the packet
            byte[] reply = _hidHandler.WriteData(packet);

            // check for reply errors
            if (reply[0] != CommandCodes.EEPROMRead || reply[2] != address) {
                throw new PacketReplyFormatException();
            }

            // according with the manual, the reply never faults
            if (reply[1] != ReplyStatusCodes.CompletedSuccessfully) {
                throw new NotImplementedException();
            }

            // return the content of the address
            byte content = reply[3];
            return content;
        }

        public void WriteAddress(byte address, byte content) {
            // create the packet
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.EEPROMWrite;
            packet[1] = address;
            packet[2] = content;

            // write the packet
            byte[] reply = _hidHandler.WriteData(packet);

            // check for reply errors
            if (reply[0] != CommandCodes.EEPROMWrite) {
                throw new PacketReplyFormatException();
            }
            
            switch (reply[1]) {
                case ReplyStatusCodes.CompletedSuccessfully:
                    break;
                case ReplyStatusCodes.EEPROMWriteFailure:
                    throw new EEPROMWriteFailureException(address);
                case ReplyStatusCodes.EEPROMIsPasswordProtectedOrPermanentlyLocked:
                    throw new EEPROMIsPasswordProtectedOrPermanentlyLockedException(address);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
