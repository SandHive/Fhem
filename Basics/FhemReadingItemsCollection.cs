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
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public class FhemReadingItemsCollection : IReadOnlyList<FhemReadingItem>
    {
        //---------------------------------------------------------------------
        #region Fields

        private SortedList<string, FhemReadingItem>  m_readings;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemReadingItemsCollection class.
        /// </summary>
        /// <remarks>
        /// This constructor is private to force the usage of the 'From...'
        /// methods.
        /// </remarks>
        private FhemReadingItemsCollection() { }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region IReadOnlyList Members

        public int Count
        {
            get
            {
                return m_readings.Count;
            }
        }

        public FhemReadingItem this[int a_index]
        {
            get
            {
                return m_readings.Values[a_index];
            }
        }

        public IEnumerator<FhemReadingItem> GetEnumerator()
        {
            return m_readings.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_readings.Values.GetEnumerator();
        }

        //-- IReadOnlyList Members
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Parses a reading items collection from its json object 
        /// representation.
        /// </summary>
        /// <param name="a_jsonObject">
        /// The json object that represents the reading items collection.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The json object may not be null.
        /// </exception>
        /// <returns>
        /// The parsed reading items collection.
        /// </returns>
        public static FhemReadingItemsCollection FromJObject( JObject a_jsonObject )
        {
            //-- Validate argument
            if( a_jsonObject == null )
            {
                throw new ArgumentNullException( "The json object may not be null!" );
            }

            //-- Create the new reading items collection
            var me = new FhemReadingItemsCollection();
            
            //-- Prepare the internal sorted list
            me.m_readings = new SortedList<string, FhemReadingItem>( a_jsonObject.Count );

            foreach( var jsonReading in a_jsonObject.Children<JProperty>() )
            {
                //-- Parse the reading item
                var readingItem = FhemReadingItem.FromJProperty( jsonReading );

                //-- Store the reading item 
                me.m_readings[readingItem.Name] = readingItem;
            }

            return me;
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------