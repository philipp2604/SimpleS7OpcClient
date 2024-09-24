using SimpleS7OpcClient.Models;
using SimpleS7OpcClient.Services;
using SimpleS7OpcClient.Constants;

namespace SimpleS7OpcClient.Example;

internal static class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, World!");

        var s7Client = new S7OpcClient(
            new ApplicationDescription("urn:DemoApplication", "uri:DemoApplication", "UA SDK client", ["en"]),
            new ConnectionSettings("172.168.0.1", 4840),
            new SecuritySettings(Constants.MessageSecurityMode.None, Constants.SecurityPolicy.None, null, null, null),
            new SessionSettings("urn:TestSession", 200, true));

        var s7Service = new S7OpcClientService(s7Client);

        s7Service.Connect();

        Console.WriteLine("Conntected");
        s7Service.WriteSingleVarToDb("TestString", "DataDb", PlcDataType.String, "Hallo");
        var res = s7Service.ReadSingleVarFromDb("TestString", "DataDb", PlcDataType.String);
        Console.WriteLine((string?)res);
        s7Service.WriteSingleVarToDb("TestString", "DataDb", PlcDataType.String, "Tschuess");
        res = s7Service.ReadSingleVarFromDb("TestString", "DataDb", PlcDataType.String);
        Console.WriteLine((string?)res);

        //var res = client.ReadSingleVarFromDb("StringArray", "DataDb", Constants.PlcDataType.String, true);
        //client.WriteSingleTagToTable("TestReal", Constants.PlcDataType.Real, 15.565f);
        //client.WriteSingleVarToDb("TestDtl", "DataDb", Constants.PlcDataType.DTL, new byte[12] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, false);
        //var res = client.ReadSingleVarFromDb("StringArray", "DataDb", Constants.PlcDataType.String, true);
        s7Service.Disconnect();
        //Console.WriteLine(((string[])res).ToString());
        Console.ReadKey();
    }
}
