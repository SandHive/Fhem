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
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public struct FhemClientResponse
    {
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets information about the error that has occurred.
        /// </summary>
        public string ErrorInformation { get; private set; }

        /// <summary>
        /// Gets a flag that specifies whether the response is marked as failed.
        /// </summary>
        public bool IsFailed { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemClientResponse struct.
        /// </summary>
        /// <param name="a_errorInformation">
        /// The information about the error that has occurred.
        /// </param>
        public FhemClientResponse( string a_errorInformation )
        {
            //-- Initialize properties
            this.IsFailed = true;
            this.ErrorInformation = a_errorInformation;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------