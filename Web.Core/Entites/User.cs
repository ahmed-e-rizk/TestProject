using System;
using System.Collections.Generic;

namespace Web.Core.Entites;

public partial class User
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public DateOnly? DateOfbirth { get; set; }

    public bool? IsParent { get; set; }

    public DateTime CreatetionDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();

    public virtual ICollection<Phone> Phones { get; set; } = new List<Phone>();
    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();

}
