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
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using Sand.Fhem.Home.Modules.FhemModule.Views.FhemObjectScreen;
using Sand.Fhem.Home.Modules.FhemModule.Views.MainScreen;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule
{
    public class FhemModule : IModule
    {
        //---------------------------------------------------------------------
        #region Fields

        private FhemService  m_fhemService = new FhemService();

        private IRegionViewRegistry  m_regionViewRegistry;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemModule class.
        /// </summary>
        /// <param name="a_regionViewRegistry"></param>
        public FhemModule( IUnityContainer a_container, IRegionViewRegistry a_regionViewRegistry )
        {
            //-- Initialize fields
            m_regionViewRegistry = a_regionViewRegistry;

            //-- Register services
            a_container.RegisterInstance<IFhemService>( m_fhemService );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region IModule Members

        public void Initialize()
        {
            //-- Register the views of the main screen
            m_regionViewRegistry.RegisterViewWithRegion( "TitleRegion", typeof( FhemServerSettingsView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "NavigationRegion", typeof( FhemMainNavigationView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemObjectsRepositoryView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemNativeCommandView ) );

            //-- Register the Fhem object views
            m_regionViewRegistry.RegisterViewWithRegion( "TitleRegion", typeof( FhemObjectTitleView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "NavigationRegion", typeof( FhemObjectNavigationView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemObjectAttributesView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemObjectInternalsView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemObjectPossibleSetsView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemObjectReadingsView ) );
        }

        //-- IModule Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------