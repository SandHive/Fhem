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
using System.Collections.ObjectModel;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels.FhemObjectScreen
{
    public class FhemObjectNavigationViewModel : FhemViewModelBase, INavigationAware
    {
        //---------------------------------------------------------------------
        #region Fields

        private MenuItemViewModel  m_attributesMenuItemViewModel;

        private MenuItemViewModel  m_internalsMenuItemViewModel;

        private MenuItemViewModel  m_possibleSetsMenuItemViewModel;

        private MenuItemViewModel  m_readingsMenuItemViewModel;

        private IRegionManager  m_regionManager;
            
        private MenuItemViewModel  m_selectedItem;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the menu entry view models for the Fhem object details.
        /// </summary>
        public ObservableCollection<MenuItemViewModel> MenuEntriesViewModels { get; } = new ObservableCollection<MenuItemViewModel>();

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public MenuItemViewModel SelectedItem
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
            : base( a_fhemService )
        {
            //-- Initialize fields
            m_regionManager = a_regionManager;
            m_attributesMenuItemViewModel = new MenuItemViewModel(

                "Attributes",
                new DelegateCommand( () => a_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectAttributesView", UriKind.Relative ) ) )
            );
            m_internalsMenuItemViewModel = new MenuItemViewModel(

                "Internals",
                new DelegateCommand( () => a_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectInternalsView", UriKind.Relative ) ) )
            );
            m_possibleSetsMenuItemViewModel = new MenuItemViewModel(

                "Possible Sets",
                new DelegateCommand( () => a_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectPossibleSetsView", UriKind.Relative ) ) )
            );
            m_readingsMenuItemViewModel = new MenuItemViewModel(

                "Readings",
                new DelegateCommand( () => a_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectReadingsView", UriKind.Relative ) ) )
            );

            //-- Add all navigation view models to our list
            this.MenuEntriesViewModels.Add( m_attributesMenuItemViewModel );
            this.MenuEntriesViewModels.Add( m_internalsMenuItemViewModel );
            this.MenuEntriesViewModels.Add( m_possibleSetsMenuItemViewModel );
            this.MenuEntriesViewModels.Add( m_readingsMenuItemViewModel );
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
            m_attributesMenuItemViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsAttributes;
            m_internalsMenuItemViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsInternals;
            m_possibleSetsMenuItemViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsPossibleSets;
            m_readingsMenuItemViewModel.IsVisible = this.FhemService.SelectedFhemObject.ContainsReadings;

            //-- Select and navigate always to the first visible navigation view model
            if( m_attributesMenuItemViewModel.IsVisible )
            {
                this.SelectedItem = m_attributesMenuItemViewModel;
                m_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectAttributesView", UriKind.Relative ) );
            }
            else if( m_internalsMenuItemViewModel.IsVisible )
            {
                this.SelectedItem = m_internalsMenuItemViewModel;
                m_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectInternalsView", UriKind.Relative ) );
            }
            else if( m_possibleSetsMenuItemViewModel.IsVisible )
            {
                this.SelectedItem = m_possibleSetsMenuItemViewModel;
                m_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectPossibleSetsView", UriKind.Relative ) );
            }
            else if( m_readingsMenuItemViewModel.IsVisible )
            {
                this.SelectedItem = m_readingsMenuItemViewModel;
                m_regionManager.RequestNavigate( "ContentRegion", new Uri( "FhemObjectReadingsView", UriKind.Relative ) );
            }
        }

        //-- INavigationAware Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------