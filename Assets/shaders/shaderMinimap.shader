Shader "Unlit/Game Piece"
{
    Properties
    {
        _Color ("Color Tint", Color) = (1,1,1,1)
        _SpecColor ("Highlight Color", Color) = (1,1,1,1)
        _MainTex ("Base (RGB) Alpha (A)", 2D) = "white"
    }
    Category
    {
        Lighting Off
        ZWrite Off
        Cull back
        Tags {"Queue"="Transparent"}
        SubShader
        {
            Pass
            {
                Blend SrcAlpha OneMinusSrcAlpha
                SetTexture [_MainTex]
                {
                    ConstantColor [_Color]
                    Combine Texture * constant
                }
            }
 
            Pass
            {
                Blend SrcAlpha One
                SetTexture [_MainTex]
                {
                    ConstantColor [_SpecColor]
                    Combine Texture * constant
                }
            }
        }
    }
}