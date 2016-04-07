
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
    /// The non volatile RAM handler.
    /// </summary>
    public interface INonVolatileRam : IRamModule {
        /// <summary>
        /// The manufacter name setter/getter.
        /// </summary>
        string ManufacterName { get; set; }

        /// <summary>
        /// The product name setter/getter.
        /// </summary>
        string ProductName { get; set; }

        /// <summary>
        /// This method reads the USB power settings.
        /// </summary>
        UsbKeyPowerSettings ReadUsbSettings();

        /// <summary>
        /// This method writes over the USB power-on settings.
        /// </summary>
        /// <param name="settings">The USB power-on settings.</param>
        void WriteUsbSettings(UsbKeyPowerSettings settings);
    }
    
    /// <summary>
    /// The USB key power settings.
    /// </summary>
    public struct UsbKeyPowerSettings {
        public int PID;
        public int VID;
        /// <summary>
        /// The USB requested current in mA.
        /// </summary>
        public int RequestedCurrent;
        public bool HostPowered;
        public bool SelfPowered;
        public bool RemoteWakeUpCapable;
    }
}
