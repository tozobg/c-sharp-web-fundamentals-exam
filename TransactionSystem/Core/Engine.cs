using TransactionSystem.Core.Interfaces;
using TransactionSystem.IO.Interfaces;

namespace TransactionSystem.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        // Write code implementation
        public void Run()
        {
            writer.WriteLine("Hello World!");
        }
    }
}