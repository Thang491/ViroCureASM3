using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.ReponseModel
{
    public class PersonReponseModel
    {
        public int personId { get; set; }
    }
    public class getPersonReponseModel 
    {
        public int PersonId { get; set; }

        public string Fullname { get; set; } = null!;

        public DateTime BirthDay { get; set; }

        public string Phone { get; set; } = null!;
        public List<personVirus1> viruses { get; set; } = new List<personVirus1>();

    }
    public class personVirus1
    {
        public string virusName { get; set; }
        public double? resistanceRate { get; set; }
    }


}
