
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
    class ExternalInterruptPin : IExternalInterruptPin {
        private readonly IHidCommunicationHandler _hidHandler;

        public ExternalInterruptPin(IHidCommunicationHandler handler) {
            _hidHandler = handler;
        }

        public int ReadNumberOfEvents(bool resetEventCounter) {
            // create the packet request
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.CurrentNumberOfEvents;
            packet[1] = (byte)(resetEventCounter ? 0 : 1);

            // write the command on device
            byte[] reply = _hidHandler.WriteData(packet);

            // check for reply errors
            if (reply[0] != CommandCodes.CurrentNumberOfEvents) {
                throw new PacketReplyFormatException();
            }

            // according with the manual, the reply never faults
            if (reply[1] != ReplyStatusCodes.CompletedSuccessfully) {
                throw new NotImplementedException();
            }

            ushort counts = BitConverter.ToUInt16(packet, 4);
            return counts;
        }
    }
}
