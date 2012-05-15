using BinNet.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BinNetTest
{
    
    
    /// <summary>
    ///This is a test class for SignedByteConverterTest and is intended
    ///to contain all SignedByteConverterTest Unit Tests
    ///</summary>
    [TestClass()]
    public class SignedByteConverterTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for Convert
        ///</summary>
        [TestMethod()]
        public void ConvertTest()
        {
            SignedDecimalConverter target = new SignedDecimalConverter(); 
            byte[] data = new byte[] { 0x9f, 0x01 }; 
            string expected = "[ -97, 1, ]"; 
            string actual;
            actual = target.Convert(data);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for ConvertBack
        ///</summary>
        [TestMethod()]
        public void ConvertBackTest()
        {
            SignedDecimalConverter target = new SignedDecimalConverter();
            string source = "[ -97, 1 ]"; 
            byte[] expected = new byte[] {0x9F, 0x01}; 
            byte[] actual;
            actual = target.ConvertBack(source);
            Assert.AreEqual(actual.Length, 2);
            Assert.AreEqual(expected[0], actual[0]);
            Assert.AreEqual(expected[1], actual[1]);
        }
    }
}
