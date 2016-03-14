using System;

using System.Collections.Generic;
using System.Text;

namespace CMWA.Packager
{

    public class FileUtility
    {
        public static string GetFile(string f, LocationType type)
        {

            switch (type)
            {
                case LocationType.CustomFiles:
                    return GetApplicationDirectory();
                case LocationType.CommonFiles:
                    return GetCommonDataDirectory() + "\\" + f;
                case LocationType.PersonalFiles:
                    return GetPersonalFiles() + "\\" + f;
                case LocationType.ProgramFiles:
                    return GetApplicationDirectory() + "\\" + f;
            }

            return f;
        }

        static string GetPersonalFiles()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TopMachineConfig";
        }

        static string GetUserApplicationLocalDirectory()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        }


        static string GetUserApplicationDirectory()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        static string GetUserDataDirectory()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.Personal); 
        }

        static string GetCommonProgramDirectory()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.CommonProgramFiles); 
        }

        static string GetCommonDataDirectory()
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData); // +"\\TopMachine " + Scrabble.Version.Version.VersionNumber;
        }

        static string GetApplicationDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}
