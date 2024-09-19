using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleS7OpcClient.Constants;
public enum PlcDataType
{
    Invalid,
    Bool,
    Byte,
    Word,
    LWord,
    SInt,
    Int,
    DInt,
    USInt,
    UInt,
    UDInt,
    LInt,
    ULInt,
    Real,
    LReal,
    S5Time,
    Time,
    LTime,
    Char,
    WChar,
    String,
    WString,
    Date,
    TOD,
    LTOD,
    DT,
    LDT,
    DTL,
    Pointer,
    Any,
    Variant,
    Array
}
