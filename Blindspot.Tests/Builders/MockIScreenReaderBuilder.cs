using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using ScreenReaderAPIWrapper;

namespace Blindspot.Tests.Builders
{
    public class MockIScreenReaderBuilder
    {
        private Mock<IScreenReader> _mockScreenReader;
        private bool _rememberIfSayStringWasCalled = false;

        public static MockIScreenReaderBuilder getBuilder()
        {
            var builder = new MockIScreenReaderBuilder();
            builder._mockScreenReader = new Mock<IScreenReader>();
            return builder;
        }

        public IScreenReader BuildMock()
        {
            // set up the mock
            if (_rememberIfSayStringWasCalled)
            {
                _mockScreenReader.Setup(s => s.SayString(It.IsAny<string>(), It.IsAny<bool>()))
                    .Callback(() => SayStringWasCalled = true);
            }
            return _mockScreenReader.Object;
        }

        public MockIScreenReaderBuilder WithRememberThatSayStringWasCalled()
        {
            _rememberIfSayStringWasCalled = true;
            return this;
        }

        public bool SayStringWasCalled { get; private set; }
    }
}