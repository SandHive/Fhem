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
using Sand.Fhem.Basics.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    [DebuggerDisplay( "{Name}" )]
    public class FhemObject
    {
        //---------------------------------------------------------------------
        #region Constants

        private const string INTERNALS_ID_TAG = "NR";
        private const string INTERNALS_STATE_TAG = "STATE";

        //-- Constants
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets the attributes of the Fhem object.
        /// </summary>
        public ReadOnlyDictionary<string, string> Attributes { get; private set; }

        /// <summary>
        /// Gets the unique Fhem identifier.
        /// </summary>
        public int ID { get; private set; }

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
        public FhemPossibleAttributesCollection PossibleAttributes { get; private set; }

        /// <summary>
        /// Gets the possible sets of the Fhem object.
        /// </summary>
        public string[] PossibleSets { get; private set; }

        /// <summary>
        /// Gets the readings of the Fhem object.
        /// </summary>
        public FhemReadingItemsCollection Readings { get; private set; }

        /// <summary>
        /// Gets the state of the Fhem object.
        /// </summary>
        public string State { get; private set; }

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

            //-- Analyze the children
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

                    case "PossibleAttrs": me.PossibleAttributes = FhemPossibleAttributesCollection.Parse( (string) jsonProperty.Value ); break;

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

                    case "Readings": me.Readings = FhemReadingItemsCollection.FromJObject( (JObject) jsonProperty.First ); break;

                    default: break;
                }
            }

            //-- Initialize properties
            me.ID = int.Parse( me.Internals[INTERNALS_ID_TAG] );

            //-- Store the 'state' in an own property
            if( me.Internals.ContainsKey( INTERNALS_STATE_TAG ) )
            {
                me.State = me.Internals[INTERNALS_STATE_TAG];
            }

            return me;
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------