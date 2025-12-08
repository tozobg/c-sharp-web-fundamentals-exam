using System;
using TransactionSystem.Core;
using TransactionSystem.Core.Interfaces;
using TransactionSystem.IO;

namespace TransactionSystem
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            // Create engine with reader and writer
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter());

            // Start program
            engine.Run();
        }
    }
}
