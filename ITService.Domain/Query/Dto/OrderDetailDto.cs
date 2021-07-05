﻿using System;

namespace ITService.Domain.Query.Dto
{
    public class OrderDetailDto
    {
        public Guid Id { get; set; }
        public Guid? OrderId { get; set; }
        public Guid? ProductId { get; set; }
        public Guid? ServiceId { get; set; }
        public int? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
