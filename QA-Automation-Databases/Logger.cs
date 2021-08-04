using System.IO;
using System.Reflection;

namespace QA_Automation_Databases
{
    public class Logger
    {
        private static readonly string _exePath = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);

        private readonly string _fileName;

        private string FullPath => $"{_exePath}\\log{_fileName}.txt";

        public Logger(string fileName)
        {
            _fileName = fileName;

            if (File.Exists(FullPath))
            {
                File.Delete(FullPath);
            }
        }

        public void LogWrite(string logMessage)
        {
            try
            {
                using StreamWriter writer = File.AppendText(FullPath);
                Log(logMessage, writer);
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        private void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.WriteLine("{0}", logMessage);
            }
            catch (IOException e)
            {
                throw e;
            }
        }

        public static bool IsLogFileEmptyOrNotExist(string fileName)
        {
            var fileToTest = new FileInfo($"{_exePath }\\log{fileName}.txt");
            if (!fileToTest.Exists || fileToTest.Length == 0)
                return true;
            else
                return false;
        }
    }
}