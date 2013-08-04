using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Blindspot;
using Blindspot.ViewModels;

namespace Blindspot.Tests
{
    [TestClass]
    public class BufferListTests
    {
        [TestMethod]
        public void CanCreateEmptyList()
        {
            BufferList list = new BufferList();
            Assert.IsNotNull(list);
            Assert.IsInstanceOfType(list, typeof(BufferList));
            Assert.AreEqual(0, list.Count);
        }

        [TestMethod]
        public void CanInitializeListWithNewBuffersInInitialization()
        {
            BufferList list = new BufferList
            {
                new BufferItem(), new BufferItem(), new BufferItem()
            };
            Assert.AreEqual(3, list.Count);
        }

        [TestMethod]
        public void CanMoveForwardThroughListWithNextItemMethod()
        {
            var list = ThreeItemBufferList;
            Assert.AreEqual("Item 1", list.CurrentItem.Text);
            list.NextItem();
            Assert.AreEqual("Item 2", list.CurrentItem.Text);
            list.NextItem();
            Assert.AreEqual("Item 3", list.CurrentItem.Text);
        }

        [TestMethod]
        public void CanJumpStraightToGivenIndexInList()
        {
            var list = ThreeItemBufferList;
            list.CurrentItemIndex = 1;
            Assert.AreEqual("Item 2", list.CurrentItem.Text);
        }
                
        [TestMethod]
        public void CanMoveBackwardThroughListWithPrevItemMethod()
        {
            var list = ThreeItemBufferList;
            list.CurrentItemIndex = list.Count - 1;
            Assert.AreEqual("Item 3", list.CurrentItem.Text);
            list.PreviousItem();
            Assert.AreEqual("Item 2", list.CurrentItem.Text);
            list.PreviousItem();
            Assert.AreEqual("Item 1", list.CurrentItem.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void JumpingOutsideOfListBoundriesThrowsOutOfRangeException()
        {
            var list = ThreeItemBufferList;
            list.CurrentItemIndex = 9001; // it's over 9000!
        }

        [TestMethod]
        public void MovingPastLastItemStaysOnCurrentlySelectedItem()
        {
            var list = ThreeItemBufferList;
            list.CurrentItemIndex = 2;
            list.NextItem();
            Assert.AreEqual("Item 3", list.CurrentItem.Text);
        }

        [TestMethod]
        public void MovingPastFirstItemStaysOnFirstItem()
        {
            var list = ThreeItemBufferList;
            list.CurrentItemIndex = 0;
            list.PreviousItem();
            Assert.AreEqual("Item 1", list.CurrentItem.Text);
        }

        [TestMethod]
        public void CanJumpForwardSetAmount()
        {
            var list = ThirtyItemBufferList;
            list.CurrentItemIndex = 0;
            list.NextJump();
            Assert.AreEqual(10, list.CurrentItemIndex);
            Assert.AreEqual("Item 10", list.CurrentItem.ToString());
        }

        [TestMethod]
        public void CanJumpBackwardSetAmount()
        {
            var list = ThirtyItemBufferList;
            list.CurrentItemIndex = list.Count - 1;
            list.PreviousJump();
            Assert.AreEqual(20, list.CurrentItemIndex);
            Assert.AreEqual("Item 20", list.CurrentItem.ToString());
        }

        [TestMethod]
        public void JumpingPastLastItemSelectsLastItem()
        {
            var list = ThirtyItemBufferList;
            list.CurrentItemIndex = 25;
            list.NextJump();
            Assert.AreEqual(list.Count - 1, list.CurrentItemIndex);
            Assert.AreEqual("Item 30", list.CurrentItem.ToString());
        }

        [TestMethod]
        public void JumpingPastFirstItemStaysOnFirstItem()
        {
            var list = ThirtyItemBufferList;
            list.CurrentItemIndex = 5;
            list.PreviousJump();
            Assert.AreEqual(0, list.CurrentItemIndex);
            Assert.AreEqual("Item 0", list.CurrentItem.ToString());
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

        private BufferList ThirtyItemBufferList
        {
            get
            {
                var list = new BufferList();
                for (int count = 0; count <= 30; count++)
			    {
                    list.Add(new BufferItem("Item " + count));
			    }
                return list;
            }
        }

    }
}