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
using System.Data.EntityClient;
using System.Data.Objects;

namespace ActivityMonitor
{
    /// <summary>
    /// Additional extension for interface <code>IQueryableExtension</code>, to
    /// allow includes on <code>IObjectSet</code> when using mocking contexts.
    /// </summary>
    public static class IQueryableExtension
    {
        public static IQueryable<T> Include<T>
            (this IQueryable<T> source, string path)
            where T : class
        {
            ObjectQuery<T> objectQuery = source as ObjectQuery<T>;
            if (objectQuery != null)
            {
                return objectQuery.Include(path);
            }
            return source;
        }
    }
}


