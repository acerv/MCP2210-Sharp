
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
using System.Threading;

namespace MCP2210 {
    class SpiDataTransfer : ISpiDataTransfer {
        private readonly IHidCommunicationHandler _hidHandler;

        public SpiDataTransfer(IHidCommunicationHandler handler) {
            _hidHandler = handler;
        }

        private static SpiEngineStatus GetSpiEngineStatusFromBytes(byte[] data) {
            // according with the manual, the command never faults
            SpiEngineStatus response = new SpiEngineStatus();
            response.ExternalRequestPending = data[2] == 0;

            switch (data[3]) {
                case 0:
                    response.BusOwner = SpiBusOwner.NoOwner;
                    break;
                case 1:
                    response.BusOwner = SpiBusOwner.UsbBridge;
                    break;
                case 2:
                    response.BusOwner = SpiBusOwner.ExternalMaster;
                    break;
                default:
                    throw new NotImplementedException();
            }

            response.AttemptedPasswordAccesses = data[4];
            response.PasswordGuessed = data[5] == 1;

            return response;
        }

        public SpiEngineStatus CancelCurrentTransfer() {
            // create the packet
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.CancelCurrentSpiTransfer;

            // send the packet
            byte[] reply = _hidHandler.WriteData(packet);

            // check for errors
            if (reply[0] != CommandCodes.CancelCurrentSpiTransfer) {
                throw new PacketReplyFormatException();
            }

            // according with the manual, the command never faults
            SpiEngineStatus response = GetSpiEngineStatusFromBytes(reply);
            return response;
        }

        public void RequestSpiBusRelease(bool gp7ReleaseStatus = true) {
            // create the packet
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.RequestSpiBusRelease;

            // assign the release status bit
            packet[1] = (byte)(gp7ReleaseStatus ? 1 : 0);
            
            // send the packet
            byte[] reply = _hidHandler.WriteData(packet);

            // check for errors
            switch (reply[1]) {
                case ReplyStatusCodes.CompletedSuccessfully:
                    break;
                case ReplyStatusCodes.SpiBusNotReleased:
                    throw new SpiBusNotReleasedException();
                default:
                    throw new NotImplementedException();
            }
        }

        public TransferReply Transfer(byte[] data) {
            // 0 to 60 inclusively is the maximum length of data (64bytes SPI data minus 4bytes header)
            if (data == null || data.Length > 60) {
                throw new ArgumentException("Spi data transfer must be non-null and should not exceed 60 bytes size.");
            }

            // create the packet
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.TransferSpiData;
            packet[1] = (byte)data.Length;

            for (int i = 0; i < data.Length; i++) {
                packet[i + 4] = data[i];
            }

            // write on device and read the reply
            byte[] reply = _hidHandler.WriteData(packet);

            // check for errors
            if (reply[0] != CommandCodes.TransferSpiData) {
                throw new PacketReplyFormatException();
            }

            switch (reply[1]) {
                case ReplyStatusCodes.CompletedSuccessfully:
                    break;
                case ReplyStatusCodes.SpiBusNotAvailable:
                    throw new SpiBusNotAvailableException();
                case ReplyStatusCodes.SpiTransferInProgress:
                    throw new SpiTransferInProgressException();
                default:
                    throw new NotImplementedException();
            }

            // create the reply data
            TransferReply transfReply = new TransferReply();
            
            switch (reply[3]) {
                case ReplyStatusCodes.SPITransferStarted:
                    transfReply.TransferStatus = SpiTransferStatus.Started;
                    break;
                case ReplyStatusCodes.SPITransferNotFinished:
                    transfReply.TransferStatus = SpiTransferStatus.Waiting;
                    break;
                case ReplyStatusCodes.SPITransferFinished:
                    transfReply.TransferStatus = SpiTransferStatus.Ended;
                    break;
                default:
                    throw new NotImplementedException();
            }

            // read the SPI return data 
            int dataLength = reply[2];
            transfReply.Data = new byte[dataLength];
            for (int i = 0; i < dataLength; i++) {
                transfReply.Data[i] = reply[i + 4];
            }

            return transfReply;
        }

        public TransferReply TransferAndRead(byte[] data, int timeoutMS) {
            // send the first command
            // 0 to 60 inclusively is the maximum length of data (64bytes SPI data minus 4bytes header)
            if (data == null || data.Length > 60) {
                throw new ArgumentException("Spi data transfer must be non-null and should not exceed 60 bytes size.");
            }

            // create the packet
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.TransferSpiData;
            packet[1] = (byte)data.Length;

            for (int i = 0; i < data.Length; i++) {
                packet[i + 4] = data[i];
            }

            // write on device and read the reply
            byte[] reply = _hidHandler.WriteData(packet);

            // check for errors
            if (reply[0] != CommandCodes.TransferSpiData) {
                throw new PacketReplyFormatException();
            }

            // continuously read the SPI bus status and wait till the data is ready
            DateTime timeout = DateTime.Now.AddMilliseconds(timeoutMS);

            while (reply[3] != ReplyStatusCodes.SPITransferFinished) {
                // check for timeout
                if (timeout <= DateTime.Now) {
                    throw new TimeoutException("SPI device is not replying.");
                }

                reply = _hidHandler.WriteData(packet);
            }

            // create the reply data
            TransferReply transfReply = new TransferReply();

            switch (reply[3]) {
                case ReplyStatusCodes.SPITransferStarted:
                    transfReply.TransferStatus = SpiTransferStatus.Started;
                    break;
                case ReplyStatusCodes.SPITransferNotFinished:
                    transfReply.TransferStatus = SpiTransferStatus.Waiting;
                    break;
                case ReplyStatusCodes.SPITransferFinished:
                    transfReply.TransferStatus = SpiTransferStatus.Ended;
                    break;
                default:
                    throw new NotImplementedException();
            }

            // read the SPI return data 
            int dataLength = reply[2];
            transfReply.Data = new byte[dataLength];
            for (int i = 0; i < dataLength; i++) {
                transfReply.Data[i] = reply[i + 4];
            }

            return transfReply;
        }

        public SpiEngineStatus ReadEngineStatus() {
            // create the packet
            byte[] packet = new byte[Constants.PacketsSize];
            packet[0] = CommandCodes.ReadCurrentSpiStatus;

            // send the packet
            byte[] reply = _hidHandler.WriteData(packet);

            // check for errors
            if (reply[0] != CommandCodes.ReadCurrentSpiStatus) {
                throw new PacketReplyFormatException();
            }

            // get the response
            SpiEngineStatus response = GetSpiEngineStatusFromBytes(reply);
            return response;
        }
    }
}
