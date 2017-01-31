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
using Newtonsoft.Json.Linq;
using System;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public class FhemReadingItem
    {
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the name of the Fhem object reading item.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the date time of the Fhem object reading item.
        /// </summary>
        public DateTime DateTime { get; private set; }

        /// <summary>
        /// Gets the value of the Fhem object reading item.
        /// </summary>
        public string Value { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemReadingItem class.
        /// </summary>
        /// <remarks>
        /// This constructor is private to force the usage of the 'From...'
        /// methods.
        /// </remarks>
        private FhemReadingItem() { }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Parses a reading item from its json property representation.
        /// </summary>
        /// <param name="a_jsonProperty">
        /// The json property that represents the reading item.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The json property may not be null.
        /// </exception>
        /// <returns>
        /// The parsed reading item.
        /// </returns>
        public static FhemReadingItem FromJProperty( JProperty a_jsonProperty )
        {
            //-- Validate argument
            if( a_jsonProperty == null )
            {
                throw new ArgumentNullException( "The json property may not be null!" );
            }
            
            //-- Create the new reading item
            var me = new FhemReadingItem() { Name = a_jsonProperty.Name };

            //-- Analyze the first json property
            var readingItemAsJsonObject = (JObject) a_jsonProperty.First;

            //-- Analyze the children
            foreach( var readingItemJsonProperty in readingItemAsJsonObject.Children<JProperty>() )
            {
                switch( readingItemJsonProperty.Name )
                {
                    case "Value": me.Value = (string) readingItemJsonProperty.Value; break;

                    case "Time":

                        var dateTimeAsString = (string) readingItemJsonProperty.Value;

                        if( !String.IsNullOrWhiteSpace( dateTimeAsString ) && ( dateTimeAsString != "null" ) )
                        {
                            me.DateTime = DateTime.Parse( dateTimeAsString );
                        }

                        break;

                    default: break;
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