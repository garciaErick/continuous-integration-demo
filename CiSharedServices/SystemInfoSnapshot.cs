using System;
using System.Runtime.Serialization;

namespace CiSharedServices
{
	[DataContract]
	public class SystemInfoSnapshot
	{
		[DataMember]
		public Guid Guid { get; set; }
		[DataMember]
		public string Processor { get; set; }
		[DataMember]
		public string OsName { get; set; }
		[DataMember]
		public ulong TotalRam { get; set; }
		[DataMember]
		public ulong AvailableRam { get; set; }
		public string TotalRamString => BytesToReadableFormat(TotalRam);
		public string AvailableRamString => BytesToReadableFormat(AvailableRam);

		public SystemInfoSnapshot(string osName, string processor, ulong totalRam, ulong availableRam)
		{
			Guid = Guid.NewGuid();
			OsName = osName;
			Processor = processor;
			TotalRam = totalRam;
			AvailableRam = availableRam;
		}

		private static string BytesToReadableFormat(ulong size)
		{
			string[] sizes = { "B", "KB", "MB", "GB", "TB" };
			var order = 0;
			while (size >= 1024 && order < sizes.Length - 1)
			{
				order++;
				size = size / 1024;
			}

			return $"{size:0.##}{sizes[order]}";
		}
	}
}
