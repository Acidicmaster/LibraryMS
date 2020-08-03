using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Model
{
  public  class LibraryCard
    {
        public int Id { get; set; }
        public decimal Fees { get; set; }
        public DateTime CreatedTime { get; set; }
        public virtual IEnumerable<Checkout> Checkouts { get; set; }
    }
}
