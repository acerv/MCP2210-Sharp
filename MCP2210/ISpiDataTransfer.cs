
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
    /// <summary>
    /// The SPI data transfer module.
    /// </summary>
    public interface ISpiDataTransfer {
        /// <summary>
        /// This method transfers data into the SPI bus.
        /// </summary>
        /// <param name="data">The data to transfer.</param>
        /// <returns>The SPI bus return data.</returns>
        TransferReply Transfer(byte[] data);

        /// <summary>
        /// This method transfers data into the SPI bus and it waits to read the reply.
        /// </summary>
        /// <param name="data">The data to transfer.</param>
        /// <param name="timeoutMS">Reply timeout.</param>
        /// <returns>The SPI bus return data.</returns>
        TransferReply TransferAndRead(byte[] data, int timeoutMS);

        /// <summary>
        /// This method cancel the current SPI data transfer.
        /// </summary>
        /// <returns>The cancel response.</returns>
        SpiEngineStatus CancelCurrentTransfer();

        /// <summary>
        /// This method requests to release the SPI bus.
        /// When SPI bus is released and the GP7 is assigned to its dedicated function,
        /// the pin will be used by the module to set its digital status. You can choose
        /// that particular status with this method.
        /// </summary>
        /// <param name="gp7ReleaseStatus">This is the status of the GP7, when it's assigned to its
        /// dedicated function.</param>
        void RequestSpiBusRelease(bool gp7ReleaseStatus = true);

        /// <summary>
        /// This method reads the current SPI engine status.
        /// </summary>
        /// <returns>The SPI engine status.</returns>
        SpiEngineStatus ReadEngineStatus();
    }

    public enum SpiTransferStatus {
        /// <summary>
        /// SPI Data Accepted. The SPI Transfer will start afterwards.
        /// </summary>
        Started,

        /// <summary>
        /// SPI Engine waiting for more data packets to complete the SPI Transfer.
        /// </summary>
        Waiting,

        /// <summary>
        /// SPI Transfer Ended. The response will contain the last received SPI data
        /// packet of the SPI Transfer.
        /// </summary>
        Ended
    }

    /// <summary>
    /// This reply is returned all the times a SPI transfer call is performed.
    /// </summary>
    public struct TransferReply {
        /// <summary>
        /// The SPI transfer status.
        /// </summary>
        public SpiTransferStatus TransferStatus;

        /// <summary>
        /// The last received SPI data packet.
        /// </summary>
        public byte[] Data;
    }
    
    /// <summary>
    /// The SPI bus owner.
    /// </summary>
    public enum SpiBusOwner {
        NoOwner,
        UsbBridge,
        ExternalMaster
    }

    /// <summary>
    /// This response contains the status of the SPI bus.
    /// </summary>
    public struct SpiEngineStatus {
        /// <summary>
        /// The status of the bus.
        /// </summary>
        public bool ExternalRequestPending;

        /// <summary>
        /// The bus owner transfer.
        /// </summary>
        public SpiBusOwner BusOwner;

        /// <summary>
        /// Informs the USB host on how many times the NVRAM password was tried.
        /// </summary>
        public int AttemptedPasswordAccesses;

        /// <summary>
        /// True when the password is correct.
        /// </summary>
        public bool PasswordGuessed;
    }
}
