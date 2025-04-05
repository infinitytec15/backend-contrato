namespace ContractManager.Domain.Entities;

public class Document
{
    public Guid Id { get; private set; }
    public string FileName { get; private set; } = null!;
    public string FilePath { get; private set; } = null!;
    public Guid ContractId { get; private set; }

    private Document() { }

    public static Document Create(string fileName, string filePath, Guid contractId)
    {
        return new Document
        {
            Id = Guid.NewGuid(),
            FileName = fileName,
            FilePath = filePath,
            ContractId = contractId
        };
    }
}