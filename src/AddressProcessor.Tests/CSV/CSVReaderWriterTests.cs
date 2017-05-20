using System.IO;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderWriterTests
    {
        private CSVReaderWriter _subject;
        private string contacts_test_data_cvs;

        [SetUp]
        public void Setup()
        {
            contacts_test_data_cvs = @"test_data\contacts.csv";

            _subject = new CSVReaderWriter();
        }

        [Test]
        public void Opens_Valid_File_without_exception()
        {

            Assert.DoesNotThrow(() =>_subject.Open(contacts_test_data_cvs, CSVReaderWriter.Mode.Read));
        }

        [Test]
        public void Throws_Exception_when_Opening_invalid_filepath()
        {

            Assert.Throws<FileNotFoundException>(() => _subject.Open(@"test_data\FileWhichDoesNotExist.dat", CSVReaderWriter.Mode.Read));
        }

    }
}
