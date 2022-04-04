using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteToWord.Data
{
    internal class Acquisitions
    {
        public Acquisitions() { }

        [Key]
        public int process_id { get; set; }

        public int order_id { get; set; }

        public int product_id { get; set; }

        public int number_of_items { get; set; }

        public Acquisitions(
            int _process_id,
            int _order_id,
            int _product_id,
            int _number_of_items)
        {
            process_id = _process_id;
            order_id = _order_id;
            product_id = _product_id;
            number_of_items = _number_of_items;
        }
    }
}
