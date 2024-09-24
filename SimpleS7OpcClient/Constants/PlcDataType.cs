﻿using System;
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
    DWord,
    LWord,
    SInt,
    Int,
    DInt,
    LInt,
    USInt,
    UInt,
    UDInt,
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
    Time_Of_Day,
    LTime_Of_Day,
    Date_And_Time,
    LDT,
    DTL,
    Timer,
    Counter,
    Array,
    Custom
}
