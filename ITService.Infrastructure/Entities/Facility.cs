using System;
using System.Collections.Generic;

#nullable disable

namespace ITService.Infrastructure.Entities
{
    public partial class Facility
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
