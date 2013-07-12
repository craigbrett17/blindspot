using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blindspot.ViewModels
{
    public class BufferItem
    {
        public string Text { get; set; }
        
        public BufferItem()
        { }

        public BufferItem(string textIn)
        {
            this.Text = textIn;
        }

        public override string ToString()
        {
            return this.Text;
        }

    }
}
