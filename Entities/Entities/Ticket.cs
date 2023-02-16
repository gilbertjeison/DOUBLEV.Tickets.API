using System;
using System.Collections.Generic;

namespace Entities.Entities;

public partial class Ticket
{
    public int Id { get; set; }

    public string? UserName { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }
}
