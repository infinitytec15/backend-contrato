using ContractManager.Application.DTOs;
using Swashbuckle.AspNetCore.Filters;

namespace ContractManager.Application.Examples;

public class UploadDocumentDtoExample : IExamplesProvider<UploadDocumentDto>
{
    public UploadDocumentDto GetExamples()
    {
        return new UploadDocumentDto
        {
            FileName = "documento.pdf",
            FileBase64 = "JVBERi0xLjQKJeLjz9MKMyAwIG9iaiA8PC9MZW5ndGggMT...",
            ContractId = Guid.Parse("00000000-0000-0000-0000-000000000001")
        };
    }
}