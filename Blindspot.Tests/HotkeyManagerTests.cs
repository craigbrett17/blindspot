using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blindspot.Helpers;
using System.Windows.Forms;
using System.Collections.Generic;
using Moq;

namespace Blindspot.Tests
{
    // more of a system test here
    [TestClass]
    public class HotkeyManagerTests
    {
        private static Mock<IBufferHolder> holder;
        private static BufferHotkeyManager manager;
        private readonly int testCaseCount = 7;

        [TestInitialize()]
        public void InitializeManager()
        {
            RemakeManager();
        }

        private static void RemakeManager()
        {
            holder = new Mock<IBufferHolder>();
            holder.Setup(h => h.Commands).Returns(new Dictionary<string, System.ComponentModel.HandledEventHandler>());
            manager = BufferHotkeyManager.LoadFromTextFile(holder.Object, new System.IO.StreamReader(@"Settings\hotkeys.txt"));
        }

        [TestMethod]
        public void ConstructorCorrectlyAssignsParentFormProperty()
        {
            Assert.IsNotNull(manager.Parent);
            Assert.IsInstanceOfType(manager.Parent, typeof(IBufferHolder));
        }

        [TestMethod]
        public void CorrectNumberOfHotkeysCreated()
        {
            Assert.AreEqual(manager.Hotkeys.Count, testCaseCount);
        }

        [TestMethod]
        public void CorrectNonModifierKeysAssigned()
        {
            Dictionary<int, Keys> correctKeys = CorrectKeysForFirstSixLinesOfFile;
            for (int index = 0; index < manager.Hotkeys.Count; index++)
            {
                Assert.AreEqual(correctKeys[index], manager.Hotkeys[index].KeyCode);
            }
        }

        private Dictionary<int, Keys> CorrectKeysForFirstSixLinesOfFile
        {
            get
            {
                return new Dictionary<int, Keys>
                {
                    {0, Keys.F4},
                    {1, Keys.Left},
                    {2, Keys.Right},
                    {3, Keys.Up},
                    {4, Keys.Down},
                    {5, Keys.A},
                    {6, Keys.A}
                };
            }
        }

    }
}