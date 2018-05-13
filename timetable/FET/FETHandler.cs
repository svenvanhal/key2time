using System;
using System.IO;

namespace BEP.timetable.FET
{
    public class FETHandler
    {
        private string filePath;
        private string FETFilePath;

        public FETHandler()
        {
          
            if (Environment.OSVersion.Platform.ToString().Equals("Unix"))
            {
                SetFETFilePath("fet-cl");
            }
            else
            {
                SetFETFilePath("fet-cl.exe");
            }
        }


        public void SetFilePath(string _filePath){
            if(!_filePath.Substring(_filePath.Length - 4).Equals(".fet")){
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
            String arg = "--inputfile=" + filePath;
            System.Diagnostics.Process.Start(GetFETFilePath(), arg);
        }

        static void Main()
        {
            FETHandler fETHandler = new FETHandler();
            fETHandler.SetFilePath("/Users/karimosman/Downloads/fet-5.35.6/examples/Greece/Corfu/THE-2008-2009-exams-sep.fet");
           fETHandler.connect();
        }

    }

 
}
 