﻿/* Copyright (c) 2016 - 2017 The Sandman (sandhive@gmail.com)
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
using Sand.Fhem.Home.Modules.FhemModule.Services;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels.FhemObjectScreen
{
    public class FhemObjectInternalsViewModel : FhemViewModelBase
    {
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObjectInternalsViewModel 
        /// class.
        /// </summary>
        /// <param name="a_fhemService"></param>
        public FhemObjectInternalsViewModel( IFhemService a_fhemService )
            : base( a_fhemService ) { }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------