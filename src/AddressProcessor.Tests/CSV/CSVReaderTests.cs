using System.IO;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVReaderTests
    {
        private CSVReader _subject;
        private string contacts_test_data_cvs;

        [SetUp]
        public void Setup()
        {
            contacts_test_data_cvs = @"test_data\contacts.csv";

            _subject = new CSVReader();
        }

        [Test]
        public void Opens_Valid_File_without_exception()
        {

            Assert.DoesNotThrow(() => new CSVReader(contacts_test_data_cvs));
        }

        [Test]
        public void Closes_Valid_File_without_exception()
        {
            _subject = new CSVReader(contacts_test_data_cvs);
            
            Assert.DoesNotThrow(() =>_subject.Close());
        }

        [Test]
        public void Closes_method_on_uninitialised_file_has_no_side_effects()
        {
            _subject = new CSVReader();

            Assert.DoesNotThrow(() => _subject.Close());
        }

        [Test]
        public void Throws_Exception_when_Opening_invalid_filepath_in_read_mode()
        {

            Assert.Throws<FileNotFoundException>(() => _subject = new CSVReader(@"test_data\FileWhichDoesNotExist.dat"));
        }


        [TearDown]
        public void Teardown()
        {
            _subject.Close();
        }
        
     }
}
