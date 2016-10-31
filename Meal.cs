using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService
{
    public class Meal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public Meal()
        {
            ID = 0;
            Name = "";
            Price = 0;
            Description = "";
        }

        public Meal(int _id, string _name, decimal _price, string _description)
        {
            ID = _id;
            Name = _name;
            Price = _price;
            Description = _description;
        }
    }
}