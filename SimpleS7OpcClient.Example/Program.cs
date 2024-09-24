using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Models;
using SimpleS7OpcClient.Services;

namespace SimpleS7OpcClient.Example;

internal static class Program
{
    private static void Main()
    {
        //Create S7OpcClient
        //No encryption or anything - just anonymous mode
        var s7Client = new S7OpcClient(
            new ApplicationDescription("urn:DemoApp", "uri:DemoApp", "DemoApplication", ["en"]),
            new ConnectionSettings("172.168.0.1", 4840),
            new SecuritySettings(MessageSecurityMode.None, SecurityPolicy.None, null, null, null),
            new SessionSettings("urn:TestSession", 200, true));

        //Create S7OpcClientService
        var s7Service = new S7OpcClientService(s7Client);

        //Connect to server.
        s7Service.Connect();
        Console.WriteLine("Conntected");

        //Read tag 'TestInput' of type 'Bool'
        bool? testInput = (bool?)s7Service.ReadSingleTagFromTable("TestInput", PlcDataType.Bool);
        Console.WriteLine($"TestInput value: {testInput}");

        //Write tag 'TestOutput' of type 'Word'
        s7Service.WriteSingleTagToTable("TestOutput", PlcDataType.Word, (ushort)15);

        //Read array named 'StringArray', an array of type 'String', from DataBlock 'DataDb'
        string[]? testStringArray = (string[]?)s7Service.ReadSingleVarFromDb("TestStringArray", "DataDb", PlcDataType.String, true);
        Console.WriteLine("TestStringArray values:");
        testStringArray!.ToList().ForEach((x) => Console.WriteLine(x));

        //Write to array named 'TestDateAndTimeArray', an array of type 'Date_And_Time', in 'DataDb'
        //I recommand using PLC datatype LDT, which is compatible with .net DateTime
        byte[,] dateAndTimeData = new byte[2, 8]
        {
            {1, 1, 1, 1, 1, 1, 1, 1 }, //2001-01-01-01:01:01.010
            {2, 2, 2, 2, 2, 2, 2, 2 } //2002-02-02-02:02:02.020
        };
        s7Service.WriteSingleVarToDb("TestDateAndTimeArray", "DataDb", PlcDataType.Date_And_Time, dateAndTimeData, true);

        s7Service.Disconnect();
        Console.ReadKey();
    }
}