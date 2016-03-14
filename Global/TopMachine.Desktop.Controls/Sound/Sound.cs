using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Media;
using System.IO;
using CMWA.Packager;
using CMWA.Packager.Custom;

namespace TopMachine.Desktop.Controls.Sound
{
    public static class Sound
    {
        public enum SoundType
        {
            LOGIN,
            LOGOUT,
            CHAT,
            STARTGAME,
            ENDGAME, 
            ENDROUND, 
            BEFOREENDROUND

        }

        private static string getNameSound(SoundType st)
        {
            string namefile = "";

            switch (st)
            {
                case SoundType.LOGIN:
                    namefile = "TopMachineData\\Sounds\\Login";
                    break;
                case SoundType.LOGOUT:
                    namefile = "TopMachineData\\Sounds\\Logout";
                    break;
                case SoundType.CHAT:
                    namefile = "TopMachineData\\Sounds\\Chat";
                    break;
                case SoundType.STARTGAME:
                    namefile = "TopMachineData\\Sounds\\StartGame";
                    break;
                case SoundType.ENDGAME:
                    namefile = "TopMachineData\\Sounds\\NewGame";
                    break;
                case SoundType.BEFOREENDROUND:
                    namefile = "TopMachineData\\Sounds\\BeforeEndRound";
                    break;
                case SoundType.ENDROUND:
                    namefile = "TopMachineData\\Sounds\\EndRound";
                    break;
                default:
                    break;
            }
            return namefile;
        }
        public static void play(SoundType st)
        {
            try
            {

                string path;
                using (SoundPlayer objPlayer = new SoundPlayer())
                {
                    path = getNameSound(st);
                    path = PackageManager.Instance.Project.GetFileName(path);
                    objPlayer.SoundLocation = path;
                    objPlayer.Play();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        private static Stream GetEmbeddedResourceStream()
        {
            System.Reflection.Assembly objAssembly;
            Stream soundstream;

            objAssembly = System.Reflection.Assembly.LoadFrom("");
            soundstream = objAssembly.GetManifestResourceStream("");
            return soundstream;

        }


    }
}
