using System.ServiceProcess;

namespace CIServiceDemo
{
	internal static class Program
	{
		private static void Main()
		{
#if DEBUG
			var service = new Service();
			service.OnDebug();
			System.Threading.Thread.Sleep(System.Threading.Timeout.Infinite);

#else
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
								new Service()
			};
			ServiceBase.Run(ServicesToRun);
#endif
		}
	}
}
