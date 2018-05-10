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

        public FETHandler()
        {
            SetFETFilePath("fet-cl.exe");
        }


        public void SetFilePath(string _filePath){
            if(!_filePath.EndsWith(".fet")){
                Console.Write("[Error] This is not a .fet file");
            }

            filePath = _filePath;
        }

        public void SetFETFilePath(string _FETFilePath){

            var projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            string dir = Path.Combine(projectPath, "Resources");
            string finalString = Path.Combine(dir, _FETFilePath);
            FETFilePath = finalString;
        }

        public string GetFETFilePath(){
            if (FETFilePath == null)
            {
                Console.Write("[Error] A FET file is not initialized");
            }
            return FETFilePath;
        }

        public void connect(){
            Process.Start(GetFETFilePath());
        }

        static void Main()
        {
            FETHandler fETHandler = new FETHandler();
            fETHandler.connect();
        }

    }

 
}
 