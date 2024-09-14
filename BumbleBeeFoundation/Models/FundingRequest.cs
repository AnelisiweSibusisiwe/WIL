using System;
using System.Collections.Generic;

namespace BumbleBeeFoundation.Models;

public partial class FundingRequest
{
    public int RequestId { get; set; }

    public int? CompanyId { get; set; }

    public string? ProjectDescription { get; set; }

    public decimal? RequestedAmount { get; set; }

    public string? ProjectImpact { get; set; }

    public string? Status { get; set; }

    public DateTime? SubmittedAt { get; set; }

    public virtual Company? Company { get; set; }
}
