using System.IO;

namespace AddressProcessing.CSV
{
    public class CSVReader
    {
        private readonly StreamReader readerStream;

        public CSVReader(string fileName)
        {
            readerStream = File.OpenText(fileName);
        }

        public CSVReader()
        {
            readerStream = null;
        }

        public string ReadLine()
        {
            return readerStream.ReadLine();
        }

        public void Close()
        {
            readerStream?.Close();
        }
    }
}