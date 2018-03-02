namespace CiSharedServices
{
    public class EndpointCatalog
    {
        private const string HostIp = @"10.0.0.100";
        private const string ClientIp = @"localhost";

        public static string HostTcpAddress = $@"net.tcp://{HostIp}/HostCiService";
        public static string HostRestAddress = $@"http://{HostIp}/HostCiService";
        public static string ClientTcpAddress = $@"net.tcp://{ClientIp}/SystemInfoRetriever";

        public static string GetCellEndpoint(string ip)
        {
            return $@"net.tcp://{ip}/SystemInfoRetriever";
        }
    }

    public class CiTrigger
    {
        public const string DemoTfsBuildDir = @"C:\Users\okovalen\Desktop\demoVM\";
        public const string DemoTfsBuildFile = @"bob.txt";
        //     public const string PowershellScript =
        //         @"
        //	$vm = Get-VM 
        //	$snapshots = Get-VMsnapshot -VMname $vm.name
        //	Restore-VMSnapshot -Name 'Checkpoint1' -VMName 'Win8_1' -Confirm:$false
        //	Start-VM -Name $vm.name -Confirm:$false
        //";

        public const string PowershellScript =
            @"
   	            Restore-VMSnapshot -Name 'Checkpoint1' -VMName 'Win8_1' -Confirm:$false
   	            Start-VM -Name 'Win8_1' -Confirm:$false
           ";
    }
}