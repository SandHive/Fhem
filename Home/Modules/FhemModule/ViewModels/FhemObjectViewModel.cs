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
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Sand.Fhem.Basics;
using System;
using System.Collections.ObjectModel;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public class FhemObjectViewModel : BindableBase
    {
        //---------------------------------------------------------------------
        #region Constants

        private const string STATE_TAG = "STATE";

        //-- Constants
        #endregion
        //---------------------------------------------------------------------
        #region Fields

        private bool  m_isNameEditable;

        private IRegionManager  m_regionManager;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the attributes of the Fhem object.
        /// </summary>
        public ReadOnlyDictionary<string, string> Attributes { get { return this.FhemObject.Attributes; } }

        /// <summary>
        /// Gets a flag that specifies whether the Fhem object contains 
        /// attributes.
        /// </summary>
        public bool ContainsAttributes
        {
            get { return this.FhemObject.Attributes.Count > 0; }
        }

        /// <summary>
        /// Gets a flag that specifies whether the Fhem object contains 
        /// internals.
        /// </summary>
        public bool ContainsInternals
        {
            get { return this.FhemObject.Internals.Count > 0; }
        }

        /// <summary>
        /// Gets a flag that specifies whether the Fhem object contains 
        /// possible sets.
        /// </summary>
        public bool ContainsPossibleSets
        {
            get { return this.FhemObject.PossibleSets.Length > 0; }
        }

        /// <summary>
        /// Gets a flag that specifies whether the Fhem object contains 
        /// readings.
        /// </summary>
        public bool ContainsReadings
        {
            get { return this.FhemObject.Readings.Count > 0; }
        }

        /// <summary>
        /// Gets the command for editing the Fhem object name.
        /// </summary>
        public DelegateCommand EditFhemObjectNameCommand { get; private set; }

        /// <summary>
        /// Gets the underlying <see cref="FhemObject"/>.
        /// </summary>
        internal FhemObject FhemObject { get; private set; }

        /// <summary>
        /// Gets the internals of the Fhem object.
        /// </summary>
        public ReadOnlyDictionary<string, string> Internals { get { return this.FhemObject.Internals; } }

        /// <summary>
        /// Gets a flag that indicates whether the name is currently editable.
        /// </summary>
        public bool IsNameEditable
        {
            get { return m_isNameEditable; }
            private set
            {
                this.SetProperty( ref m_isNameEditable, value );
            }
        }

        /// <summary>
        /// Gets the name of the Fhem object.
        /// </summary>
        public string Name
        {
            get { return this.FhemObject.Name; }
            set
            {

            }
        }

        /// <summary>
        /// Gets the command for opening the details of a Fhem object.
        /// </summary>
        public DelegateCommand OpenFhemObjectDetailsCommand { get; private set; }

        /// <summary>
        /// Gets the possible attributes of the Fhem object.
        /// </summary>
        public FhemPossibleAttributesCollection PossibleAttributes { get { return this.FhemObject.PossibleAttributes; } }

        /// <summary>
        /// Gets the possible sets of the Fhem object.
        /// </summary>
        public string[] PossibleSets { get { return this.FhemObject.PossibleSets; } }

        /// <summary>
        /// Gets the readings of the Fhem object.
        /// </summary>
        public FhemReadingItemsCollection Readings { get { return this.FhemObject.Readings; } }

        /// <summary>
        /// Gets the state of the Fhem object.
        /// </summary>
        public string State { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObjectsViewModel class.
        /// </summary>
        /// <param name="a_fhemObject">
        /// The <see cref="FhemObject"/> that is represented by this view model.
        /// </param>
        /// <param name="a_regionManager"></param>
        private FhemObjectViewModel( FhemObject a_fhemObject, IRegionManager a_regionManager )
        {
            //-- Initialize fields
            m_regionManager = a_regionManager;

            //-- Initialize properties
            this.FhemObject = a_fhemObject;

            //-- Initialize commands
            this.EditFhemObjectNameCommand = new DelegateCommand( this.EditFhemObjectNameCommandAction );
            this.OpenFhemObjectDetailsCommand = new DelegateCommand( this.OpenFhemObjectDetailsCommandAction );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Performs the action that should be invoked by the 
        /// 'EditFhemObjectNameCommand'.
        /// </summary>
        private void EditFhemObjectNameCommandAction()
        {
            this.IsNameEditable = true;
        }

        /// <summary>
        /// Creates a FhemObject view model.
        /// </summary>
        /// <param name="a_fhemObject"></param>
        /// <param name="a_regionManager"></param>
        /// <returns></returns>
        public static FhemObjectViewModel Create( FhemObject a_fhemObject, IRegionManager a_regionManager )
        {
            var me = new FhemObjectViewModel( a_fhemObject, a_regionManager );
            
            //-- Store the 'state' in an own property
            if( a_fhemObject.Internals.ContainsKey( STATE_TAG ) )
            {
                me.State = a_fhemObject.Internals[STATE_TAG];
            }

            return me;
        }
        
        /// <summary>
        /// Performs the action that should be invoked by the 
        /// 'OpenFhemObjectDetailsCommand'.
        /// </summary>
        private void OpenFhemObjectDetailsCommandAction()
        {
            m_regionManager.RequestNavigate( "TitleRegion", new Uri( "FhemObjectTitleView", UriKind.Relative ) );
            m_regionManager.RequestNavigate( "NavigationRegion", new Uri( "FhemObjectNavigationView", UriKind.Relative ) );
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------