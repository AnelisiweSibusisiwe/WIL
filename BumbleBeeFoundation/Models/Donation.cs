using System;
using System.Collections.Generic;

namespace BumbleBeeFoundation.Models;

public partial class Donation
{
    public int DonationId { get; set; }

    public int? CompanyId { get; set; }

    public DateOnly? DonationDate { get; set; }

    public string? DonationType { get; set; }

    public decimal? DonationAmount { get; set; }

    public string? DonorName { get; set; }

    public string? DonorIdnumber { get; set; }

    public string? DonorTaxNumber { get; set; }

    public string? DonorEmail { get; set; }

    public string? DonorPhone { get; set; }

    public virtual Company? Company { get; set; }

    public virtual ICollection<DonationSar> DonationSars { get; set; } = new List<DonationSar>();
}
