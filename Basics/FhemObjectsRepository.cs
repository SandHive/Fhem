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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Timers;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public class FhemObjectsRepository : IEnumerable<FhemObject>, INotifyCollectionChanged
    {
        //---------------------------------------------------------------------
        #region Fields

        private FhemClient  m_fhemClient;
        
        private SortedList<int, FhemObject>  m_fhemObjectsByNr = new SortedList<int, FhemObject>();

        private Timer  m_updateTimer;

        private double  m_updateTimerInterval = 5000;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the duration of the last repository update. Just nice to know.
        /// </summary>
        public TimeSpan LastUpdateDuration { get; private set; }

        /// <summary>
        /// Gets or sets the update intervall.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The update interval may not be smaller than 2000ms.
        /// </exception>
        public double UpdateInterval
        {
            get { return m_updateTimerInterval;  }
            set
            {
                //-- Do nothing when the value has not changed
                if( value == m_updateTimerInterval ) { return; }

                if( value < 2000 )
                {
                    throw new ArgumentOutOfRangeException( "The update interval may not be smaller than 2000ms!" );
                }

                //-- Update the timer interval
                m_updateTimerInterval = value;

                //-- Reset the update timer
                this.ResetUpdateTimer( m_updateTimerInterval );
            }
        }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObjectsRepository class.
        /// </summary>
        /// <param name="a_fhemClient">
        /// The Fhem client for keeping the repository up to date.
        /// </param>
        /// <remarks>
        /// The constructor is private to force the usage of the static 
        /// 'Create' method (long lasting operations do not belong into a 
        /// constructor ;).
        /// </remarks>
        public FhemObjectsRepository( FhemClient a_fhemClient )
        {
            //-- Initialize fields
            m_fhemClient = a_fhemClient;
            
            //-- Start the update timer with a very small interval to force an
            //-- immediate update. After elapsing, the timer will be configured
            //-- anew for standard use
            m_updateTimer = new Timer( 20 );
            m_updateTimer.AutoReset = false; //-- The starter event handler should be called only once
            m_updateTimer.Elapsed += m_updateTimer_Elapsed_StarterHandler;
            m_updateTimer.Start();
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Event Handlers

        private void m_updateTimer_Elapsed( object sender, ElapsedEventArgs e )
        {
            //-- Keep the start timestamp in mind
            var startDateTime = DateTime.Now;

            //-- Get all available Fhem objects
            var currentFhemObjects = m_fhemClient.GetFhemObjects();

            //-- Determine old Fhem objects
            ////var oldFhemObjects = new List<FhemObject>();
            ////foreach( FhemObject tmpFhemObject in m_fhemObjectsByNr.Values )
            ////{
            ////    if( !currentFhemObjects.ContainsKey( tmpFhemObject.ID ) )
            ////    {
            ////        oldFhemObjects.Add( tmpFhemObject );
            ////    }
            ////}
            
            ////-- Determine new Fhem objects
            //var newFhemObjects = new List<FhemObject>();
            //foreach( FhemObject currentFhemObject in currentFhemObjects )
            //{
            //    if( !m_fhemObjectsByNr.ContainsKey( currentFhemObject.ID ) )
            //    {
            //        newFhemObjects.Add( currentFhemObject );
            //    }
            //}

            ////-- Raise the 'CollectionChanged' event
            //this.CollectionChanged?.Invoke( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Replace, newFhemObjects, oldFhemObjects ) );

            //-- Calculate the update duration
            this.LastUpdateDuration = DateTime.Now - startDateTime;
        }

        private void m_updateTimer_Elapsed_StarterHandler( object sender, ElapsedEventArgs e )
        {
            //-- Get all available Fhem objects
            var fhemObjects = m_fhemClient.GetFhemObjects();
            
            //-- Initialize the observable collection
            foreach( var fhemObject in fhemObjects )
            {
                m_fhemObjectsByNr.Add( fhemObject.ID, fhemObject );
            }

            //-- Raise the 'CollectionChanged' event
            this.CollectionChanged?.Invoke( this, new NotifyCollectionChangedEventArgs( NotifyCollectionChangedAction.Add, fhemObjects ) );
            
            //-- Reset the update timer for regular use
            this.ResetUpdateTimer( m_updateTimerInterval );
        }

        //-- Event Handlers
        #endregion
        //---------------------------------------------------------------------
        #region IEnumerable

        public IEnumerator GetEnumerator()
        {
            return m_fhemObjectsByNr.Values.GetEnumerator();
        }

        IEnumerator<FhemObject> IEnumerable<FhemObject>.GetEnumerator()
        {
            return m_fhemObjectsByNr.Values.GetEnumerator();
        }

        //-- IEnumerable
        #endregion
        //---------------------------------------------------------------------
        #region INotifyCollectionChanged

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        //-- INotifyCollectionChanged
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Resets the update timer with the given update interval.
        /// </summary>
        /// <param name="a_updateInterval">
        /// The update interval.
        /// </param>
        private void ResetUpdateTimer( double a_updateInterval )
        {
            if( m_updateTimer != null )
            {
                //-- Stop an existing timer
                m_updateTimer.Stop();
                m_updateTimer.Elapsed -= m_updateTimer_Elapsed;
            }

            //-- Create a new timer with the new interval
            m_updateTimer = new Timer( a_updateInterval );
            m_updateTimer.Elapsed += m_updateTimer_Elapsed;
            m_updateTimer.Start();
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------