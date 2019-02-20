using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using NUnit.Framework;

namespace User.DAL.Tests.Abstractions
{
    public static class IdentityResultAssert
    {
        public static void IsSuccess(IdentityResult result)
        {
            Assert.NotNull(result);
            Assert.True(result.Succeeded);
        }

        public static void IsFailure(IdentityResult result)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
        }

        public static void IsFailure(IdentityResult result, string error)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.AreEqual(error, result.Errors.First().Description);
        }

        public static void IsFailure(IdentityResult result, IdentityError error)
        {
            Assert.NotNull(result);
            Assert.False(result.Succeeded);
            Assert.AreEqual(error.Description, result.Errors.First().Description);
            Assert.AreEqual(error.Code, result.Errors.First().Code);
        }
    }
}
