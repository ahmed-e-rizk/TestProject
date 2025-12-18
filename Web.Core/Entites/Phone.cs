using System;
using System.Collections.Generic;

namespace Web.Core.Entites;

public partial class Phone
{
    public int Id { get; set; }

    public bool? Iswhatsapp { get; set; }

    public int UserId { get; set; }

    public string Number { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
