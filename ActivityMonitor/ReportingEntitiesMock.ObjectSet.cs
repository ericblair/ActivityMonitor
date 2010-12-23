//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
// Architectural overview and usage guide: 
// http://blogofrab.blogspot.com/2010/08/maintenance-free-mocking-for-unit.html
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.EntityClient;
using System.Data.Objects;

namespace ActivityMonitor.ReportingEntitiesMockObjectSet
{
    /// <summary>
    /// Concrete object set for use with Mock contexts. Implements all of the
    /// required interfaces, but performs no database functionality; instead
    /// merely stores the data for testing.
    /// </summary>
    public partial class MockObjectSet<T> : IObjectSet <T> 
        where T : class
    {
        private readonly IList<T> m_container = new List<T>();
    
        #region IObjectSet<T> Members
        /// <summary>
        /// Notifies the set that an object that represents a new entity must 
        /// be added to the set.
        /// </summary>
        public void AddObject(T entity)
        {
            m_container.Add(entity);
        }
    
        /// <summary>
        /// Notifies the set that an object that represents an existing entity
        /// must be added to the set.
        /// </summary>
        public void Attach(T entity)
        {
            m_container.Add(entity);
        }
    
        /// <summary>
        /// Notifies the set that an object that represents an existing entity 
        /// must be deleted from the set.
        /// </summary>
        public void DeleteObject(T entity)
        {
            m_container.Remove(entity);
        }
    
        /// <summary>
        /// Notifies the set that an object that represents an existing entity must be
        /// detached from the set.
        /// </summary>
        /// <param name="entity"></param>
        public void Detach(T entity)
        {
            m_container.Remove(entity);
        }
        #endregion
    
        #region IEnumerable<T> Members
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return m_container.GetEnumerator();
        }
        #endregion
    
        #region IEnumerable Members
        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_container.GetEnumerator();
        }
        #endregion
    
        #region IQueryable<T> Members
        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression 
        /// tree associated with this instance of System.Linq.IQueryable is executed.
        /// </summary>
        public Type ElementType
        {
            get { return typeof(T); }
        }
    
        /// <summary>
        /// Gets the expression tree that is associated with the instance of 
        /// <code>System.Linq.IQueryable.</code>
        /// </summary>
        public System.Linq.Expressions.Expression Expression
        {
            get { return m_container.AsQueryable<T>().Expression; }
        }
    
        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        public IQueryProvider Provider
        {
            get { return m_container.AsQueryable<T>().Provider; }
        }
        #endregion
    }
}
