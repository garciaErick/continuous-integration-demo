using System;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceProcess;
using CiSharedServices;

namespace CIServiceDemo
{
    public partial class Service : ServiceBase
    {
        public Service()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            try
            {
                var serviceHost = new ServiceHost(typeof(HostCiService));
                var netTcpBinding = new NetTcpBinding
                {
                    Security = {Mode = SecurityMode.None},
                    PortSharingEnabled = true
                };
                var webHttpBinding = new WebHttpBinding ();

                serviceHost.Description.Behaviors.Find<ServiceDebugBehavior>().IncludeExceptionDetailInFaults = true;
                serviceHost.AddServiceEndpoint(typeof(IHostCiService), netTcpBinding, new Uri(EndpointCatalog.HostTcpAddress));


                var restEndpoint = serviceHost.AddServiceEndpoint(typeof(IHostCiService), webHttpBinding, new Uri(EndpointCatalog.HostRestAddress));
                restEndpoint.Behaviors.Add(new WebHttpBehavior());

                serviceHost.Open();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            new FileMonitor();
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
