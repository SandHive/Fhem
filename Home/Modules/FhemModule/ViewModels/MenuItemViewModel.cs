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
using System.Windows.Input;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public class MenuItemViewModel : BindableBase
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
        public object Header { get; private set; }

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
                        //-- 
                        if( this.ClickCommand.CanExecute( null ) )
                        {
                            this.ClickCommand.Execute( null );
                        }
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

        /// <summary>
        /// Gets the command that is executed when the menu item is clicked.
        /// </summary>
        public ICommand ClickCommand { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the MenuItemViewModel class.
        /// </summary>
        /// <param name="a_header">
        /// The header.
        /// </param>
        /// <param name="a_clickCommand">
        /// The command that is executed when the menu item is clicked.
        /// </param>
        public MenuItemViewModel( object a_header, ICommand a_clickCommand )
        {
            //-- Initialize properties
            this.Header = a_header;
            this.ClickCommand = a_clickCommand;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------