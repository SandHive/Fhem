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

        #region 'GetFhemObject' Tests

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void FhemClient_GetFhemObject_FhemClientNotConnectedTest()
        {
            var fhemClient = new FhemClient();

            fhemClient.GetFhemObject( "Palim" );
        }

        //-- 'GetFhemObject' Tests
        #endregion

        #region 'GetFhemObjects' Tests

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void FhemClient_GetFhemObjects_FhemClientNotConnectedTest()
        {
            var fhemClient = new FhemClient();

            fhemClient.GetFhemObjects();
        }

        //-- 'GetFhemObjects' Tests
        #endregion

        #region 'RenameFhemObject' Tests

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void FhemClient_RenameFhemObject_FhemClientNotConnectedTest()
        {
            var fhemClient = new FhemClient();

            fhemClient.RenameFhemObject( null, "Palim" );
        }

        [TestMethod]
        [ExpectedException( typeof( ArgumentNullException ) )]
        public void FhemClient_RenameFhemObject_FhemObjectArgumentEmptyTest()
        {
            var fhemClient = new FhemClient( null, null );

            fhemClient.RenameFhemObject( null, "Palim" );
        }

        //-- 'RenameFhemObject' Tests
        #endregion

        #region 'SendNativeCommand' Tests

        [TestMethod]
        [ExpectedException( typeof( InvalidOperationException ) )]
        public void FhemClient_SendNativeCommand_FhemClientNotConnectedTest()
        {
            var fhemClient = new FhemClient();

            fhemClient.SendNativeCommand( "jsonlist2" );
        }

        //-- 'SendNativeCommand' Tests
        #endregion

        //-- Tests
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------