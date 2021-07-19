using System.IO;
using System.Reflection;

namespace QA_Automation_Databases
{
    public class Logger 
    {
        private readonly string _exePath = Path.GetDirectoryName(
            Assembly.GetExecutingAssembly().Location);

        public void LogWrite(string logMessage)
        {
            try
            {
                using StreamWriter writer = File.AppendText($"{_exePath}\\log.txt");
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
    }
}