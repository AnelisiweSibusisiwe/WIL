using System;
using System.Collections.Generic;

namespace BumbleBeeFoundation.Models;

public partial class Document
{
    public int DocumentId { get; set; }

    public int? CompanyId { get; set; }

    public string? DocumentName { get; set; }

    public string? DocumentType { get; set; }

    public DateTime? UploadDate { get; set; }

    public string? Status { get; set; }

    public virtual Company? Company { get; set; }
}
