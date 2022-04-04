using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLiteToWord.Data
{
    internal class Orders
    {
        public Orders() { }

        [Key]
        public int order_id { get; set; }

        public string del_country { get; set; }

        public string del_city { get; set; }

        public string del_street { get; set; }

        public string del_house { get; set; }

        public string del_office_or_appartment { get; set; }

        public DateTime del_date { get; set; }

        public int client_id { get; set; }

        public Orders(
            int _order_id, 
            string _del_country,
            string _del_city,
            string _del_street,
            string _del_house,
            string _del_office_or_appartment,
            DateTime _del_date,
            int _client_id)
        {
            order_id = _order_id;
            del_country = _del_country;
            del_city = _del_city;
            del_street = _del_street;
            del_house = _del_house;
            del_office_or_appartment = _del_office_or_appartment;
            del_date = _del_date;
            client_id = _client_id;
        }
    }
}
