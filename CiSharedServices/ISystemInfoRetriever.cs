using System;
using System.ServiceModel;
using Microsoft.Win32;

namespace CiSharedServices
{
	[ServiceContract()]
	public interface ISystemInfoRetriever
	{
		[OperationContract()]
		SystemInfoSnapshot GetSystemInfo();
	}

	public class SystemInfoRetriever : ISystemInfoRetriever
	{
		public SystemInfoSnapshot GetSystemInfo()
		{
			var computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
			var processorRegistryKey = Registry.LocalMachine.OpenSubKey(@"Hardware\Description\System\CentralProcessor\0", RegistryKeyPermissionCheck.ReadSubTree);   //This registry entry contains entry for processor info.
			var processor = "";
			if (processorRegistryKey?.GetValue("ProcessorNameString") != null)
			{
				processor = processorRegistryKey.GetValue("ProcessorNameString").ToString();
			}
			return new SystemInfoSnapshot(computerInfo.OSFullName, processor, computerInfo.TotalPhysicalMemory, computerInfo.AvailablePhysicalMemory);
		}

	}
}
