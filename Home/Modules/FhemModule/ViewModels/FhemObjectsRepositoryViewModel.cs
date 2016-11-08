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
using Sand.Fhem.Basics;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
using System.ComponentModel;
using System.Windows.Data;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public class FhemObjectsRepositoryViewModel : FhemViewModelBase
    {
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the command for editing the Fhem object name.
        /// </summary>
        public DelegateCommand EditFhemObjectNameCommand { get; private set; }

        /// <summary>
        /// Gets the Fhem objects repository.
        /// </summary>
        public ICollectionView FhemObjectsRepository { get; private set; }

        /// <summary>
        /// Gets the command for opening the details of a Fhem object.
        /// </summary>
        public DelegateCommand OpenFhemObjectDetailsCommand { get; private set; }
        
        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObjectsRepositoryViewModel 
        /// class.
        /// </summary>
        public FhemObjectsRepositoryViewModel( IFhemService a_fhemService, IRegionManager a_regionManager )
            : base( a_fhemService, a_regionManager )
        {
            //-- Initialize properties
            this.Header = "Show Fhem Objects";

            //-- Initialize commands
            this.EditFhemObjectNameCommand = new DelegateCommand( this.EditFhemObjectNameCommandAction );
            this.OpenFhemObjectDetailsCommand = new DelegateCommand( this.OpenFhemObjectDetailsCommandAction );

            //-- Register to events
            this.FhemService.FhemClient.IsConnectedChanged += FhemClient_IsConnectedChanged;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Event Handlers

        private void FhemClient_IsConnectedChanged( object sender, EventArgs e )
        {
            if( this.FhemService.FhemClient.IsConnected )
            {
                //-- Use the Fhem object repository as source for the collection view 
                this.FhemObjectsRepository = CollectionViewSource.GetDefaultView( this.FhemService.FhemObjectRepository );

                //-- Sort the Fhem objects by their names
                this.FhemObjectsRepository.SortDescriptions.Add( new SortDescription( "Name", ListSortDirection.Ascending ) );

                //-- Force a property update
                this.OnPropertyChanged( "FhemObjectsRepository" );
            }
        }

        //-- Event Handlers
        #endregion
        //---------------------------------------------------------------------
        #region FhemContentViewModel Members

        protected override void OnSelected()
        {
            base.OnSelected();

            this.RegionManager.RequestNavigate( "ContentRegion", new System.Uri( "FhemObjectsRepositoryView", UriKind.Relative ) );
        }

        //-- FhemContentViewModel Members
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Performs the action that should be invoked by the 
        /// 'EditFhemObjectNameCommand'.
        /// </summary>
        private void EditFhemObjectNameCommandAction()
        {
            
        }

        /// <summary>
        /// Performs the action that should be invoked by the 
        /// 'OpenFhemObjectDetailsCommand'.
        /// </summary>
        private void OpenFhemObjectDetailsCommandAction()
        {
            this.RegionManager.RequestNavigate( "TitleRegion", new Uri( "FhemObjectTitleView", UriKind.Relative ) );
            this.RegionManager.RequestNavigate( "NavigationRegion", new Uri( "FhemObjectNavigationView", UriKind.Relative ) );
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------