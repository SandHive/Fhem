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
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public abstract class CollectionItemViewModel : BindableBase
    {
        //---------------------------------------------------------------------
        #region Fields

        private bool  m_isSelected;

        private bool  m_isVisible = true;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the header.
        /// </summary>
        public object Header { get; protected set; }

        /// <summary>
        /// Gets or sets a flag that specifies whether this content view model
        /// is currently selected and should be shown in the content region.
        /// </summary>
        public bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                if( this.SetProperty( ref m_isSelected, value ) )
                {
                    if( m_isSelected )
                    {
                        //-- Give an inherited class the chance to react on the selection
                        this.OnSelected();
                    }
                }                
            }
        }

        /// <summary>
        /// Gets a flag that indicates whether the attributes of the Fhem 
        /// object are visible.
        /// </summary>
        public bool IsVisible
        {
            get { return m_isVisible; }
            set
            {
                this.SetProperty( ref m_isVisible, value );
            }
        }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Invoked when the 'IsSelected' property is set to 'True'.
        /// </summary>
        protected virtual void OnSelected() { }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------