﻿namespace SimpleS7OpcClient.Constants;

/// <summary>
/// Represent the data types that can be read / written.
/// </summary>
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
    Custom
}