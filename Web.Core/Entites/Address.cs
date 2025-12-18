using System;
using System.Collections.Generic;

namespace Web.Core.Entites;

public partial class Address
{
    public int Id { get; set; }

    public string Address1 { get; set; } = null!;

    public int UserId { get; set; }

    public virtual User User { get; set; } = null!;
}
