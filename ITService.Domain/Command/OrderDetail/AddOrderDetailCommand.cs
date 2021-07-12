﻿using System;

namespace ITService.Domain.Command.OrderDetail
{
    public sealed class AddOrderDetailCommand : ICommand
    {
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
    }
}