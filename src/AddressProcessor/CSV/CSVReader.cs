using System.IO;

namespace AddressProcessing.CSV
{
    public class CSVReader
    {
        private readonly StreamReader readerStream;

        public CSVReader(string filePath)
        {
            if (File.Exists(filePath))
            {
                readerStream = File.OpenText(filePath);
            }
            else throw new FileNotFoundException(string.Format("Could not open file path: {0}", filePath));
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