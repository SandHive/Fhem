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
using Microsoft.Practices.ServiceLocation;
using Sand.Fhem.Home.Modules.FhemModule.Model;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.Behaviors
{
    /// <summary>
    /// Manages the focus handling after an Fhem object name editing. 
    /// </summary>
    /// <remarks>
    /// When editing a Fhem object name, the name will be shown in a TextBox 
    /// and the focus is set from The ListViewItem to the TextBox. When editing
    /// ends, thw focus will be set to the ListViewItem again by this behavior.
    /// </remarks>
    public class RestoreFocusOnListViewItemBehavior : Behavior<ListView>
    {
        //---------------------------------------------------------------------
        #region Fields

        private IFhemService  m_fhemService;

        private IInputElement  m_lastFocusedElementBeforeRenaming;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Event Handling

        private void FhemService_FhemObjectNameEditingEnd( object sender, FhemObjectViewModelEventArgs e )
        {
            //-- Restore the focus
            m_lastFocusedElementBeforeRenaming?.Focus();
        }
        
        private void FhemService_FhemObjectNameEditingStart( object sender, FhemObjectViewModelEventArgs e )
        {
            //-- Keep the last focused element in mind
            m_lastFocusedElementBeforeRenaming = Keyboard.FocusedElement;
        }

        //-- Event Handling
        #endregion
        //---------------------------------------------------------------------
        #region Behavior Members

        protected override void OnAttached()
        {
            base.OnAttached();

            //-- Determine the Fhem service
            m_fhemService = (IFhemService) ServiceLocator.Current.GetService( typeof( IFhemService ) );

            //-- Register to events
            m_fhemService.FhemObjectNameEditingEnd += FhemService_FhemObjectNameEditingEnd;
            m_fhemService.FhemObjectNameEditingStart += FhemService_FhemObjectNameEditingStart;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            //-- Unregister events again
            m_fhemService.FhemObjectNameEditingEnd -= FhemService_FhemObjectNameEditingEnd;
            m_fhemService.FhemObjectNameEditingStart -= FhemService_FhemObjectNameEditingStart;
        }

        //-- Behavior Members
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------