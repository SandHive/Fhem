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
using Prism.Commands;
using Prism.Regions;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public class FhemNativeCommandViewModel : FhemContentViewModel
    {
        //---------------------------------------------------------------------
        #region Fields

        private string  m_fhemResponse;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets or sets the native command string. 
        /// </summary>
        public string NativeCommandString { get; set; }
        
        /// <summary>
        /// Gets the response of the native command string.
        /// </summary>
        public string FhemResponse
        {
            get { return m_fhemResponse; }
            private set
            {
                //-- Check that the value has really changed
                if( value == m_fhemResponse ) { return; }

                //-- Apply value
                m_fhemResponse = value;

                //-- Propagate the change
                this.OnPropertyChanged();
            }
        }
        
        /// <summary>
        /// Gets the command for sending a native command string.
        /// </summary>
        public DelegateCommand SendNativeCommandStringCommand { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemNativeCommandViewModel class.
        /// </summary>
        public FhemNativeCommandViewModel( IFhemClientService a_fhemClientService, IRegionManager a_regionManager )
            : base( a_fhemClientService, a_regionManager )
        {
            //-- Initialize properties
            this.Header = "Send Native Command";

            //-- Initialize commands
            this.SendNativeCommandStringCommand = new DelegateCommand( () => this.SendNativeCommandStringCommandAction() );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region FhemContentViewModel Members

        protected override void OnSelected()
        {
            base.OnSelected();
        
            this.RegionManager.RequestNavigate( "ContentRegion", new System.Uri( "FhemNativeCommandView", UriKind.Relative ) );
        }

        //-- FhemContentViewModel Members
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// The action that should be performed when executing the 'SendNativeCommandStringCommand'.
        /// </summary>
        private void SendNativeCommandStringCommandAction()
        {
            this.FhemResponse = this.FhemClientService.FhemClient.SendNativeCommand( this.NativeCommandString );
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------