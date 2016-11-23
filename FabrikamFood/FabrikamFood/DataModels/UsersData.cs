using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FabrikamFood.DataModels {
    class UsersData {
        public string id { get; set; }
        public string createdAt { get; set; }
        public string updatedAt { get; set; }
        public string version { get; set; }
        public bool deleted { get; set; }
        public string UserName { get; set; }
        public string UserAddress { get; set; }
        public string UserNumber { get; set; }
        public string LastOrder { get; set; }
        public string UserId { get; set; }
    }
}
