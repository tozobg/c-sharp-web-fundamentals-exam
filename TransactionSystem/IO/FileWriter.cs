using System.IO;
using TransactionSystem.IO.Interfaces;

namespace TransactionSystem.IO
{
    public class FileWriter : IWriter
    {
        public void Write(string line)
        {
            string filePath = "../../../test.txt";

            using StreamWriter sw = new(filePath, true);

            sw.Write(line);
        }

        public void WriteLine(string line)
        {
            string filePath = "../../../test.txt";

            using StreamWriter sw = new(filePath, true);

            sw.WriteLine(line);
        }
    }
}