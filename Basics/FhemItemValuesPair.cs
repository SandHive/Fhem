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
using System.Collections.Generic;
using System.Diagnostics;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    [DebuggerDisplay( "Name: {Name}" )]
    public class FhemItemValuesPair
    {
        //---------------------------------------------------------------------
        #region Fields

        private static char[]  m_itemValuesSeparatorToken = new char[] { ':' };

        private static char[]  m_valuesSeparatorToken = new char[] { ',' };

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the name of the item.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the values of the item.
        /// </summary>
        public IReadOnlyList<string> Values { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemItemValuesPair class.
        /// </summary>
        /// <param name="a_name">
        /// The name of the item.
        /// </param>
        public FhemItemValuesPair( string a_name )
            : this( a_name, new string[0] ) { }

        /// <summary>
        /// Initializes a new instance of the FhemItemValuesPair class.
        /// </summary>
        /// <param name="a_name">
        /// The name of the item.
        /// </param>
        /// <param name="a_values">
        /// The values of the item.
        /// </param>
        public FhemItemValuesPair( string a_name, IReadOnlyList<string> a_values )
        {
            //-- Initialize properties
            this.Name = a_name;
            this.Values = a_values;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Parses an item / values pair from its string representation.
        /// </summary>
        /// <param name="a_parseString">
        /// The string that represents the item / values pair.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The parse string may not be null or empty.
        /// </exception>
        /// <returns>
        /// The parsed item / values pair.
        /// </returns>
        public static FhemItemValuesPair Parse( string a_parseString )
        {
            //-- Validate argument
            if( String.IsNullOrWhiteSpace( a_parseString ) )
            {
                throw new ArgumentNullException( "The parse string may not be null or empty!" );
            }

            //-- Separate the item name and the values
            var itemValuesPairParts = a_parseString.Split( m_itemValuesSeparatorToken, StringSplitOptions.RemoveEmptyEntries );

            if( itemValuesPairParts.Length == 1 )
            {
                //-- There is only a name without any values available
                return new FhemItemValuesPair( itemValuesPairParts[0] );
            }
            else if( itemValuesPairParts.Length == 2 )
            {
                //-- Separate the values
                var itemValues = itemValuesPairParts[1].Split( m_valuesSeparatorToken, StringSplitOptions.RemoveEmptyEntries );

                return new FhemItemValuesPair( itemValuesPairParts[0], itemValues );
            }

            throw new ArgumentException( "The parse string does not represent an item / values pair!" );
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------