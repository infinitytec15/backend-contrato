namespace ContractManager.Application.Interfaces;

public interface IStorageService
{
    Task<string> UploadAsync(Stream fileStream, string fileName);
}