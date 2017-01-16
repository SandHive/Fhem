﻿/* Copyright (c) 2016 The Sandman (sandhive@gmail.com)
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
using Sand.Fhem.Basics;
using Sand.Fhem.Home.Modules.FhemModule.Model;
using Sand.Fhem.Home.Modules.FhemModule.ViewModels;
using System;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.Services
{
    public interface IFhemService
    {
        /// <summary>
        /// Gets the Fhem client.
        /// </summary>
        FhemClient FhemClient { get; }

        /// <summary>
        /// Occurs when the editing of the Fhem object name ends.
        /// </summary>
        event EventHandler<FhemObjectViewModelEventArgs> FhemObjectNameEditingEnd;

        /// <summary>
        /// Occurs when the editing of the Fhem object name starts.
        /// </summary>
        event EventHandler<FhemObjectViewModelEventArgs> FhemObjectNameEditingStart;
        
        /// <summary>
        /// Gets or sets the selected Fhem object view model.
        /// </summary>
        FhemObjectViewModel SelectedFhemObject { get; set; }
    }
}
//-----------------------------------------------------------------------------