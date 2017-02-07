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
using System.Collections.ObjectModel;
//-----------------------------------------------------------------------------
namespace Sand.Fhem.Basics.Collections
{
    public class FhemObjectsCollection : ICollection<FhemObject>, IEnumerable<FhemObject>, IEnumerable, IList, IList<FhemObject>
    {
        //---------------------------------------------------------------------
        #region Fields

        private SortedList<int, FhemObject>  m_fhemObjectsById;

        //-- Fields
        #endregion
        //---------------------------------------------------------------------
        #region Constructors

        /// <summary>
        /// Initializes a new instanc of the FhemObjectsCollection class.
        /// </summary>
        public FhemObjectsCollection()
        {
            //-- Initialize fields
            m_fhemObjectsById = new SortedList<int, FhemObject>();
        }

        /// <summary>
        /// Initializes a new instanc of the FhemObjectsCollection class.
        /// </summary>
        /// <param name="a_capacity">
        /// The initial number of elements that the 
        /// System.Collections.Generic.SortedList can contain.
        /// </param>
        public FhemObjectsCollection( int a_capacity )
        {
            //-- Initialize fields
            m_fhemObjectsById = new SortedList<int, FhemObject>( a_capacity );
        }

        //-- Constructors
        #endregion
        //---------------------------------------------------------------------
        #region ICollection<FhemObject> Members

        public int Count
        {
            get { return m_fhemObjectsById.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public void Add( FhemObject a_fhemObject )
        {
            m_fhemObjectsById.Add( a_fhemObject.ID, a_fhemObject );
        }

        public void Clear()
        {
            m_fhemObjectsById.Clear();
        }

        public bool Contains( FhemObject a_fhemObject )
        {
            return m_fhemObjectsById.ContainsKey( a_fhemObject.ID );
        }

        public void CopyTo( FhemObject[] array, int arrayIndex )
        {
            throw new NotSupportedException();
        }

        public bool Remove( FhemObject a_fhemObject )
        {
            return m_fhemObjectsById.Remove( a_fhemObject.ID );
        }

        //--  ICollection<FhemObject> Members
        #endregion
        //---------------------------------------------------------------------
        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return m_fhemObjectsById.Values.GetEnumerator();
        }

        //-- IEnumerable Members
        #endregion
        //---------------------------------------------------------------------
        #region IEnumerable<FhemObject> Members

        IEnumerator<FhemObject> IEnumerable<FhemObject>.GetEnumerator()
        {
            return m_fhemObjectsById.Values.GetEnumerator();
        }

        //-- IEnumerable<FhemObject> Members
        #endregion
        //---------------------------------------------------------------------
        #region IList Members

        public bool IsFixedSize
        {
            get { throw new NotSupportedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotSupportedException(); }
        }

        public bool IsSynchronized
        {
            get { throw new NotSupportedException(); }
        }

        object IList.this[int a_fhemObjectId]
        {
            get { return m_fhemObjectsById[a_fhemObjectId]; }
            set { m_fhemObjectsById[a_fhemObjectId] = (FhemObject) value; }
        }

        public int Add( object a_value )
        {
            var fhemObject = (FhemObject) a_value;

            this.Add( fhemObject );

            return this.IndexOf( fhemObject );
        }

        public bool Contains( object a_value )
        {
            var fhemObject = (FhemObject) a_value;

            return m_fhemObjectsById.ContainsKey( fhemObject.ID );
        }

        public int IndexOf( object a_value )
        {
            var fhemObject = (FhemObject) a_value;

            return this.IndexOf( fhemObject );
        }

        public void Insert( int index, object a_value )
        {
            throw new NotSupportedException();
        }

        public void Remove( object a_value )
        {
            var fhemObject = (FhemObject) a_value;

            this.Remove( fhemObject );
        }

        public void CopyTo( Array array, int index )
        {
            throw new NotSupportedException();
        }

        //-- IList Members
        #endregion
        //---------------------------------------------------------------------
        #region IList<FhemObject> Members

        public FhemObject this[int a_fhemObjectId]
        {
            get { return m_fhemObjectsById[a_fhemObjectId]; }
            set { m_fhemObjectsById[a_fhemObjectId] = value; }
        }

        public int IndexOf( FhemObject a_fhemObject )
        {
            return m_fhemObjectsById.IndexOfKey( a_fhemObject.ID );
        }

        public void Insert( int a_index, FhemObject a_fhemObject )
        {
            throw new NotSupportedException();
        }

        public void RemoveAt( int a_index )
        {
            m_fhemObjectsById.RemoveAt( a_index );
        }

        //-- IList<FhemObject> Members
        #endregion
        //---------------------------------------------------------------------
        #region Methods

        /// <summary>
        /// Returns a read-only System.Collections.Generic.IList wrapper for 
        /// the current collection.
        /// </summary>
        /// <returns>
        ///  A System.Collections.ObjectModel.ReadOnlyCollection that acts as a 
        ///  read-only wrapper around the current System.Collections.Generic.List.
        /// </returns>
        public ReadOnlyCollection<FhemObject> AsReadOnly()
        {
            return new ReadOnlyCollection<FhemObject>( this );
        }

        //-- Methods
        #endregion
        //---------------------------------------------------------------------
    }
}
//-----------------------------------------------------------------------------