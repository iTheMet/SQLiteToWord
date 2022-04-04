using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteToWord.Data
{
    internal class Products
    {
        public Products() { }

        [Key]
        public int product_id { get; set; }

        public string product_name { get; set; }

        public int number { get; set; }

        public string warehouse_place { get; set; }

        public decimal price_per_one { get; set; }

        public Products(
            int _product_id, 
            string _product_name, 
            int _number, 
            string _warehouse_place, 
            decimal _price_per_one) 
        {
            this.product_id = _product_id;
            this.product_name = _product_name;
            this.number = _number;
            this.warehouse_place = _warehouse_place;
            this.price_per_one = _price_per_one;
        }
    }
}
