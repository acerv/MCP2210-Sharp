
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
    /// This is a wrapper around the HID USB communication library.
    /// Implement this interface to integrate a new communication library.
    /// </summary>
    interface IHidCommunicationHandler {
        /// <summary>
        /// This method opens the HID device.
        /// </summary>
        void Open();

        /// <summary>
        /// This method closes the HID device.
        /// </summary>
        void Close();

        /// <summary>
        /// This method writes a command and it reads the reply of the USB device.
        /// </summary>
        /// <param name="data">The command to write.</param>
        /// <returns>The reply of the device..</returns>
        byte[] WriteData(byte[] data);
    }
}
