namespace TransactionSystem.IO.Interfaces
{
    public interface IWriter
    {
        void Write(string line);

        void WriteLine(string line);
    }
}