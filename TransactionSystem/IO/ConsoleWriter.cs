using System;
using TransactionSystem.IO.Interfaces;

namespace TransactionSystem.IO
{
    public class ConsoleWriter : IWriter
    {
        public void Write(string line) => Console.Write(line);

        public void WriteLine(string line) => Console.WriteLine(line);
    }
}