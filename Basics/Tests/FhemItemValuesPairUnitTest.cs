using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics.Tests
{
    [TestClass]
    public class FhemItemValuesPairUnitTest
    {
        //---------------------------------------------------------------------
        #region Tests

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void FhemItemValuesPair_Parse_ArgumentEmptyTest()
        {
            FhemItemValuesPair.Parse( String.Empty );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void FhemItemValuesPair_Parse_ArgumentNullTest()
        {
            FhemItemValuesPair.Parse( null );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentException ) )]
        public void FhemItemValuesPair_Parse_InvalidParseStringTest()
        {
            var parseString = "TestItem:Value1:Value2:Value3";

            FhemItemValuesPair.Parse( parseString );
        }

        [TestMethod]
        public void FhemItemValuesPair_Parse_ValidityTest()
        {
            var parseString = "TestItem:Value1,Value2,Value3";
            
            var itemValuesPair = FhemItemValuesPair.Parse( parseString );

            //-- Make some asserts
            Assert.AreEqual<string>( "TestItem", itemValuesPair.Name );
            Assert.AreEqual<int>( 3, itemValuesPair.Values.Count );
            Assert.AreEqual<string>( "Value1", itemValuesPair.Values[0] );
            Assert.AreEqual<string>( "Value2", itemValuesPair.Values[1] );
            Assert.AreEqual<string>( "Value3", itemValuesPair.Values[2] );
        }

        //-- Tests
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------