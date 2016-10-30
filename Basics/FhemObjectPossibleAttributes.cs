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
using System;
using System.Collections.Generic;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public class FhemObjectPossibleAttributes
    {
        //---------------------------------------------------------------------
        #region Fields

        private SortedList<string, IList<string>>  m_possibleAttributeItems;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        public IEnumerable<KeyValuePair<string, IList<string>>> PossibleAttributeItems {  get { return m_possibleAttributeItems; } }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObjectPossibleAttributeItem 
        /// class.
        /// </summary>
        /// <remarks>
        /// This constructor is private to force the usage of the 'From...'
        /// methods.
        /// </remarks>
        private FhemObjectPossibleAttributes() { }

        //-- Constructors
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
        public static FhemObjectPossibleAttributes Parse( string a_parseString )
        {
            //-- Validate argument
            if( String.IsNullOrWhiteSpace( a_parseString ) )
            {
                throw new ArgumentNullException( "The parse string may not be null or empty!" );
            }
            
            //-- Create the new possible attribute item
            var me = new FhemObjectPossibleAttributes();

            //-- Just separate the attributes
            var AttributeValuePairs = a_parseString.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

            //-- Sort the result
            Array.Sort( AttributeValuePairs );

            me.m_possibleAttributeItems = new SortedList<string, IList<string>>( AttributeValuePairs.Length );

            foreach( var attributeValuePair in AttributeValuePairs )
            {
                var attributeValuePairParts = attributeValuePair.Split( new char[] { ':' }, StringSplitOptions.RemoveEmptyEntries );

                if( attributeValuePairParts.Length == 1 )
                {
                    //-- In case of duplicates, the last one wins
                    me.m_possibleAttributeItems[attributeValuePairParts[0]] = new List<string>( 0 );
                }
                else if( attributeValuePairParts.Length == 2 )
                {
                    var possibleValues = attributeValuePairParts[1].Split( new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries );

                    //-- In case of duplicates, the last one wins
                    me.m_possibleAttributeItems[attributeValuePairParts[0]] = possibleValues;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

            return me;
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------