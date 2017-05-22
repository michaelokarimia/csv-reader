using System.IO;
using AddressProcessing.CSV;
using NUnit.Framework;

namespace Csv.Tests
{
    [TestFixture]
    public class CSVWriterTests
    {
        private CSVWriter _subject;

        [SetUp]
        public void Setup()
        {
            _subject = new CSVWriter();
        }

        [Test]
        public void Closes_Valid_File_without_exception()
        {
            _subject = new CSVWriter(@"test_data\CSVWriterTestFile_To_Write_To.txt");
            
            Assert.DoesNotThrow(() =>_subject.Close());
        }

        [Test]
        public void Close_method_on_uninitialised_file_has_no_side_effects()
        {
            _subject = new CSVWriter();

            Assert.DoesNotThrow(() => _subject.Close());
        }


        [Test]
        public void Write_can_create_a_file()
        {
            string expectedFilePath = @"test_data\file_to_write.text";
            var fileToWriteTo = new FileInfo(expectedFilePath);

            fileToWriteTo.Delete();

            Assert.That(!fileToWriteTo.Exists, string.Format("File should have been deleted {0} \n but still exists", fileToWriteTo.FullName));
            
            _subject = new CSVWriter(expectedFilePath);

            var actualFile = new FileInfo(expectedFilePath);

            Assert.That(actualFile.Exists, string.Format("File was not created in {0}", actualFile.FullName));
        }

        [Test]
        public void WriteLine_can_write_to_file()
        {
            string expectedFilePath = @"test_data\file_with_content_to_write_to.txt";
            var fileToWriteTo = new FileInfo(expectedFilePath);

            fileToWriteTo.Delete();

            Assert.That(!fileToWriteTo.Exists, string.Format("File should have been deleted {0} \n but still exists", fileToWriteTo.FullName));

            _subject = new CSVWriter(expectedFilePath);

            var columns = "First Column Second column Third column";
            _subject.WriteLine(columns);
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
