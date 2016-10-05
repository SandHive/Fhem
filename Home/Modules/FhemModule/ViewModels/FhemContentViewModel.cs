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
using Prism.Mvvm;
using Prism.Regions;
using Sand.Fhem.Home.Modules.FhemModule.Services;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public abstract class FhemContentViewModel : BindableBase
    {
        //---------------------------------------------------------------------
        #region Fields

        private bool  m_isActive;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the Fhem client service.
        /// </summary>
        protected IFhemClientService FhemClientService { get; private set; }

        /// <summary>
        /// Gets the header.
        /// </summary>
        public abstract string Header { get; }

        /// <summary>
        /// Gets or sets a flag that specifies whether this content view model
        /// is currently active and should be shown in the content region.
        /// </summary>
        public bool IsActive
        {
            get
            {
                return m_isActive;
            }
            set
            {
                //-- Do nothing when the value has not changed
                if( m_isActive == value ) { return; }

                //-- Update the value
                m_isActive = value;

                if( m_isActive )
                {
                    //-- Give an inherited class the chance to react on
                    this.OnActivated();
                }
            }
        }

        /// <summary>
        /// Gets the region manager.
        /// </summary>
        protected IRegionManager RegionManager { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemContentViewModel class.
        /// </summary>
        public FhemContentViewModel( IFhemClientService a_fhemClientService, IRegionManager a_regionManager )
        {
            //-- Initialize properties
            this.FhemClientService = a_fhemClientService;
            this.RegionManager = a_regionManager;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Invoked when the 'IsActive' is set to 'True'.
        /// </summary>
        protected virtual void OnActivated() { }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------