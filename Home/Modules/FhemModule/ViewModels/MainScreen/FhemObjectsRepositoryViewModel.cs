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
using Sand.Fhem.Home.Basics.Services;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
using System.Collections.Generic;
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

        private IApplicationService  m_applicationService;

        private ObservableCollection<FhemObjectViewModel>  m_fhemObjectsCollection = new ObservableCollection<FhemObjectViewModel>();

        private Dictionary<FhemObject, FhemObjectViewModel>  m_fhemObjectViewModelsByFhemObjects = new Dictionary<FhemObject, FhemObjectViewModel>();

        private FhemObjectsRepository  m_fhemObjectsRepository;

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
        public FhemObjectsRepositoryViewModel( IFhemService a_fhemService, IRegionManager a_regionManager, IApplicationService a_applicationService )
            : base( a_fhemService )
        {
            //-- Initialize fields
            m_applicationService = a_applicationService;
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
            if( !m_applicationService.UserInterfaceDispatcher.CheckAccess() )
            {
                m_applicationService.UserInterfaceDispatcher.BeginInvoke( new EventHandler( FhemClient_IsConnectedChanged ), sender, e );
            }
            else
            {
                if( this.FhemService.FhemClient.IsConnected )
                {
                    //-- Get the Fhem object repository 
                    m_fhemObjectsRepository = new FhemObjectsRepository( this.FhemService.FhemClient );

                    //-- First of all register to the 'CollectionChanged' event in
                    //-- order to get all updates
                    m_fhemObjectsRepository.CollectionChanged += m_fhemObjectRepository_CollectionChanged;

                    lock( this )
                    {
                        foreach( FhemObject fhemObject in m_fhemObjectsRepository )
                        {
                            this.HandleNewFhemObject( fhemObject, this.FhemService, m_regionManager );
                        }

                        //-- Sort the Fhem objects by their names
                        this.FhemObjectsCollectionView.SortDescriptions.Add( new SortDescription( "Name", ListSortDirection.Ascending ) );
                    }

                    //-- Force a property update
                    //this.OnPropertyChanged( "FhemObjectsCollectionView" );
                }
            }
        }

        private void m_fhemObjectRepository_CollectionChanged( object sender, NotifyCollectionChangedEventArgs e )
        {
            if( !m_applicationService.UserInterfaceDispatcher.CheckAccess() )
            {
                m_applicationService.UserInterfaceDispatcher.BeginInvoke( new NotifyCollectionChangedEventHandler( m_fhemObjectRepository_CollectionChanged ), sender, e );
            }
            else
            {
                lock( this )
                {
                    if( e.OldItems != null )
                    {
                        foreach( FhemObject fhemObject in e.OldItems )
                        {
                            this.HandleRemovedFhemObject( fhemObject );
                        }
                    }

                    if( e.NewItems != null )
                    {
                        foreach( FhemObject fhemObject in e.NewItems )
                        {
                            this.HandleNewFhemObject( fhemObject, this.FhemService, m_regionManager );
                        }
                    }
                }
            }
        }

        //-- Event Handlers
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        private void HandleNewFhemObject( FhemObject a_fhemObject, IFhemService a_fhemService, IRegionManager a_regionManager )
        {
            //-- First of all create a view model for the Fhem object
            var fhemObjectViewModel = FhemObjectViewModel.Create( a_fhemObject, a_fhemService, a_regionManager, m_applicationService );

            //-- Add the view model to the public collection
            m_fhemObjectsCollection.Add( fhemObjectViewModel );

            //-- Add a link between the Fhem object and its view model to the private index
            m_fhemObjectViewModelsByFhemObjects.Add( a_fhemObject, fhemObjectViewModel );
        }

        private void HandleRemovedFhemObject( FhemObject a_fhemObject )
        {
            if( m_fhemObjectViewModelsByFhemObjects.ContainsKey( a_fhemObject ) )
            {
                //-- Determine the corresponding view model
                var fhemObjectViewModel = m_fhemObjectViewModelsByFhemObjects[a_fhemObject];

                //-- Remove the view model from the collection
                m_fhemObjectsCollection.Remove( fhemObjectViewModel );
                m_fhemObjectViewModelsByFhemObjects.Remove( a_fhemObject );
            }
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------