using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blindspot;
using Blindspot.ViewModels;

namespace Blindspot.Tests
{
    [TestClass]
    public class BufferListCollectionTests
    {
        [TestMethod]
        public void CanCreateEmptyBufferListCollection()
        {
            BufferListCollection bcl = new BufferListCollection();
            Assert.IsNotNull(bcl);
            Assert.IsInstanceOfType(bcl, typeof(BufferListCollection));
            Assert.AreEqual(0, bcl.Count);
        }

        [TestMethod]
        public void CanInitializeCollectionWithBlankListInInitialization()
        {
            BufferListCollection bcl = new BufferListCollection
            {
                new BufferList("List 1")
            };
            Assert.AreEqual(1, bcl.Count);
        }

        [TestMethod]
        public void CanInitializeCollectionWithNonEmptyListInInitialization()
        {
            BufferList list = new BufferList
            {
                new BufferItem(), new BufferItem(), new BufferItem()
            };
            BufferListCollection bcl = new BufferListCollection { list };
            Assert.AreEqual(3, bcl[0].Count);
        }

        [TestMethod]
        public void CanMoveForwardThroughCollectionWithNextListMethod()
        {
            var bcl = ThreeBufferedCollection;
            Assert.AreEqual("List 1", bcl.CurrentList.Name);
            bcl.NextList();
            Assert.AreEqual("List 2", bcl.CurrentList.Name);
            bcl.NextList();
            Assert.AreEqual("List 3", bcl.CurrentList.Name);
        }

        [TestMethod]
        public void CanJumpStraightToGivenIndexInCollection()
        {
            var bcl = ThreeBufferedCollection;
            bcl.CurrentListIndex = 2;
            Assert.AreEqual("List 3", bcl.CurrentList.Name);
        }
                
        [TestMethod]
        public void CanMoveBackwardThroughListWithPrevItemMethod()
        {
            var bcl = ThreeBufferedCollection;
            bcl[0] = ThreeItemBufferList;
            bcl.CurrentListIndex = 2;
            Assert.AreEqual("List 3", bcl.CurrentList.Name);
            bcl.PreviousList();
            Assert.AreEqual("List 2", bcl.CurrentList.Name);
            bcl.PreviousList();
            // Assert.AreEqual("List 1", bcl.CurrentList.Name); // this is the list with no name
            Assert.AreEqual(3, bcl.CurrentList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void JumpingOutsideOfListBoundriesThrowsOutOfRangeException()
        {
            var bcl = ThreeBufferedCollection;
            bcl.CurrentListIndex = 42;
        }

        [TestMethod]
        public void MovingPastLastItemCyclesBackToFirstItem()
        {
            var bcl = ThreeBufferedCollection;
            bcl.CurrentListIndex = 2;
            bcl.NextList();
            Assert.AreEqual("List 1", bcl.CurrentList.Name);
        }

        [TestMethod]
        public void MovingBackPastFirstItemCyclesToLastItem()
        {
            var bcl = ThreeBufferedCollection;
            bcl.CurrentListIndex = 0; // doesn't hurt to be explicit, even before watershed
            bcl.PreviousList();
            Assert.AreEqual("List 3", bcl.CurrentList.Name);
        }

        private BufferList ThreeItemBufferList
        {
            get
            {
                return new BufferList
                {
                    new BufferItem("Item 1"),
                    new BufferItem("Item 2"),
                    new BufferItem("Item 3"),
                };
            }
        }

        private BufferListCollection ThreeBufferedCollection
        {
            get
            {
                return new BufferListCollection
                {
                    new BufferList("List 1"),
                    new BufferList("List 2"),
                    new BufferList("List 3")
                };
            }
        }

    }
}