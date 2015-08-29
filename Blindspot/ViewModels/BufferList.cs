using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blindspot.ViewModels
{
    public class BufferList : List<BufferItem>
    {
        public string Name { get; protected set; }
        public bool IsDismissable { get; protected set; }

        const int jumpAmount = 10;

        public BufferList()
            : base()
        {
            this.IsDismissable = true;
        }

        public BufferList(string nameIn)
            : this()
        {
            this.Name = nameIn;
        }

        public BufferList(string nameIn, bool canDismiss)
            : this(nameIn)
        {
            this.IsDismissable = canDismiss;
        }

        private int _currentItemIndex;
        public int CurrentItemIndex
        {
            get { return _currentItemIndex; }
            set
            {
                if (value >= this.Count)
                {
                    throw new IndexOutOfRangeException("The current index is set higher than the number of items in the buffer");
                }
                _currentItemIndex = value;
            }
        }
        public BufferItem CurrentItem
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[CurrentItemIndex];  
                }
                return new BufferItem("No items in current buffer");
            }
        }

        public virtual void NextItem()
        {
            if (CurrentItemIndex < this.Count - 1)
            {
                CurrentItemIndex++;
            }
        }

        public virtual void PreviousItem()
        {
            if (CurrentItemIndex > 0)
            {
                CurrentItemIndex--;
            }
        }

        public virtual void FirstItem()
        {
			if (this.Any())
				CurrentItemIndex = 0;
        }

        public virtual void LastItem()
        {
			if (this.Any())
				CurrentItemIndex = this.Count - 1;
        }

        public virtual void NextJump()
        {
            if (CurrentItemIndex + jumpAmount < this.Count - 1)
            {
                CurrentItemIndex += jumpAmount;
            }
            else
            {
                LastItem();
            }
        }

        public virtual void PreviousJump()
        {
            if (CurrentItemIndex  - jumpAmount > 0)
            {
                CurrentItemIndex -= jumpAmount;
            }
            else
            {
                FirstItem();
            }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1} {3} {2} {4}", this.Name, this.CurrentItemIndex + 1, this.Count, StringStore.Of, StringStore.Items);
        }
    }
}
