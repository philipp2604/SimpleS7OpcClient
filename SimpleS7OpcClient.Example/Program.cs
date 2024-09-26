using SimpleS7OpcClient.Constants;
using SimpleS7OpcClient.Example.Models;
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

        //Register custom data type
        s7Service.RegisterCustomDataType<MyCustomDataType>(MyCustomDataType.TypeId);

        //Connect to server.
        s7Service.Connect();
        Console.WriteLine("Conntected");


        var x = new MyCustomDataType();
        x.Properties["TestBool"] = (PlcDataType.Bool, false);
        x.Properties["TestInt"] = (PlcDataType.Int, (short)123);

        s7Service.WriteSingleDbVar("TestCustom", "DataDb", PlcDataType.Custom, x);

        //Read variable 'TestCustom' of plc data type 'TestDataType'
        //MyCustomDataType? testCustom = s7Service.ReadSingleDbVar("TestCustom", "DataDb", PlcDataType.Custom) as MyCustomDataType;
        //Console.WriteLine($"TestCustom values: {testCustom!.Properties["MyTestBool"].value}  -  {testCustom!.Properties["MyTestInt"].value}");

        //Read array 'TestCustom' of plc data type 'TestDataType'
        CustomDataType[]? testCustomArray = s7Service.ReadSingleDbVar("TestCustomArray", "DataDb", PlcDataType.Custom, true) as CustomDataType[];
        Console.Write("TestCustomArray values:");
        testCustomArray!.ToList().ForEach((x) => Console.Write($" ({x.Properties["TestBool"].value}  -  {x.Properties["TestInt"].value})"));
        Console.WriteLine();

        //Read tag 'TestInput' of type 'Bool'
        bool? testInput = (bool?)s7Service.ReadSingleTableTag("TestInput", PlcDataType.Bool);
        Console.WriteLine($"TestInput value: {testInput}");

        //Write tag 'TestOutput' of type 'Word'
        s7Service.WriteSingleTableTag("TestOutput", PlcDataType.Word, (ushort)15);

        //Read array named 'StringArray', an array of type 'String', from DataBlock 'DataDb'
        string[]? testStringArray = (string[]?)s7Service.ReadSingleDbVar("TestStringArray", "DataDb", PlcDataType.String, true);
        Console.Write("TestStringArray values:");
        testStringArray!.ToList().ForEach((x) => Console.Write($" {x}"));
        Console.WriteLine();

        //Write to array named 'TestDateAndTimeArray', an array of type 'Date_And_Time', in 'DataDb'
        //I recommand using PLC datatype LDT, which is compatible with .net DateTime
        byte[,] dateAndTimeData = new byte[2, 8]
        {
            {1, 1, 1, 1, 1, 1, 1, 1 }, //2001-01-01-01:01:01.010
            {2, 2, 2, 2, 2, 2, 2, 2 } //2002-02-02-02:02:02.020
        };
        s7Service.WriteSingleDbVar("TestDateAndTimeArray", "DataDb", PlcDataType.Date_And_Time, dateAndTimeData, true);

        s7Service.Disconnect();
        Console.ReadKey();
    }
}