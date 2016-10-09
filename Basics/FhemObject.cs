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
using System.Collections.Generic;
using System.Collections.ObjectModel;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public class FhemObject
    {
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the attributes of the Fhem object.
        /// </summary>
        public ReadOnlyDictionary<string, string> Attributes { get; private set; }

        /// <summary>
        /// Gets the internals of the Fhem object.
        /// </summary>
        public ReadOnlyDictionary<string, string> Internals { get; private set; }

        /// <summary>
        /// Gets the name of the Fhem object.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the possible attributes of the Fhem object.
        /// </summary>
        public string[] PossibleAttributes { get; private set; }

        /// <summary>
        /// Gets the possible sets of the Fhem object.
        /// </summary>
        public string[] PossibleSets { get; private set; }

        public object Readings { get; private set; }

        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemObject class.
        /// </summary>
        /// <remarks>
        /// This constructor is private to force the usage of the 'From...'
        /// methods.
        /// </remarks>
        private FhemObject() { }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Parses a Fhem object from its json object representation.
        /// </summary>
        /// <param name="a_jsonObject">
        /// The json object that represents the Fhem object.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The json object may not be null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The json object must have 6 children
        /// </exception>
        /// <returns>
        /// The parses Fhem object.
        /// </returns>
        public static FhemObject FromJObject( JObject a_jsonObject )
        {
            //-- Validate argument
            if( a_jsonObject == null )
            {
                throw new ArgumentNullException( "The json object may not be null!" );
            }

            if( a_jsonObject.Count != 6 )
            {
                throw new ArgumentOutOfRangeException( "The json object must have 6 children!" );
            }

            //-- Create the new fhem object
            var me = new FhemObject();

            //-- Analyze the first json property
            foreach( var jsonProperty in a_jsonObject.Children<JProperty>() )
            {
                switch( jsonProperty.Name )
                {
                    #region case "Attributes":

                    case "Attributes":

                        //-- Get the Json object that contains the attributes
                        var attributesAsJsonObject = (JObject) jsonProperty.First;

                        //-- Prepare a dictionary
                        var attributes = new Dictionary<string, string>( attributesAsJsonObject.Count );

                        foreach( var jsonAttribute in attributesAsJsonObject.Children<JProperty>() )
                        {
                            attributes.Add( jsonAttribute.Name, (string) jsonAttribute.Value );
                        }

                        //-- Wrap the dictionary with a readonly dictionary
                        me.Attributes = new ReadOnlyDictionary<string, string>( attributes );

                        break;

                    //-- case "Attributes":
                    #endregion

                    #region case "Internals":

                    case "Internals":

                        //-- Get the Json object that contains the attributes
                        var internalsAsJsonObject = (JObject) jsonProperty.First;

                        //-- Prepare a dictionary
                        var internals = new Dictionary<string, string>( internalsAsJsonObject.Count );

                        foreach( var jsonInternal in internalsAsJsonObject.Children<JProperty>() )
                        {
                            internals.Add( jsonInternal.Name, (string) jsonInternal.Value );
                        }

                        //-- Wrap the dictionary with a readonly dictionary
                        me.Internals = new ReadOnlyDictionary<string, string>( internals );

                        break;

                    //-- case "Internals":
                    #endregion

                    case "Name": me.Name = (string) jsonProperty.Value; break;

                    #region case "PossibleAttrs":

                    case "PossibleAttrs":

                        //-- Get the string that contains all possible attributes
                        var encodedPossibleAttributes = (string) jsonProperty.Value;

                        //-- Just separate the attributes
                        me.PossibleAttributes = encodedPossibleAttributes.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

                        //-- Sort the result
                        Array.Sort( me.PossibleAttributes );

                        break;

                    //-- case "PossibleAttrs":
                    #endregion
                    
                    #region case "PossibleSets":

                    case "PossibleSets":

                        //-- Get the string that contains all possible sets
                        var encodedPossibleSets = (string) jsonProperty.Value;

                        //-- Just separate the sets
                        me.PossibleSets = encodedPossibleSets.Split( new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries );

                        //-- Sort the result
                        Array.Sort( me.PossibleSets );

                        break;

                    //-- case "PossibleSets":
                    #endregion

                    case "Readings": me.Readings = jsonProperty.Value; break;

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