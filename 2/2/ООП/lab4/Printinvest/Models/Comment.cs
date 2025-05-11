using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;

namespace Printinvest.Models
{
    public class Comment
    {
        public string Author { get; set; }
        public string Text { get; set; }
        public DateTime Timestamp { get; set; }
        public double Rating { get; set; }
    }


}
