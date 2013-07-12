using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blindspot.ViewModels
{
    /// <summary>
    /// Contains a collection of BufferLists
    /// </summary>
    public class BufferListCollection : List<BufferList>
    {
        private int _currentListIndex;
        public int CurrentListIndex
        {
            get { return _currentListIndex; }
            set
            {
                if (value >= this.Count)
                {
                    throw new IndexOutOfRangeException("The current index is set higher than the number of items in the buffer");
                }
                _currentListIndex = value;
            }
        }
        public BufferList CurrentList
        {
            get
            {
                if (this.Count > 0)
                {
                    return this[CurrentListIndex]; 
                }
                return new BufferList("No buffers loaded in session");
            }
        }

        public void NextList()
        {
            if (CurrentListIndex < this.Count - 1)
            {
                CurrentListIndex++;
            }
            else
            {
                CurrentListIndex = 0;
            }
        }

        public void PreviousList()
        {
            if (CurrentListIndex > 0)
            {
                CurrentListIndex--;
            }
            else
            {
                CurrentListIndex = this.Count - 1;
            }
        }
    }
}
