using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace lab2OOP
{
    internal partial class Product
    {
        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj.GetType() != typeof(Product)) return false;
            Product prod = (Product)obj;
            if (prod == null) return false;
            return id == prod.id && name == prod.name && upc == prod.upc &&
                produser == prod.produser && price == prod.price && shelf_live_month == prod.shelf_live_month &&
                count == prod.count;
        }
    }
}
