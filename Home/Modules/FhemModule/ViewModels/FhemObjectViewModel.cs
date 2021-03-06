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
using Prism.Commands;
using Prism.Regions;
using Sand.Fhem.Basics;
using Sand.Fhem.Home.Basics.Services;
using Sand.Fhem.Home.Modules.FhemModule.Services;
using System;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Home.Modules.FhemModule.ViewModels
{
    public class FhemObjectViewModel : FhemViewModelBase
    {
        //---------------------------------------------------------------------
        #region Fields

        private IApplicationService  m_applicationService;

        private string  m_fhemObjectName;

        private bool  m_isNameEditable;
        
        private IRegionManager  m_regionManager;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the command for aborting the Fhem object renaming.
        /// </summary>
        public DelegateCommand AbortFhemObjectRenamingCommand { get; private set; }
        
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
        public FhemObject FhemObject { get; private set; }

        /// <summary>
        /// Gets a flag that indicates whether the name is currently editable.
        /// </summary>
        public bool IsNameEditable
        {
            get { return m_isNameEditable; }
            internal set
            {
                this.SetProperty( ref m_isNameEditable, value );
            }
        }

        /// <summary>
        /// Gets or sets the name of the Fhem object.
        /// </summary>
        public string Name
        {
            get { return m_fhemObjectName; }
            set
            {
                this.SetProperty( ref m_fhemObjectName, value );
            }
        }

        /// <summary>
        /// Gets the command for opening the details of a Fhem object.
        /// </summary>
        public DelegateCommand OpenFhemObjectDetailsCommand { get; private set; }

        /// <summary>
        /// Gets the command for renaming the Fhem object.
        /// </summary>
        public DelegateCommand RenameFhemObjectNameCommand { get; private set; }

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
        /// <param name="a_fhemService"></param>
        /// <param name="a_regionManager"></param>
        /// <param name="a_applicationService"></param>
        public FhemObjectViewModel( FhemObject a_fhemObject, IFhemService a_fhemService, IRegionManager a_regionManager, IApplicationService a_applicationService )
            : base( a_fhemService )
        {
            //-- Initialize fields
            m_applicationService = a_applicationService;
            m_regionManager = a_regionManager;

            //-- Initialize properties
            this.FhemObject = a_fhemObject;
            this.Name = this.FhemObject.Name;

            //-- Initialize commands
            this.AbortFhemObjectRenamingCommand = new DelegateCommand( this.AbortFhemObjectRenamingCommandAction );
            this.EditFhemObjectNameCommand = new DelegateCommand( this.EditFhemObjectNameCommandAction );
            this.OpenFhemObjectDetailsCommand = new DelegateCommand( this.OpenFhemObjectDetailsCommandAction );
            this.RenameFhemObjectNameCommand = new DelegateCommand( this.RenameFhemObjectNameCommandAction );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Performs the action that should be invoked by the 
        /// 'AbortFhemObjectRenamingCommand'.
        /// </summary>
        private void AbortFhemObjectRenamingCommandAction()
        {
            //-- Inform the Fhem service about the ending of the editing
            ( (FhemService) this.FhemService ).RaiseFhemObjectNameEditingEndEvent( this );

            //-- Just make the name readonly again and discard the changes
            this.IsNameEditable = false;
        }
        
        /// <summary>
        /// Performs the action that should be invoked by the 
        /// 'EditFhemObjectNameCommand'.
        /// </summary>
        private void EditFhemObjectNameCommandAction()
        {
            //-- Inform the Fhem service about the starting of the editing
            ( (FhemService) this.FhemService ).RaiseFhemObjectNameEditingStartEvent( this );

            //-- Make the name editable
            this.IsNameEditable = true;
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

        /// <summary>
        /// Performs the action that should be invoked by the 
        /// 'RenameFhemObjectNameCommand'.
        /// </summary>
        private void RenameFhemObjectNameCommandAction()
        {
            //-- Inform the Fhem service about the ending of the editing
            ( (FhemService) this.FhemService ).RaiseFhemObjectNameEditingEndEvent( this );

            //-- Make the name readonly again
            this.IsNameEditable = false;

            //-- Rename the FhemObject 
            var fhemClientResponse = this.FhemService.FhemClient.RenameFhemObject( this.FhemObject, this.Name );

            if( fhemClientResponse.IsFailed )
            {
                //-- Renaming failed, so let's take the original name again
                this.Name = this.FhemObject.Name;

                //-- Show the error information
                m_applicationService.ShowErrorMessage( fhemClientResponse.ErrorInformation );
            }
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------