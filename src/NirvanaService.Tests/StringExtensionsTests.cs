using System;
using NUnit.Framework;

namespace NirvanaService.Tests
{
    [TestFixture]
    public class StringExtensionsTests
    {
        [TestFixtureSetUp]
        public void Setup()
        {
            Environment.SetEnvironmentVariable("TEST", "Hello");
            Environment.SetEnvironmentVariable("TEST2", "World");
        }

        [Test]
        public void ResolveEnvVariables_ShouldReturnSameStringIfNoEnvironmentVariables()
        {
            var str = "TESTING";
            Assert.AreEqual("TESTING", str.ResolveEnvVariables());
        }

        [Test]
        public void ResolveEnvVariables_ShouldReplaceOneVariable()
        {
            var str = "%TEST%ING";
            Assert.AreEqual("HelloING", str.ResolveEnvVariables());

        }

        [Test]
        public void ResolveEnvVariables_ShouldNotReplaceMissingVariable()
        {
            var str = "%MISSING%ING";
            Assert.AreEqual("%MISSING%ING", str.ResolveEnvVariables());

        }

        [Test]
        public void ResolveEnvVariables_ShouldReplaceAllVariables()
        {
            var str = "This is %TEST% some magic %TEST2% text";
            Assert.AreEqual("This is Hello some magic World text", str.ResolveEnvVariables());

        }
    }
}
