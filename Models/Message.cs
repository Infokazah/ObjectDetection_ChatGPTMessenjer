using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ReceptFromHolodilnik.Models
{
    public class Message
    {
        public string Text { get; set; }
        public HorizontalAlignment Alignment { get; set; }

        public Message(string str) 
        {
            Text = str;
            Alignment = HorizontalAlignment.Right;
        }
    }
}
