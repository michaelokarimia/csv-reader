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
        public void Closes_Valid_File_without_exception()
        {
            _subject.Open(contacts_test_data_cvs, CSVReaderWriter.Mode.Read);
            
            Assert.DoesNotThrow(() =>_subject.Close());
        }



        [Test]
        public void Throws_Exception_when_Opening_invalid_filepath_in_read_mode()
        {

            Assert.Throws<FileNotFoundException>(() => _subject.Open(@"test_data\FileWhichDoesNotExist.dat", CSVReaderWriter.Mode.Read));
        }

        [Test]
        public void Write_can_create_a_file()
        {
            string expectedFilePath = @"test_data\file_to_write.text";
            var fileToWriteTo = new FileInfo(expectedFilePath);

            fileToWriteTo.Delete();

            Assert.That(!fileToWriteTo.Exists, string.Format("File should have been deleted {0} \n but still exists", fileToWriteTo.FullName));
            
            _subject.Open(expectedFilePath,CSVReaderWriter.Mode.Write);

            var actualFile = new FileInfo(expectedFilePath);

            Assert.That(actualFile.Exists, string.Format("File was not created in {0}", actualFile.FullName));
        }


        [Test]
        public void Checks_for_lines_that_have_a_valid_number_of_columns()
        {
            _subject.Open(contacts_test_data_cvs, CSVReaderWriter.Mode.Read);
            
            var hasValidFirstRow  = _subject.Read("", "");

            Assert.That(hasValidFirstRow, "First row had no columns");
        }

        [Test]
        public void Can_extract_name_and_address_from_line()
        {
            _subject.Open(contacts_test_data_cvs, CSVReaderWriter.Mode.Read);

            string name = "";
            string address = "";
            var hasValidFirstRow = _subject.Read(out name, out address);

            Assert.That(hasValidFirstRow, "First row had no columns");
            Assert.That(name, Is.Not.Empty, "Expected Name to not be empty");
            Assert.That(address, Is.Not.Empty, "Expected address to not be empty");
        }


        [Test]
        public void Write_Method_can_write_to_file()
        {
            string expectedFilePath = @"test_data\file_with_content_to_write_to.txt";
            var fileToWriteTo = new FileInfo(expectedFilePath);

            fileToWriteTo.Delete();

            Assert.That(!fileToWriteTo.Exists, string.Format("File should have been deleted {0} \n but still exists", fileToWriteTo.FullName));

            _subject.Open(expectedFilePath, CSVReaderWriter.Mode.Write);

            string[] columns = new string[] { "First Column", "Second column", "Third column"};
            _subject.Write(columns);
            _subject.Close();

            var actualFile = new FileInfo(expectedFilePath);

            Assert.That(actualFile.Exists, string.Format("File was not created in {0}", actualFile.FullName));
            Assert.That(actualFile.Length, Is.Not.EqualTo(0), string.Format("File was empty in {0}", actualFile.FullName));              
        }



        [TearDown]
        public void Teardown()
        {
            _subject.Close();
        }
        
     }
}
