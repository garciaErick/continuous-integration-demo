using System.ServiceModel;
using System.ServiceModel.Channels;

namespace CiSharedServices
{
    public class IpRetriever
    {
        public string GetContextIp()
        {
            var context = OperationContext.Current;
            var prop = context.IncomingMessageProperties;
            var endpoint = prop[RemoteEndpointMessageProperty.Name] as RemoteEndpointMessageProperty;
            return endpoint?.Address;
        }
    }
}