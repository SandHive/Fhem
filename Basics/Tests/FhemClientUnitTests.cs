using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics.Tests
{
    [TestClass]
    public class FhemClientUnitTests
    {
        //---------------------------------------------------------------------
        #region Tests

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void FhemClient_RenameFhemObject_FhemObjectArgumentEmptyTest()
        {
            var fhemClient = new FhemClient( null, null );

            fhemClient.RenameFhemObject( null, "Palim" );
        }
        
        //-- Tests
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------