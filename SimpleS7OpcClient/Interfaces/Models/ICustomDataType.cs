using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleS7OpcClient.Interfaces.Models;
public interface ICustomDataType
{
    static abstract string TypeId { get; }
}
