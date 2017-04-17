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
using Prism.Mvvm;
using Sand.Fhem.Basics;
using Sand.Fhem.Home.Modules.FhemModule.ViewModels;
using System;
using Sand.Fhem.Home.Modules.FhemModule.Model;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.Services
{
    public class FhemService : BindableBase, IFhemService
    {
        //---------------------------------------------------------------------
        #region Fields

        private FhemObjectViewModel  m_selectedFhemObject;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region IFhemService Members

        public FhemClient FhemClient { get; } = new FhemClient();

        public event EventHandler<FhemObjectViewModelEventArgs> FhemObjectNameEditingEnd;

        public event EventHandler<FhemObjectViewModelEventArgs> FhemObjectNameEditingStart;
        
        public FhemObjectViewModel SelectedFhemObjectViewModel
        {
            get { return m_selectedFhemObject; }
            set
            {
                if( m_selectedFhemObject != null )
                {
                    if( m_selectedFhemObject.IsNameEditable )
                    {
                        //-- Remove the 'IsNameEditable' flag of the previous selected Fhem object
                        m_selectedFhemObject.IsNameEditable = false;
                    }
                }

                if( this.SetProperty( ref m_selectedFhemObject, value ) )
                {
                    this.SelectedFhemObjectViewModelChanged?.Invoke( this, EventArgs.Empty );
                }
            }
        }

        public event EventHandler SelectedFhemObjectViewModelChanged;

        //-- IFhemService Members
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Raises the 'FhemObjectNameEditingEnd' event.
        /// </summary>
        /// <param name="a_fhemObjectViewModel">
        /// The affected <see cref="FhemObjectViewModel"/>.
        /// </param>
        internal void RaiseFhemObjectNameEditingEndEvent( FhemObjectViewModel a_fhemObjectViewModel )
        {
            this.FhemObjectNameEditingEnd?.Invoke( this, new FhemObjectViewModelEventArgs( a_fhemObjectViewModel ) );
        }

        /// <summary>
        /// Raises the 'FhemObjectNameEditingStart' event.
        /// </summary>
        /// <param name="a_fhemObjectViewModel">
        /// The affected <see cref="FhemObjectViewModel"/>.
        /// </param>
        internal void RaiseFhemObjectNameEditingStartEvent( FhemObjectViewModel a_fhemObjectViewModel )
        {
            this.FhemObjectNameEditingStart?.Invoke( this, new FhemObjectViewModelEventArgs( a_fhemObjectViewModel ) );
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------