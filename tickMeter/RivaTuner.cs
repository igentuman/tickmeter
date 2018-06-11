using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace tickMeter
{
    public static class RivaTuner
    {
        [DllImport("kernel32")]
        private unsafe static extern void* LoadLibrary(string dllname);
        [DllImport("kernel32")]
        private unsafe static extern void FreeLibrary(void* handle);

        private sealed unsafe class LibraryUnloader
        {
            internal LibraryUnloader(void* handle)
            {
                this.handle = handle;
            }

            ~LibraryUnloader()
            {
                if (handle != null)
                    FreeLibrary(handle);
            }

            private void* handle;

        } // LibraryUnloader

        private static readonly LibraryUnloader unloader;

        static RivaTuner()
        {
            if (!IsRivaRunning())
            {
                RunRiva();
            }

            string path;

            if (IntPtr.Size == 4)
                path = "x86/rivatuner.dll";
            else
                path = "x64/rivatuner.dll";

            unsafe
            {
                void* handle = LoadLibrary(path);

                if (handle == null)
                    throw new DllNotFoundException("Unable to find the native rivatuner library: " + path);

                unloader = new LibraryUnloader(handle);
            }
        }

        public static bool IsRivaRunning()
        {
            Process[] pname = Process.GetProcessesByName("RTSS");
            if (pname.Length == 0)
                return false;
            else
                return true;
        }

        public static void RunRiva()
        {
            FileInfo f = new FileInfo(@"C:\Program Files (x86)\RivaTuner Statistics Server\RTSS.exe");
            if (f.Exists)
            {
                try
                {
                    Process.Start(f.FullName);
                    Thread.Sleep(2000);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }

        [DllImport("rivatuner", CallingConvention = CallingConvention.Cdecl)]
        public static extern bool print(string text);
    }
}
