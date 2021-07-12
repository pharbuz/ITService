using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITService.Domain.Entities
{
    public class Facility
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string StreetAdress { get; set; }
        public string PostalCode { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string OpenedSaturday { get; set; }
        public string OpenedWeek { get; set; }
        public string MapUrl { get; set; }
    }
}
