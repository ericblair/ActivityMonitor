// <copyright file="UpdateActivityDataTest.cs" company="Microsoft">Copyright © Microsoft 2010</copyright>
using System;
using ActivityMonitor;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ActivityMonitor
{
    /// <summary>This class contains parameterized unit tests for UpdateActivityData</summary>
    [PexClass(typeof(UpdateActivityData))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class UpdateActivityDataTest
    {
        /// <summary>Test stub for CheckActivityDataHasBeenUpdated()</summary>
        [PexMethod]
        internal bool CheckActivityDataHasBeenUpdated([PexAssumeUnderTest]UpdateActivityData target)
        {
            bool result = target.CheckActivityDataHasBeenUpdated();
            return result;
            // TODO: add assertions to method UpdateActivityDataTest.CheckActivityDataHasBeenUpdated(UpdateActivityData)
        }
    }
}
