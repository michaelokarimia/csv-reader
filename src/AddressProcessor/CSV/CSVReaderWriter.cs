using System;

namespace AddressProcessing.CSV
{
    /*
        2) Refactor this class into clean, elegant, rock-solid & well performing code, without over-engineering.
           Assume this code is in production and backwards compatibility must be maintained.
    */

    public class CSVReaderWriter
    {
        private CSVReader csvReader;
        private CSVWriter csvWriter;

        public CSVReaderWriter()
        {
            csvReader = new CSVReader();
            csvWriter = new CSVWriter();
        }

        [Flags]
        public enum Mode { Read = 1, Write = 2 };

        public void Open(string fileName, Mode mode)
        {
            if (mode == Mode.Read)
            {
                csvReader = new CSVReader(fileName);
            }
            else if (mode == Mode.Write)
            {
                csvWriter = new CSVWriter(fileName);
            }
            else
            {
                throw new Exception("Unknown file mode for " + fileName);
            }
        }

        public void Write(params string[] columns)
        {
            var formattedLine = GetTabDelimitedString(columns);
            csvWriter.WriteLine(formattedLine);
        }

        private static string GetTabDelimitedString(string[] columns)
        {
            string outPut = "";

            for (int i = 0; i < columns.Length; i++)
            {
                outPut += columns[i];
                if ((columns.Length - 1) != i)
                {
                    outPut += "\t";
                }
            }
            return outPut;
        }

        public bool Read(string column1, string column2)
        {
            return IsNextLineTabDelimited();
        }

        private bool IsNextLineTabDelimited()
        {
            char[] separator = {'\t'};

            var line = csvReader.ReadLine();
            var columns = line.Split(separator);

            return columns.Length != 0;
        }

        public bool Read(out string column1, out string column2)
        {
            const int FIRST_COLUMN = 0;
            const int SECOND_COLUMN = 1;

            string line;
            string[] columns;

            char[] separator = { '\t' };

            line = csvReader.ReadLine();

            if (line == null)
            {
                column1 = null;
                column2 = null;

                return false;
            }

            columns = line.Split(separator);

            if (columns.Length == 0)
            {
                column1 = null;
                column2 = null;

                return false;
            } 
            else
            {
                column1 = columns[FIRST_COLUMN];
                column2 = columns[SECOND_COLUMN];

                return true;
            }
        }

        public void Close()
        {
            csvWriter.Close();
            csvReader.Close();
        }
    }
}
