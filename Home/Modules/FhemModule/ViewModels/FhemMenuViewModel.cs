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
using Prism.Mvvm;
using Sand.Fhem.Basics;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
using System.ComponentModel;
using System.Windows.Data;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public class FhemMenuViewModel : BindableBase
    {
        //---------------------------------------------------------------------
        #region Fields

        private IFhemClientService  m_fhemClientService;
        
        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the repository of all Fhem objects.
        /// </summary>
        public ICollectionView FhemObjects { get; private set; }
        
        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemMenuViewModel class.
        /// </summary>
        public FhemMenuViewModel( IFhemClientService a_fhemClientService )
        {
            //-- Initialize fields
            m_fhemClientService = a_fhemClientService;

            //-- Register to events
            m_fhemClientService.FhemClient.IsConnectedChanged += FhemClient_IsConnectedChanged ;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Event Handlers

        private void FhemClient_IsConnectedChanged( object sender, EventArgs e )
        {
            if( m_fhemClientService.FhemClient.IsConnected )
            {
                //-- Get the Fhem object repository 
                var fhemObjectRepository = m_fhemClientService.FhemClient.GetObjectRepository();

                //-- Use the Fhem object repository as source for the collection view 
                this.FhemObjects = CollectionViewSource.GetDefaultView( fhemObjectRepository );

                //-- Sort the Fhem objects by their names
                this.FhemObjects.SortDescriptions.Add( new SortDescription( "Name", ListSortDirection.Ascending ) );

                //-- Force a property update
                this.OnPropertyChanged( "FhemObjects" );
            }
        }

        //-- Event Handlers
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------