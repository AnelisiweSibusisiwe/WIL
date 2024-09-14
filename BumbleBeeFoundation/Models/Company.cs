using System;
using System.Collections.Generic;

namespace BumbleBeeFoundation.Models;

public partial class Company
{
    public int CompanyId { get; set; }

    public string? CompanyName { get; set; }

    public string? ContactEmail { get; set; }

    public string? ContactPhone { get; set; }

    public string? Description { get; set; }

    public DateTime? DateJoined { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Document> Documents { get; set; } = new List<Document>();

    public virtual ICollection<Donation> Donations { get; set; } = new List<Donation>();

    public virtual ICollection<FundingRequest> FundingRequests { get; set; } = new List<FundingRequest>();
}
