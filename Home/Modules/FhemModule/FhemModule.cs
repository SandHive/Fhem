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
using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using Sand.Fhem.Home.Modules.FhemModule.Views;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule
{
    public class FhemModule : IModule
    {
        //---------------------------------------------------------------------
        #region Fields

        private FhemClientService  m_fhemClientService = new FhemClientService();

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
            a_container.RegisterInstance<IFhemClientService>( m_fhemClientService );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region IModule Members

        public void Initialize()
        {
            m_regionViewRegistry.RegisterViewWithRegion( "FhemServerSettingsRegion", typeof( FhemServerSettingsView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "MenuRegion", typeof( FhemMenuView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemObjectsView ) );
            m_regionViewRegistry.RegisterViewWithRegion( "ContentRegion", typeof( FhemNativeCommandView ) );
        }

        //-- IModule Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------