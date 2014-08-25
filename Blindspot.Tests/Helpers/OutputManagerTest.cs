using System;
using Blindspot.Helpers;
using Blindspot.Tests.Builders;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Blindspot.Tests.Helpers
{
    [TestClass]
    public class OutputManagerTest
    {
        [TestMethod]
        public void Output_OutputsToScreenReaderWhenUserHasScreenReaderOption()
        {
            // arrange
            var builder = MockIScreenReaderBuilder.getBuilder().WithRememberThatSayStringWasCalled();
            var mockScreenReader = builder.BuildMock();
            var outputManager = new OutputManager(mockScreenReader);
            UserSettings.Instance.ScreenReaderOutput = true;

            // act
            outputManager.OutputMessage("Put me out");

            // assert
            Assert.IsTrue(builder.SayStringWasCalled);
        }
    }
}