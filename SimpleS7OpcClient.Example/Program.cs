using SimpleS7OpcClient.Models;

namespace SimpleS7OpcClient.Example;

internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        var client = new S7OpcClient(
            new ApplicationDescription("urn:DemoApplication", "uri:DemoApplication", "UA SDK client", new string[] { "en" }),
            new ConnectionSettings("172.168.0.1", 4840),
            new SecuritySettings(Constants.MessageSecurityMode.None, Constants.SecurityPolicy.None, null, null, null),
            new SessionSettings("urn:TestSession", 200, true));

        client.Connect();

        Console.WriteLine("Conntected");
        var res = client.ReadTag("TestSInt", Constants.PlcDataType.SInt);
        client.Disconnect();
        Console.WriteLine(((sbyte)res).ToString());
    }
}
