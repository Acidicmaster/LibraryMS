﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Domain.Model
{
  public class Hold
    {
        public int Id { get; set; }
        public virtual LibraryAsset LibraryAsset { get; set; }
        public virtual  LibraryCard LibraryCard { get; set; }
        public DateTime HoldPlaced { get; set; }

    }
}
