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
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System.Collections.ObjectModel;
using System.Linq;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels.FhemObjectScreen
{
    public class FhemObjectAttributesViewModel : FhemViewModelBase
    {
        //---------------------------------------------------------------------
        #region Properties

        public ObservableCollection<FhemItemValuePairViewModel> AttributeViewModels { get; } = new ObservableCollection<FhemItemValuePairViewModel>();

        public ObservableCollection<FhemItemValuePairViewModel> PossibleAttributeViewModels { get; } = new ObservableCollection<FhemItemValuePairViewModel>();

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObjectAttributesViewModel class.
        /// </summary>
        /// <param name="a_fhemService"></param>
        public FhemObjectAttributesViewModel( IFhemService a_fhemService )
            : base( a_fhemService )
        {
            //-- Register to events
            a_fhemService.SelectedFhemObjectViewModelChanged += ( sender, e ) => this.Initialize();

            //-- For the case that there exists already a selected Fhem object view model
            this.Initialize();            
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        private void Initialize()
        {
            if( this.FhemService.SelectedFhemObjectViewModel != null )
            {
                //-- Initialize the possible attributes
                this.PossibleAttributeViewModels.Clear();
                foreach( var fhemItemValuePair in this.FhemService.SelectedFhemObjectViewModel.FhemObject.PossibleAttributes )
                {
                    this.PossibleAttributeViewModels.Add( new FhemItemValuePairViewModel( fhemItemValuePair ) );
                }

                //-- Initialize the active attributes
                this.AttributeViewModels.Clear();
                foreach( var keyValuePair in this.FhemService.SelectedFhemObjectViewModel.FhemObject.Attributes )
                {
                    var attributeName = keyValuePair.Key;
                    var attributeValue = keyValuePair.Value;

                    //--
                    var fhemItemValuePairViewModel = this.PossibleAttributeViewModels.Single( itemViewModel => itemViewModel.Item.Name == attributeName );
                    fhemItemValuePairViewModel.SelectedValue = attributeValue;

                    this.AttributeViewModels.Add( fhemItemValuePairViewModel );
                }
            }
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------