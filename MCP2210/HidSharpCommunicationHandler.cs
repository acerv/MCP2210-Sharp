
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

using HidSharp;
using System;

namespace MCP2210 {
    class HidSharpCommunicationHandler : IHidCommunicationHandler {
        private static HidDevice _device;

        public HidSharpCommunicationHandler() {
        }

        public void Close() {
            // No closing methods for the HidDevice.
        }

        private static byte[] CreatePacketWithReportId(byte[] packet) {
            byte[] dataBytesWithReportId = new byte[packet.Length + 1];
            Array.Copy(packet, 0, dataBytesWithReportId, 1, packet.Length);
            return dataBytesWithReportId;
        }

        private static byte[] RemoveReportIdFromPacket(byte[] packetWithReportId) {
            byte[] usbReplyBytes = new byte[packetWithReportId.Length - 1];
            Array.Copy(packetWithReportId, 1, usbReplyBytes, 0, usbReplyBytes.Length);
            return usbReplyBytes;
        }

        public byte[] WriteData(byte[] data) {
            if (_device == null) {
                throw new DeviceNotFoundException();
            }

            HidStream stream;
            if (_device.TryOpen(out stream)) {
                // get the command with report id
                byte[] cmdBytesWithReportId = CreatePacketWithReportId(data);

                // get reply without report id
                stream.Write(cmdBytesWithReportId);
                byte[] usbReplyBytesWithReportId = stream.Read();
                byte[] usbReplyBytes = RemoveReportIdFromPacket(usbReplyBytesWithReportId);
                
                return usbReplyBytes;
            }
            return null;
        }

        public void Open() {
            HidDeviceLoader loader = new HidDeviceLoader();
            _device = loader.GetDeviceOrDefault(Constants.VendorId, Constants.ProductId);
            if (_device == null) {
                throw new DeviceNotFoundException();
            }

            _device.Open();
        }
    }
}
