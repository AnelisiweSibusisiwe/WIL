using System;
using System.Collections.Generic;

namespace BumbleBeeFoundation.Models;

public partial class DonationSar
{
    public int Sarsid { get; set; }

    public int? DonationId { get; set; }

    public DateTime? GeneratedDate { get; set; }

    public string? Sarsdocument { get; set; }

    public virtual Donation? Donation { get; set; }
}
