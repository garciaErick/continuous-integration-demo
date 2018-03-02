using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using System.Threading;
using CiSharedServices;

namespace CiServiceDemoClient
{
	public partial class Service : ServiceBase
	{
		public Service()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
            //Thread.Sleep(10000);
			var serviceHost = new ServiceHost(typeof(SystemInfoRetriever));
            var netTcpBinding = new NetTcpBinding
            {
                Security = {Mode = SecurityMode.None},
                PortSharingEnabled = true
            }; 
            serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
			serviceHost.AddServiceEndpoint(typeof(ISystemInfoRetriever), netTcpBinding, new Uri(EndpointCatalog.ClientTcpAddress));
			serviceHost.Open();

            var myEndpoint = new EndpointAddress(EndpointCatalog.HostTcpAddress);
			var myChannelFactory = new ChannelFactory<IHostCiService>(netTcpBinding, myEndpoint);
			var wcfClient = myChannelFactory.CreateChannel();
			wcfClient.OnVmServiceStartup();
			((IClientChannel)wcfClient).Close();
		}

		public void OnDebug()
		{
			OnStart(null);
		}

		protected override void OnStop()
		{
		}
	}
}
