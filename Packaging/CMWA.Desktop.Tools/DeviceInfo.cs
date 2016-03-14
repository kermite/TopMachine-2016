using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Xml.Serialization;
using System.IO;

namespace CMWA.Packager.Tools
{

    internal class DeviceInfo
    {
        [DllImport("coredll.dll")]
        private static extern bool KernelIoControl(Int32 IoControlCode, IntPtr
          InputBuffer, Int32 InputBufferSize, byte[] OutputBuffer, Int32
          OutputBufferSize, ref Int32 BytesReturned);

        private static Int32 FILE_DEVICE_HAL = 0x00000101;
        private static Int32 FILE_ANY_ACCESS = 0x0;
        private static Int32 METHOD_BUFFERED = 0x0;

        private static Int32 IOCTL_HAL_GET_DEVICEID =
            ((FILE_DEVICE_HAL) << 16) | ((FILE_ANY_ACCESS) << 14)
             | ((21) << 2) | (METHOD_BUFFERED);


        public static byte[] GetDeviceIDBytes()
        {
            byte[] OutputBuffer = new byte[256];
            
            Int32 OutputBufferSize, BytesReturned;
            OutputBufferSize = OutputBuffer.Length;
            BytesReturned = 0;

            bool retVal = KernelIoControl(IOCTL_HAL_GET_DEVICEID,
                    IntPtr.Zero,
                    0,
                    OutputBuffer,
                    OutputBufferSize,
                    ref BytesReturned);

            // If the request failed, exit the method now
            if (retVal == false)
            {
                return null;
            }

            Int32 PresetIDOffset = BitConverter.ToInt32(OutputBuffer, 4);
            Int32 PlatformIDOffset = BitConverter.ToInt32(OutputBuffer, 0xc);
            Int32 PlatformIDSize = BitConverter.ToInt32(OutputBuffer, 0x10);
            byte[] FinalBuffer = new byte[10+PlatformIDSize];

            Array.Copy(OutputBuffer, PresetIDOffset, FinalBuffer, 0, 10);
            Array.Copy(OutputBuffer, PlatformIDOffset, FinalBuffer, 10, PlatformIDSize);

            return FinalBuffer; 
        }
    }
}
