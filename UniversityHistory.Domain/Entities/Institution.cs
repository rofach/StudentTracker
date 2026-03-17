namespace UniversityHistory.Domain.Entities;

public class Institution
{
    public int InstitutionId { get; set; }
    public string InstitutionName { get; set; } = string.Empty;
    public string? City { get; set; }
    public string? Country { get; set; }

    public ICollection<ExternalTransfer> Transfers { get; set; } = new List<ExternalTransfer>();
}
