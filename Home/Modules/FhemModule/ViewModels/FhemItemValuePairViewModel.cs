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
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels.FhemObjectScreen
{
    public class FhemItemValuePairViewModel : BindableBase
    {
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets a flag that specifies whether the FhemItemValuesPair object
        /// contains multiple values.
        /// </summary>
        public bool HasMultipleValues
        {
            get
            {
                if( this.Item.Values != null )
                {
                    if( this.Item.Values.Count > 0 )
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the FhemItemValuesPair object.
        /// </summary>
        public FhemItemValuesPair Item { get; private set; }

        /// <summary>
        /// Gets or sets the selected value.
        /// </summary>
        public string SelectedValue { get; set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemItemValuePairViewModel class.
        /// </summary>
        /// <param name="fhemItemValuePair"></param>
        public FhemItemValuePairViewModel( FhemItemValuesPair a_fhemItemValuePair )
        {
            //-- Initialize properties
            this.Item = a_fhemItemValuePair;

            if( this.HasMultipleValues )
            {
                this.SelectedValue = a_fhemItemValuePair.Values[0];
            }
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------