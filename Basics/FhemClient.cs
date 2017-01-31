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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Sockets;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics
{
    public class FhemClient : IDisposable
    {
        //---------------------------------------------------------------------
        #region Events

        /// <summary>
        /// Occurs when the 'IsConnected' property has changed.
        /// </summary>
        public event EventHandler IsConnectedChanged;

        //-- Events
        #endregion
        //---------------------------------------------------------------------
        #region Fields

        private bool  m_isConnected;

        private INetworkStreamReader  m_networkStreamReader;

        private INetworkStreamWriter  m_networkStreamWriter;

        private TcpClient  m_tcpClient;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Gets a flag that indicates whether this Fhem client is connected
        /// or disconnected.
        /// </summary>
        public bool IsConnected
        {
            get { return m_isConnected; }
            private set
            {
                //-- Do nothing when the value has not changed
                if( value == m_isConnected ) { return; }

                //-- Update the member variable
                m_isConnected = value;

                //-- Raise the corresponding event
                this.IsConnectedChanged?.Invoke( this, EventArgs.Empty );
            }
        }
        
        //-- Properties
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the FhemClient class.
        /// </summary>
        public FhemClient() { }

        /// <summary>
        /// Initializes a new instance of the FhemClient class.
        /// </summary>
        /// <param name="a_hostName">
        /// The host name of the Fhem server.
        /// </param>
        /// <param name="a_telnetPort">
        /// The Fhem server port where telnet is provided.
        /// </param>
        public FhemClient( string a_hostName, int a_telnetPort )
        {
            this.Connect( a_hostName, a_telnetPort );
        }

        /// <summary>
        /// Initializes a new instance of the FhemClient class.
        /// </summary>
        /// <param name="a_networkStreamReader"></param>
        /// <param name="a_networkStreamWriter"></param>
        /// <remarks>
        /// This constructor is intended for unit testing.
        /// </remarks>
        internal FhemClient( INetworkStreamReader a_networkStreamReader, INetworkStreamWriter a_networkStreamWriter )
        {
            //-- Initialize fields
            m_networkStreamReader = a_networkStreamReader;
            m_networkStreamWriter = a_networkStreamWriter;
            m_isConnected = true;
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region IDisposable Members

        public void Dispose()
        {
            //-- Clean up everything
            m_networkStreamReader?.Dispose();
            m_tcpClient?.Close();
        }

        //-- IDisposable Members
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Checks the connection to the Fhem server.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The Fhem client is not connected to a Fhem server.
        /// </exception>
        private void CheckConnection()
        {
            if( !m_isConnected )
            {
                throw new InvalidOperationException( "The Fhem client is not connected to a Fhem server!" );
            }
        }

        /// <summary>
        /// Connects this Fhem client with a Fhem server.
        /// </summary>
        /// <param name="a_hostName">
        /// The host name of the Fhem server.
        /// </param>
        /// <param name="a_telnetPort">
        /// The Fhem server port where telnet is provided.
        /// </param>
        public void Connect( string a_hostName, int a_telnetPort )
        {
            //-- Create the TCP client
            m_tcpClient = new TcpClient( a_hostName, a_telnetPort );

            //-- Get the network stream and create the reader and writer instances
            var networkStream = m_tcpClient.GetStream();
            m_networkStreamReader = new NetworkStreamReader( networkStream );
            m_networkStreamWriter = new NetworkStreamWriter( networkStream );

            //-- Set a flag that we are connected
            this.IsConnected = true;
        }
        
        /// <summary>
        /// Gets a specific <see cref="FhemObject"/>.
        /// </summary>
        /// <param name="a_fhemObjectName">
        /// The name of the desired Fhem object.
        /// </param>
        /// <returns>
        /// The fitting Fhem object or null when no fitting object was found.
        /// </returns>
        public FhemObject GetFhemObject( string a_fhemObjectName )
        {
            //-- Check that we are really connected to a Fhem server
            this.CheckConnection();

            //-- Use the 'jsonlist2' command for creating the FHEM object list
            var jsonlist2Response = this.SendNativeCommand( String.Format( "jsonlist2 {0}", a_fhemObjectName ) );

            //-- Parse the response into a JSON object
            var jsonObject = JObject.Parse( jsonlist2Response );

            //-- Determine the 3 main json tokens
            var argJsonToken = jsonObject.First;
            var resultsJsonToken = argJsonToken.Next;
            var totalResultsJsonToken = (JProperty) resultsJsonToken.Next;

            //-- Determine the first json token that represents a fhem object
            var fhemObjectAsJsonObject = (JObject) resultsJsonToken.First.First;

            //-- Get the number of results
            var resultsCount = (int) totalResultsJsonToken.Value;

            if( resultsCount > 0 )
            {
                //-- Parse the Fhem object from the JObject
                var fhemObject = FhemObject.FromJObject( fhemObjectAsJsonObject );

                //-- Return the first found Fhem object
                return fhemObject;    
            }

            //-- Nothing found :(
            return null;
        }

        /// <summary>
        /// Gets all available Fhem objects.
        /// </summary>
        /// <returns></returns>
        public ReadOnlyCollection<FhemObject> GetFhemObjects()
        {
            //-- Check that we are really connected to a Fhem server
            this.CheckConnection();

            //-- Use the 'jsonlist2' command for creating the FHEM object list
            var jsonlist2Response = this.SendNativeCommand( "jsonlist2" );

            //-- Parse the response into a JSON object
            var jsonObject = JObject.Parse( jsonlist2Response );

            //-- Determine the 3 main json tokens
            var argJsonToken = jsonObject.First;
            var resultsJsonToken = argJsonToken.Next;
            var totalResultsJsonToken = (JProperty) resultsJsonToken.Next;

            //-- Determine the first json token that represents a fhem object
            var fhemObjectAsJsonObject = (JObject) resultsJsonToken.First.First;

            //-- Get the number of results
            var resultsCount = (int) totalResultsJsonToken.Value;

            //-- Prepare the list for storing all Fhem objects
            var fhemObjects = new List<FhemObject>( resultsCount );

            while( fhemObjectAsJsonObject != null )
            {
                //-- Parse the Fhem object from the JObject
                var fhemObject = FhemObject.FromJObject( fhemObjectAsJsonObject );

                //-- Add it to the list
                fhemObjects.Add( fhemObject );

                //-- Update to the next JObject
                fhemObjectAsJsonObject = (JObject) fhemObjectAsJsonObject.Next;
            }

            return fhemObjects.AsReadOnly();
        }

        /// <summary>
        /// Renames a <see cref="FhemObject"/>.
        /// </summary>
        /// <param name="a_fhemObject">
        /// The affected FhemObject.
        /// </param>
        /// <param name="a_newName">
        /// The new name.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The FhemObject may not be null.
        /// </exception>
        public FhemClientResponse RenameFhemObject( FhemObject a_fhemObject, string a_newName )
        {
            //-- Check that we are really connected to a Fhem server
            this.CheckConnection();

            if( a_fhemObject == null )
            {
                throw new ArgumentNullException( "The FhemObject may not be null!" );
            }

            if( a_fhemObject.Name != a_newName )
            {
                //-- Assemble the native command for renaming the Fhem object
                var nativeRenameCommand = String.Format( "rename {0} {1}", a_fhemObject.Name, a_newName );

                //-- Send the renaming command
                var response = this.SendNativeCommand( nativeRenameCommand );

                if( response != null )
                {
                    //-- Any response will describe an error in this case
                    return new FhemClientResponse( response );
                }
            }

            //-- Everything ok
            return new FhemClientResponse();
        }
        
        /// <summary>
        /// Sends a native Fhem command.
        /// </summary>
        /// <param name="a_nativeCommandString">
        /// The native Fhem command.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// The native command string may not be null or empty.
        /// </exception>
        /// <returns>
        /// The native Fhem response.
        /// </returns>
        public string SendNativeCommand( string a_nativeCommandString )
        {
            //-- Check that we are really connected to a Fhem server
            this.CheckConnection();

            if( String.IsNullOrWhiteSpace( a_nativeCommandString ) )
            {
                throw new ArgumentNullException( "The native command string may not be null or empty!" );
            }

            //-- Each FHEM command must end with "\r\n"
            if( !a_nativeCommandString.EndsWith( "\r\n" ) )
            {
                a_nativeCommandString = a_nativeCommandString + "\r\n";
            }

            //-- Write the native command string to the network stream
            m_networkStreamWriter.Write( a_nativeCommandString );

            //-- Read the response from the network stream
            var response = m_networkStreamReader.ReadString();

            return response;
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------