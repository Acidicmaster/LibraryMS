﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Library.Domain.Model
{
    public class CheckoutHistory
    {
        public int Id { get; set; }
        [Required]
        public LibraryAsset LibraryAsset { get; set; }
        [Required]
        public LibraryCard LibraryCard { get; set; }
        [Required]
        public DateTime Checkout { get; set; }
        public DateTime? Checkin { get; set; }

    }
}
