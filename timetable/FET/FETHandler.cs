using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BEP.timetable.FET
{
    public class FETHandler
    {
        private string filePath;
        private string FETFilePath;

        public FETHandler(string _filePath)
        {
            SetFilePath(_filePath);
        }


        public void SetFilePath(string _filePath){
            if(!_filePath.EndsWith(".fet")){
                Console.Write("[Error] not a .fet file");
            }

            filePath = _filePath;
        }

        public void SetFETFilePath(string _FETFilePath){

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string dir = Path.Combine(projectPath, "Resources");
            string finalString = Path.Combine(dir, _FETFilePath);

            Console.Write(finalString);
            FETFilePath = finalString;
        }

        public string GetFETFilePath(){
            if (FETFilePath == null)
            {
                Console.Write("[Error] not a FET file initialized");
            }
            return FETFilePath;
        }

        static void Main()
        {
            FETHandler fETHandler = new FETHandler(".fet");
            fETHandler.SetFETFilePath("fet-cl.exe");
            Process.Start(fETHandler.GetFETFilePath());
        }

    }

 
}
 