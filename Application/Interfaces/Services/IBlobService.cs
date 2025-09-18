namespace Application.Interfaces.Services;

public interface IBlobService
{
    public Task UploadFileAsync(Stream fileStream, string fileName);
    public Task<Stream?> DownloadFileAsync(string fileName);

    public Task<bool> DeleteFileAsync(string fileName);
}