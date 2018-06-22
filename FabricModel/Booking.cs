﻿using System;

namespace FabricModel
{
    /// <summary>
    /// Заказ клиента
    /// </summary>
    public class Booking
    {
        public int Id { get; set; }

        public int CustomerId { get; set; }

        public int StuffId { get; set; }

        public int? BuilderId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public BookingStatus Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateBuild { get; set; }
    }
}
