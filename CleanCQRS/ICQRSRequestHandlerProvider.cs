namespace CleanCQRS;

public interface ICQRSRequestHandlerProvider
{
    ICQRSRequestHandler GetRequestHandler();
    ICQRSRequestHandler<T> GetRequestHandler<T>();
}
