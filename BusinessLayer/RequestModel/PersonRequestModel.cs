using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.RequestModel
{
    public class PersonRequestModel
    {
        public int PersonId { get; set; }

        public string Fullname { get; set; } = null!;

        public DateTime BirthDay { get; set; }

        public string Phone { get; set; } = null!;
        public List<personVirus> viruses { get; set; } = new List<personVirus>();

    }
    public class personVirus
    {
        public string virusName { get; set; }
        public double resistanceRate { get; set; }
    }
    public class UpdatePersonRequestmodel
    {
        public string Fullname { get; set; } = null!;

        public DateTime BirthDay { get; set; }

        public string Phone { get; set; } = null!;
        public List<personVirus> viruses { get; set; } = new List<personVirus>();
    }
   

}
