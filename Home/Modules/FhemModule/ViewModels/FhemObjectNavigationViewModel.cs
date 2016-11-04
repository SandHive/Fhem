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
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public class FhemObjectNavigationViewModel : FhemNavigationViewModelBase, INavigationAware
    {
        //---------------------------------------------------------------------
        #region Fields

        private FhemObjectAttributesViewModel  m_fhemObjectAttributesViewModel;

        private FhemObjectInternalsViewModel  m_fhemObjectInternalsViewModel;

        private FhemObjectPossibleSetsViewModel  m_fhemObjectPossibleSetsViewModel;

        private FhemObjectReadingsViewModel  m_fhemObjectReadingsViewModel;

        private int  m_selectedIndex;

        private FhemViewModelBase  m_selectedItem;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public FhemViewModelBase SelectedItem
        {
            get { return m_selectedItem; }
            set
            {
                this.SetProperty( ref m_selectedItem, value );
            }
        }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemMainNavigationViewModel class.
        /// </summary>
        public FhemObjectNavigationViewModel( IFhemService a_fhemService, IRegionManager a_regionManager )
            : base( a_fhemService, a_regionManager )
        {
            //-- Initialize fields
            m_fhemObjectAttributesViewModel = new FhemObjectAttributesViewModel( a_fhemService, a_regionManager );
            m_fhemObjectInternalsViewModel = new FhemObjectInternalsViewModel( a_fhemService, a_regionManager );
            m_fhemObjectPossibleSetsViewModel = new FhemObjectPossibleSetsViewModel( a_fhemService, a_regionManager );
            m_fhemObjectReadingsViewModel = new FhemObjectReadingsViewModel( a_fhemService, a_regionManager );

            //-- Add all navigation view models to our list
            this.NavigationViewModels.Add( m_fhemObjectAttributesViewModel );
            this.NavigationViewModels.Add( m_fhemObjectInternalsViewModel );
            this.NavigationViewModels.Add( m_fhemObjectPossibleSetsViewModel );
            this.NavigationViewModels.Add( m_fhemObjectReadingsViewModel );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region INavigationAware Members

        public bool IsNavigationTarget( NavigationContext navigationContext )
        {
            return true;
        }

        public void OnNavigatedFrom( NavigationContext navigationContext )
        {
            
        }

        public void OnNavigatedTo( NavigationContext navigationContext )
        {
            //-- Update the 'IsVisible' flag of all navigation view models
            m_fhemObjectAttributesViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsAttributes;
            m_fhemObjectInternalsViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsInternals;
            m_fhemObjectPossibleSetsViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsPossibleSets;
            m_fhemObjectReadingsViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsReadings;

            //-- Select and navigate always to the first visible navigation view model
            if( m_fhemObjectAttributesViewModel.IsVisible )
            {
                this.SelectedItem = m_fhemObjectAttributesViewModel;
                this.RegionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectAttributesView", UriKind.Relative ) );
            }
            else if( m_fhemObjectInternalsViewModel.IsVisible )
            {
                this.SelectedItem = m_fhemObjectInternalsViewModel;
                this.RegionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectInternalsView", UriKind.Relative ) );
            }
            else if( m_fhemObjectPossibleSetsViewModel.IsVisible )
            {
                this.SelectedItem = m_fhemObjectPossibleSetsViewModel;
                this.RegionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectPossibleSetsView", UriKind.Relative ) );
            }
            else if( m_fhemObjectReadingsViewModel.IsVisible )
            {
                this.SelectedItem = m_fhemObjectReadingsViewModel;
                this.RegionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectReadingsView", UriKind.Relative ) );
            }
        }

        //-- INavigationAware Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------