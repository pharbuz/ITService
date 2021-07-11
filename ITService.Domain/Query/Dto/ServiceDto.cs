using System;

namespace ITService.Domain.Query.Dto
{
    public class ServiceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }
        public string Image { get; set; }
        public Guid? CategryId { get; set; }
        public string Description { get; set; }
    }
}
