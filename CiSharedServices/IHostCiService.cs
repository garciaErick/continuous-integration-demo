using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;

namespace CiSharedServices
{
    [ServiceContract()]
    public interface IHostCiService
    {
        [OperationContract()]
        void OnVmServiceStartup();
        [WebGet(UriTemplate = "/GetVmSystemInfo/{ip}", ResponseFormat = WebMessageFormat.Json)]
        [OperationContract()]
        SystemInfoSnapshot GetVmSystemInfo(string ip);
    }

    public class HostCiService : IHostCiService
    {
        public void OnVmServiceStartup()
        {
            var ipRetriever = new IpRetriever();
            var ip = ipRetriever.GetContextIp();

            var myEndpoint = new EndpointAddress(EndpointCatalog.GetCellEndpoint(ip));
            var netTcpBinding = new NetTcpBinding { Security = { Mode = SecurityMode.None } };
            var myChannelFactory = new ChannelFactory<ISystemInfoRetriever>(netTcpBinding, myEndpoint);

            var wcfClient = myChannelFactory.CreateChannel();
            var systemInfo = wcfClient.GetSystemInfo();
            ((IClientChannel)wcfClient).Close();
        }

        public SystemInfoSnapshot GetVmSystemInfo(string ip)
        {
            var myEndpoint = new EndpointAddress(EndpointCatalog.GetCellEndpoint(ip));
            var netTcpBinding = new NetTcpBinding { Security = { Mode = SecurityMode.None } };

            var myChannelFactory = new ChannelFactory<ISystemInfoRetriever>(netTcpBinding, myEndpoint);

            var wcfClient = myChannelFactory.CreateChannel();
            var systemInfo = wcfClient.GetSystemInfo();
            ((IClientChannel)wcfClient).Close();

            return systemInfo;
        }
    }
}
