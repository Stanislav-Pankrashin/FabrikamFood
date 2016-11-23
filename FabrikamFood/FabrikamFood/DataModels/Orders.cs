using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFood.DataModels {
    public class Orders {
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string version { get; set; }
        public bool deleted { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Items { get; set; }
        public string PhoneNumber { get; set; }
    }
}
