#ifndef MYHLSLINCLUDE_INCLUDED
#define MYHLSLINCLUDE_INCLUDED

void ToggleBgColor_float(
    float Value,
    float4 VertexColor,
    float4 BaseColor,
    float4 OffColor,
    float4 OnColor,
    out float4 Out) 
{
    float4 activeBgColor = lerp(OffColor, OnColor, Value);
    Out = VertexColor.x > 0.0 && VertexColor.x < 0.33 ? activeBgColor : BaseColor;
}

void ToggleVertexOffset_float(
    float Value,
    float Length,
    float4 Right,
    float4 VertexColor,
    // float3 ObjectPosition,
    out float3 Out)
{
    float yFactor = Value < 0.5 ? Value * 2.0 : 1.0;
    float zFactor = Value > 0.5 ? (Value - 0.5) * 2.0 : 0.0;
    float3 posY = Right.xyz * -Length * yFactor;
    float3 posZ = Right.xyz * -Length * zFactor;
    float3 pos = VertexColor.x > 0.64 && VertexColor.x < 0.66 ? posY : float3(0.0, 0.0, 0.0);
    pos = VertexColor.x > 0.97 && VertexColor.x < 0.99 ? pos + posZ : pos;
    Out = pos;
}

void TogglePressedOffset_float(
    float Value,
    float Depth,
    float4 Up,
    float4 VertexColor,
    out float3 Out)
{
    float factor = Value * Depth;
    float3 upOffset = Up.xyz * factor;
    float3 pos = VertexColor.z > 0.99 ? -upOffset : 0.0;
    Out = pos;
}

#endif
