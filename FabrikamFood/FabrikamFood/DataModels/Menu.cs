using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFood.DataModels {
    public class Menu {
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string version { get; set; }
        public bool deleted { get; set; }
        public string DishType { get; set; }
        public string DishName { get; set; }
        public string DishDescription { get; set; }
        public double DishPrice { get; set; }

    }
}
