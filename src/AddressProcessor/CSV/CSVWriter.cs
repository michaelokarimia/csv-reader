using System.IO;

namespace AddressProcessing.CSV
{
    public class CSVWriter
    {
        private readonly StreamWriter writerStream;

        public CSVWriter()
        {
        }

        public CSVWriter(string fileName)
        {
            FileInfo fileInfo = new FileInfo(fileName);
            writerStream = fileInfo.CreateText();
        }

        public void WriteLine(string line)
        {
            writerStream.WriteLine(line);
        }

        public void Close()
        {
            writerStream?.Close();
        }
    }
}