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
using Prism.Unity;
using Sand.Fhem.Home.Basics.Services;
using System.Windows;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Prism.FhemAgent
{
    internal class FhemHomeBootstrapper : UnityBootstrapper
    {
        //---------------------------------------------------------------------
        #region Fields

        private IApplicationService  m_applicationService;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region UnityBootstrapper Members

        protected override void ConfigureContainer()
        {
            base.ConfigureContainer();

            //-- Create services
            m_applicationService = new ApplicationService();

            //-- Register services
            this.Container.RegisterInstance<IApplicationService>( m_applicationService );
        }

        protected override void ConfigureModuleCatalog()
        {
            base.ConfigureModuleCatalog();

            this.ModuleCatalog.AddModule( new ModuleInfo( "FhemModule", "Sand.Fhem.Home.Modules.FhemModule.FhemModule, Sand.Fhem.Home.Modules.FhemModule" ) );
        }

        protected override DependencyObject CreateShell()
        {
            return new FhemHomeShell();
        }

        protected override void InitializeShell()
        {
            FhemHomeApplication.Current.MainWindow = (Window) this.Shell;
            FhemHomeApplication.Current.MainWindow.Show();
        }

        //-- UnityBootstrapper Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------