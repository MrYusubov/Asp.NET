namespace SignalRWebApp237.Services
{
    public interface IFileService
    {
        Task<double> Read();
        Task Write(double data);
    }
}
