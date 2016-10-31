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
using System;
using System.Collections;
using System.Collections.Generic;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public class FhemPossibleAttributes : IReadOnlyList<FhemItemValuesPair>
    {
        //---------------------------------------------------------------------
        #region Fields

        private SortedList<string, FhemItemValuesPair>  m_possibleAttributeItems;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemPossibleAttributes class.
        /// </summary>
        /// <remarks>
        /// This constructor is private to force the usage of the 'From...'
        /// methods.
        /// </remarks>
        private FhemPossibleAttributes() { }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region IReadOnlyList Members

        public int Count
        {
            get
            {
                return m_possibleAttributeItems.Count;
            }
        }

        public FhemItemValuesPair this[int a_index]
        {
            get
            {
                return m_possibleAttributeItems.Values[a_index];
            }
        }

        public IEnumerator<FhemItemValuesPair> GetEnumerator()
        {
            return m_possibleAttributeItems.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_possibleAttributeItems.Values.GetEnumerator();
        }

        //-- IReadOnlyList Members
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Parses a possible attribute from its string representation.
        /// </summary>
        /// <param name="a_parseString">
        /// The string that represents the possible attribute.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The parse string may not be null or empty.
        /// </exception>
        /// <returns>
        /// The parsed possible attribute.
        /// </returns>
        public static FhemPossibleAttributes Parse( string a_parseString )
        {
            //-- Validate argument
            if( String.IsNullOrWhiteSpace( a_parseString ) )
            {
                throw new ArgumentNullException( "The parse string may not be null or empty!" );
            }
            
            //-- Create the new possible attribute item
            var me = new FhemPossibleAttributes();

            //-- Just separate the attributes
            var attributeValuesPairs = a_parseString.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );
            
            //-- Prepare the internal sorted list (we use a SortedList for two
            //-- reasons: 1. to sort the possible attributes by their names, 2.
            //-- to be able to prevent dupplicates by having a 'key' property
            //-- so the last value will always overwrite the previous one)
            me.m_possibleAttributeItems = new SortedList<string, FhemItemValuesPair>( attributeValuesPairs.Length );

            foreach( var attributeValuesPair in attributeValuesPairs )
            {
                var itemValuesPair = FhemItemValuesPair.Parse( attributeValuesPair );

                //-- In case of duplicates, the last one wins
                me.m_possibleAttributeItems[itemValuesPair.Name] = itemValuesPair;
            }

            return me;
        }
        
        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------