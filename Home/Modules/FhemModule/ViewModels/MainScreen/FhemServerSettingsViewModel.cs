/* Copyright (c) 2016 - 2017 The Sandman (sandhive@gmail.com)
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
using Prism.Mvvm;
using Sand.Fhem.Basics;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels.MainScreen
{
    public class FhemServerSettingsViewModel : BindableBase
    {
        //---------------------------------------------------------------------
        #region Fields

        private IFhemService  m_fhemService;
        
        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the command for connecting to the Fhem server.
        /// </summary>
        public DelegateCommand ConnectCommand { get; private set; }
        
        /// <summary>
        /// Gets or sets the IP adress of the Fhem server.
        /// </summary>
        public string FhemServerIP { get; set; } = "192.168.178.50";

        /// <summary>
        /// Gets or sets the port of the Fhem server.
        /// </summary>
        public string FhemServerPort { get; set; } = "7072";

        /// <summary>
        /// Gets a flag that specifies whether a Fhem client is connected.
        /// </summary>
        public bool IsFhemClientConnected {  get { return m_fhemService.FhemClient.IsConnected; } }
        
        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemServerSettingsViewModel 
        /// class.
        /// </summary>
        public FhemServerSettingsViewModel( IFhemService a_fhemService )
        {
            //-- Initialize fields
            m_fhemService = a_fhemService;

            //-- Register to events
            m_fhemService.FhemClient.IsConnectedChanged += FhemClient_IsConnectedChanged ;

            //-- Initialize commands
            this.ConnectCommand = new DelegateCommand( () => this.ConnectCommandAction()  );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Event Handlers

        private void FhemClient_IsConnectedChanged( object sender, EventArgs e )
        {
            //-- Just force an update of the 'IsFhemClientConnected' property
            this.OnPropertyChanged( "IsFhemClientConnected" );
        }

        //-- Event Handlers
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// The action that should be performed when executing the 'ConnectCommand'.
        /// </summary>
        private void ConnectCommandAction()
        {
            //-- Connect to the Fhem server
            m_fhemService.FhemClient.Connect( this.FhemServerIP, int.Parse( this.FhemServerPort ) );
        }
        
        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------