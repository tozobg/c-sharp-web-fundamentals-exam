using System;
using TransactionSystem.IO.Interfaces;

namespace TransactionSystem.IO
{
    public class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();
    }
}