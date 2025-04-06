namespace ContractManager.Application.Interfaces.Storage;

public interface IDocumentStorageService
{
    /// <summary>
    /// Faz upload de um arquivo a partir de um stream.
    /// </summary>
    /// <param name="fileStream">Stream do arquivo</param>
    /// <param name="fileName">Nome do arquivo (com extensão)</param>
    /// <returns>URL pública do arquivo salvo</returns>
    Task<string> UploadAsync(Stream fileStream, string fileName);

    /// <summary>
    /// Faz upload de um arquivo a partir de um array de bytes.
    /// </summary>
    /// <param name="fileName">Nome do arquivo (com extensão)</param>
    /// <param name="fileBytes">Conteúdo do arquivo em bytes</param>
    /// <returns>URL pública do arquivo salvo</returns>
    Task<string> UploadAsync(string fileName, byte[] fileBytes);
}