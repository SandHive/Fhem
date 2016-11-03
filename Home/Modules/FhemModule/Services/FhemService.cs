/* Copyright (c) 2016 The Sandman (sandhive@gmail.com)
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy 
 * of this software and associated documentation files (the "Software"), to 
 * deal in the Software without restriction, including without limitation the 
 * rights to use, copy, modify, merge, publish, distribute, sublicense, and/or 
 * sell copies of the Software, and to permit persons to whom the Software is 
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in 
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING 
 * FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS 
 * IN THE SOFTWARE.
 */
using Prism.Mvvm;
using Sand.Fhem.Basics;
using System;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.Services
{
    public class FhemService : BindableBase, IFhemService
    {
        //---------------------------------------------------------------------
        #region Fields

        private FhemObject  m_selectedFhemObject;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemService class.
        /// </summary>
        public FhemService()
        {
            //-- Register to events
            this.FhemClient.IsConnectedChanged += FhemClient_IsConnectedChanged;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Event Handlers

        private void FhemClient_IsConnectedChanged( object sender, EventArgs e )
        {
            if( this.FhemClient.IsConnected )
            {
                //-- Get the Fhem object repository 
                this.FhemObjectRepository = this.FhemClient.GetObjectRepository();
            }
        }

        //-- Event Handlers
        #endregion
        //---------------------------------------------------------------------
        #region IFhemService Members

        public FhemClient FhemClient { get; } = new FhemClient();

        public FhemObjectsRepository FhemObjectRepository { get; private set; }

        public FhemObject SelectedFhemObject
        {
            get { return m_selectedFhemObject; }
            set { this.SetProperty( ref m_selectedFhemObject, value ); }
        }

        //-- IFhemService Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------