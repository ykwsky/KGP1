using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D2RCharacterCopy
{
    public static class SharedData
    {
        private static string CustomPath = @"C:\";
        public static string BackupFile = "";

        public static string GetPath()
        {
            CustomPath = Convert.ToString(Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\D2RCharacterCopy", @"CustomPath", @"C:\"));

            return CustomPath;
        }

        public static void SetPath(string path)
        {
            CustomPath = path;
            Registry.SetValue(@"HKEY_CURRENT_USER\SOFTWARE\D2RCharacterCopy", @"CustomPath", path);
        }
    }
}
