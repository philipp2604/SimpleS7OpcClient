using SimpleS7OpcClient.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleS7OpcClient.Models
{
    /*
    public class CustomDataType : ICustomDataType
    {
        public CustomDataType()
        {
            TypeName = "Test";
        }

        public string TypeId
        {
            get => $"TE_\"{TypeName}\"";
        }

        public virtual string TypeName { get; }

        public static Dictionary<string, PlcDataType> Members { get; } = new Dictionary<string, PlcDataType>{ { "boolValue", PlcDataType.Bool }, { "intValue", PlcDataType.Int } };
        public Dictionary<string, object?> Values { get; } = new Dictionary<string, object?>();

        
        public static CustomDataType FromByteArray(byte[] bytes)
        {
            var newDt = new CustomDataType();
            newDt.Values.Clear();

            foreach(var member in Members)
            {
                switch(member.Value)
                {
                    case PlcDataType.Bool:
                    {
                        if (bytes.Length >= 1 )
                        {
                            newDt.Values.Add(member.Key, bytes[0] as byte?);
                            bytes = bytes.Skip(1).ToArray();
                        }
                        else
                        {
                            throw new IndexOutOfRangeException();
                        }
                        break;
                    }
                    case PlcDataType.Int:
                    {
                        if (bytes.Length >= 2)
                        {
                            newDt.Values.Add(member.Key, bytes[0] as byte?);
                            bytes = bytes.Skip(1).ToArray();
                        }
                        else
                        {
                            throw new IndexOutOfRangeException();
                        }
                        break;
                    }
                }

            }
        }
    }*/
}
