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
using Prism.Regions;
using Sand.Fhem.Basics;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels.MainScreen
{
    public class FhemObjectsRepositoryViewModel : FhemViewModelBase
    {
        //---------------------------------------------------------------------
        #region Fields

        private ObservableCollection<FhemObjectViewModel>  m_fhemObjectsCollection = new ObservableCollection<FhemObjectViewModel>();

        private IRegionManager  m_regionManager;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the Fhem objects collection view.
        /// </summary>
        public ICollectionView FhemObjectsCollectionView { get; private set; }
        
        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObjectsRepositoryViewModel 
        /// class.
        /// </summary>
        public FhemObjectsRepositoryViewModel( IFhemService a_fhemService, IRegionManager a_regionManager )
            : base( a_fhemService )
        {
            //-- Initialize fields
            m_regionManager = a_regionManager;

            //-- Initialize properties
            this.FhemObjectsCollectionView = CollectionViewSource.GetDefaultView( m_fhemObjectsCollection );
            
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
                //-- First of all register to the 'CollectionChanged' event in
                //-- order to get all updates
                this.FhemService.FhemObjectRepository.CollectionChanged += FhemObjectRepository_CollectionChanged;

                lock( this )
                {
                    foreach( FhemObject fhemObject in this.FhemService.FhemObjectRepository )
                    {
                        m_fhemObjectsCollection.Add( FhemObjectViewModel.Create( fhemObject, this.FhemService, m_regionManager ) );
                    }

                    //-- Sort the Fhem objects by their names
                    this.FhemObjectsCollectionView.SortDescriptions.Add( new SortDescription( "Name", ListSortDirection.Ascending ) );
                }

                //-- Force a property update
                this.OnPropertyChanged( "FhemObjectsCollectionView" );
            }
        }

        private void FhemObjectRepository_CollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            lock( this )
            {
                if( e.OldItems != null )
                {
                    foreach( FhemObjectViewModel fhemObjectViewModel in e.OldItems )
                    {
                        m_fhemObjectsCollection.Remove( fhemObjectViewModel );
                    }
                }

                if( e.NewItems != null )
                {
                    foreach( FhemObjectViewModel fhemObjectViewModel in e.NewItems )
                    {
                        m_fhemObjectsCollection.Add( fhemObjectViewModel );
                    }
                }
            }
        }

        //-- Event Handlers
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------