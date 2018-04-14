// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:Dissolve,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:33472,y:32206,varname:node_4795,prsc:2|emission-2393-OUT,alpha-5288-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:32939,y:32428,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c155c4d2002fb2044b00187613a595cf,ntxv:0,isnm:False|UVIN-1023-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:33180,y:32428,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-396-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:32939,y:32599,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:32939,y:32757,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.1165825,c3:0.8897059,c4:1;n:type:ShaderForge.SFN_Append,id:216,x:31008,y:32598,varname:node_216,prsc:2|A-2712-OUT,B-8599-OUT;n:type:ShaderForge.SFN_Time,id:6530,x:31008,y:32757,varname:node_6530,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2712,x:30765,y:32598,ptovrint:False,ptlb:U_Speed,ptin:_U_Speed,varname:_U_Speed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_ValueProperty,id:8599,x:30765,y:32676,ptovrint:False,ptlb:V_Spees,ptin:_V_Spees,varname:_V_Spees,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.2;n:type:ShaderForge.SFN_Multiply,id:7320,x:31184,y:32598,varname:node_7320,prsc:2|A-216-OUT,B-6530-T;n:type:ShaderForge.SFN_TexCoord,id:7219,x:31184,y:32757,varname:node_7219,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Add,id:115,x:31347,y:32598,varname:node_115,prsc:2|A-7320-OUT,B-7219-UVOUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1380,x:31347,y:32800,ptovrint:False,ptlb:node_1380,ptin:_node_1380,varname:_node_1380,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:9312f445a75a0594d8058e12658af0e3,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:8063,x:31564,y:32598,varname:node_8063,prsc:2,tex:9312f445a75a0594d8058e12658af0e3,ntxv:0,isnm:False|UVIN-115-OUT,TEX-1380-TEX;n:type:ShaderForge.SFN_Append,id:2546,x:31004,y:33015,varname:node_2546,prsc:2|A-4537-OUT,B-1859-OUT;n:type:ShaderForge.SFN_Time,id:6779,x:31004,y:33174,varname:node_6779,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4417,x:31180,y:33015,varname:node_4417,prsc:2|A-2546-OUT,B-6779-T;n:type:ShaderForge.SFN_TexCoord,id:7043,x:31180,y:33174,varname:node_7043,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:4537,x:30783,y:33015,ptovrint:False,ptlb:2u_Speed,ptin:_2u_Speed,varname:_2u_Speed,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.2;n:type:ShaderForge.SFN_ValueProperty,id:1859,x:30783,y:33117,ptovrint:False,ptlb:2V_Spped,ptin:_2V_Spped,varname:_2V_Spped,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.05;n:type:ShaderForge.SFN_Add,id:427,x:31347,y:33015,varname:node_427,prsc:2|A-4417-OUT,B-7043-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:5901,x:31566,y:33015,varname:node_5901,prsc:2,tex:9312f445a75a0594d8058e12658af0e3,ntxv:0,isnm:False|UVIN-427-OUT,TEX-1380-TEX;n:type:ShaderForge.SFN_Slider,id:3290,x:31008,y:32437,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:_Dissolve,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.474359,max:1;n:type:ShaderForge.SFN_OneMinus,id:65,x:31347,y:32436,varname:node_65,prsc:2|IN-3290-OUT;n:type:ShaderForge.SFN_RemapRange,id:861,x:31564,y:32436,varname:node_861,prsc:2,frmn:0,frmx:1,tomn:-0.65,tomx:0.65|IN-65-OUT;n:type:ShaderForge.SFN_Add,id:3912,x:31789,y:32436,varname:node_3912,prsc:2|A-861-OUT,B-8063-R;n:type:ShaderForge.SFN_Add,id:949,x:31789,y:32598,varname:node_949,prsc:2|A-861-OUT,B-5901-R;n:type:ShaderForge.SFN_Multiply,id:7995,x:31993,y:32436,varname:node_7995,prsc:2|A-3912-OUT,B-949-OUT;n:type:ShaderForge.SFN_RemapRange,id:3267,x:32184,y:32436,varname:node_3267,prsc:2,frmn:0,frmx:1,tomn:-4,tomx:10|IN-7995-OUT;n:type:ShaderForge.SFN_Clamp01,id:8934,x:32370,y:32428,varname:node_8934,prsc:2|IN-3267-OUT;n:type:ShaderForge.SFN_OneMinus,id:4732,x:32543,y:32428,varname:node_4732,prsc:2|IN-8934-OUT;n:type:ShaderForge.SFN_Append,id:1023,x:32719,y:32428,varname:node_1023,prsc:2|A-4732-OUT,B-2964-OUT;n:type:ShaderForge.SFN_Vector1,id:2964,x:32543,y:32582,varname:node_2964,prsc:2,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:396,x:32939,y:32944,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:_Opacity,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:5288,x:33180,y:32285,varname:node_5288,prsc:2|A-8947-OUT,B-6074-R;n:type:ShaderForge.SFN_ValueProperty,id:8947,x:32950,y:32285,ptovrint:False,ptlb:Strench,ptin:_Strench,varname:_Strench,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;proporder:6074-797-2712-8599-1380-3290-4537-1859-396-8947;pass:END;sub:END;*/

Shader "Shader Forge/electricity" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0,0.1165825,0.8897059,1)
        _U_Speed ("U_Speed", Float ) = 0.2
        _V_Spees ("V_Spees", Float ) = 0.2
        _node_1380 ("node_1380", 2D) = "white" {}
        _Dissolve ("Dissolve", Range(0, 1)) = 0.474359
        _2u_Speed ("2u_Speed", Float ) = -0.2
        _2V_Spped ("2V_Spped", Float ) = 0.05
        _Opacity ("Opacity", Float ) = 2
        _Strench ("Strench", Float ) = 0.8
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _U_Speed;
            uniform float _V_Spees;
            uniform sampler2D _node_1380; uniform float4 _node_1380_ST;
            uniform float _2u_Speed;
            uniform float _2V_Spped;
            uniform float _Dissolve;
            uniform float _Opacity;
            uniform float _Strench;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float node_861 = ((1.0 - _Dissolve)*1.3+-0.65);
                float4 node_6530 = _Time + _TimeEditor;
                float2 node_115 = ((float2(_U_Speed,_V_Spees)*node_6530.g)+i.uv0);
                float4 node_8063 = tex2D(_node_1380,TRANSFORM_TEX(node_115, _node_1380));
                float4 node_6779 = _Time + _TimeEditor;
                float2 node_427 = ((float2(_2u_Speed,_2V_Spped)*node_6779.g)+i.uv0);
                float4 node_5901 = tex2D(_node_1380,TRANSFORM_TEX(node_427, _node_1380));
                float2 node_1023 = float2((1.0 - saturate((((node_861+node_8063.r)*(node_861+node_5901.r))*14.0+-4.0))),0.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_1023, _MainTex));
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_Opacity);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,(_Strench*_MainTex_var.r));
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
