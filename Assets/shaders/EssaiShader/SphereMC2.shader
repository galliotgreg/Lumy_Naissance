// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32993,y:32663,varname:node_2865,prsc:2|emission-5652-OUT;n:type:ShaderForge.SFN_Clamp01,id:7494,x:32470,y:32870,varname:node_7494,prsc:2|IN-4406-OUT;n:type:ShaderForge.SFN_Min,id:7887,x:32071,y:32748,varname:node_7887,prsc:2|A-7064-OUT,B-1207-OUT;n:type:ShaderForge.SFN_Multiply,id:4406,x:32308,y:32870,varname:node_4406,prsc:2|A-7887-OUT,B-5891-OUT;n:type:ShaderForge.SFN_Power,id:7064,x:32003,y:32499,varname:node_7064,prsc:2|VAL-3731-OUT,EXP-4438-OUT;n:type:ShaderForge.SFN_OneMinus,id:3731,x:31714,y:32593,varname:node_3731,prsc:2|IN-2330-OUT;n:type:ShaderForge.SFN_Fresnel,id:2330,x:31505,y:32593,varname:node_2330,prsc:2|EXP-8632-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8632,x:31262,y:32577,ptovrint:False,ptlb:node_8632,ptin:_node_8632,varname:node_8632,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Fresnel,id:6855,x:31568,y:32863,varname:node_6855,prsc:2|EXP-7432-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7432,x:31306,y:32956,ptovrint:False,ptlb:node_8632_copy,ptin:_node_8632_copy,varname:_node_8632_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:8;n:type:ShaderForge.SFN_Vector1,id:4438,x:31656,y:32772,varname:node_4438,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:5891,x:32110,y:32955,varname:node_5891,prsc:2,v1:1.4;n:type:ShaderForge.SFN_VertexColor,id:7578,x:32243,y:33412,varname:node_7578,prsc:2;n:type:ShaderForge.SFN_Color,id:9102,x:32021,y:33634,ptovrint:False,ptlb:node_9102,ptin:_node_9102,varname:node_9102,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7573529,c2:0.7573529,c3:0.7573529,c4:1;n:type:ShaderForge.SFN_Multiply,id:5652,x:32732,y:33134,varname:node_5652,prsc:2|A-3679-A,B-3704-OUT;n:type:ShaderForge.SFN_Vector1,id:8257,x:32213,y:33770,varname:node_8257,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:3704,x:32544,y:33376,varname:node_3704,prsc:2|A-7494-OUT,B-7578-RGB,C-9279-RGB,D-8257-OUT,E-7578-A;n:type:ShaderForge.SFN_Tex2dAsset,id:5782,x:31321,y:33703,ptovrint:False,ptlb:node_5782,ptin:_node_5782,varname:node_5782,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Time,id:5533,x:30262,y:33391,varname:node_5533,prsc:2;n:type:ShaderForge.SFN_ValueProperty,id:2598,x:30156,y:33182,ptovrint:False,ptlb:x,ptin:_x,varname:_node_8632_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_ValueProperty,id:3630,x:30141,y:33300,ptovrint:False,ptlb:y,ptin:_y,varname:_node_8632_copy_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:9358,x:30396,y:33171,varname:node_9358,prsc:2|A-2598-OUT,B-3630-OUT;n:type:ShaderForge.SFN_Tex2d,id:3679,x:31539,y:33293,varname:node_3679,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-165-OUT,TEX-5782-TEX;n:type:ShaderForge.SFN_Multiply,id:1207,x:31901,y:32970,varname:node_1207,prsc:2|A-6855-OUT,B-3679-RGB,C-9157-OUT;n:type:ShaderForge.SFN_Multiply,id:9157,x:32022,y:33373,varname:node_9157,prsc:2|A-3679-A,B-2697-OUT;n:type:ShaderForge.SFN_Slider,id:2697,x:31641,y:33704,ptovrint:False,ptlb:node_2697,ptin:_node_2697,varname:node_2697,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Multiply,id:2373,x:31108,y:33167,varname:node_2373,prsc:2|A-9358-OUT,B-3898-OUT;n:type:ShaderForge.SFN_Add,id:165,x:31306,y:33062,varname:node_165,prsc:2|A-3021-UVOUT,B-2373-OUT;n:type:ShaderForge.SFN_TexCoord,id:3021,x:30854,y:32847,varname:node_3021,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Color,id:9279,x:32458,y:33664,ptovrint:False,ptlb:node_9102_copy,ptin:_node_9102_copy,varname:_node_9102_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1838235,c2:0.7124745,c3:0.7352941,c4:1;n:type:ShaderForge.SFN_Vector1,id:1346,x:30585,y:33614,varname:node_1346,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Multiply,id:3898,x:30954,y:33348,varname:node_3898,prsc:2|A-5533-T,B-1346-OUT;n:type:ShaderForge.SFN_Panner,id:7078,x:29570,y:34254,varname:node_7078,prsc:2,spu:1,spv:1;n:type:ShaderForge.SFN_Lerp,id:8766,x:31741,y:33498,varname:node_8766,prsc:2;n:type:ShaderForge.SFN_Multiply,id:799,x:31536,y:33826,varname:node_799,prsc:2;n:type:ShaderForge.SFN_Lerp,id:7116,x:29858,y:32020,varname:node_7116,prsc:2|A-672-UVOUT,B-8983-RGB,T-6187-OUT;n:type:ShaderForge.SFN_TexCoord,id:672,x:29388,y:31826,varname:node_672,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:6187,x:29207,y:32183,ptovrint:False,ptlb:node_2697_copy,ptin:_node_2697_copy,varname:_node_2697_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.009365,max:2;n:type:ShaderForge.SFN_Panner,id:5464,x:29285,y:31993,varname:node_5464,prsc:2,spu:0.1,spv:0.8|UVIN-7482-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:7482,x:29089,y:31975,varname:node_7482,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Tex2d,id:8983,x:29499,y:31986,ptovrint:False,ptlb:node_133,ptin:_node_133,varname:node_3679,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-5464-UVOUT;proporder:8632-7432-9102-5782-2598-3630-2697-9279-6187;pass:END;sub:END;*/

Shader "Shader Forge/SphereMC2" {
    Properties {
        _node_8632 ("node_8632", Float ) = 1
        _node_8632_copy ("node_8632_copy", Float ) = 8
        _node_9102 ("node_9102", Color) = (0.7573529,0.7573529,0.7573529,1)
        _node_5782 ("node_5782", 2D) = "white" {}
        _x ("x", Float ) = 2
        _y ("y", Float ) = 1
        _node_2697 ("node_2697", Range(0, 1)) = 1
        _node_9102_copy ("node_9102_copy", Color) = (0.1838235,0.7124745,0.7352941,1)
        _node_2697_copy ("node_2697_copy", Range(0, 2)) = 1.009365
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
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _node_8632;
            uniform float _node_8632_copy;
            uniform sampler2D _node_5782; uniform float4 _node_5782_ST;
            uniform float _x;
            uniform float _y;
            uniform float _node_2697;
            uniform float4 _node_9102_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
                UNITY_FOG_COORDS(3)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
////// Lighting:
////// Emissive:
                float4 node_5533 = _Time + _TimeEditor;
                float2 node_165 = (i.uv0+(float2(_x,_y)*(node_5533.g*0.1)));
                float4 node_3679 = tex2D(_node_5782,TRANSFORM_TEX(node_165, _node_5782));
                float3 emissive = (node_3679.a*(saturate((min(pow((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_8632)),1.0),(pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_8632_copy)*node_3679.rgb*(node_3679.a*_node_2697)))*1.4))*i.vertexColor.rgb*_node_9102_copy.rgb*1.5*i.vertexColor.a));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "Meta"
            Tags {
                "LightMode"="Meta"
            }
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_META 1
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float _node_8632;
            uniform float _node_8632_copy;
            uniform sampler2D _node_5782; uniform float4 _node_5782_ST;
            uniform float _x;
            uniform float _y;
            uniform float _node_2697;
            uniform float4 _node_9102_copy;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_5533 = _Time + _TimeEditor;
                float2 node_165 = (i.uv0+(float2(_x,_y)*(node_5533.g*0.1)));
                float4 node_3679 = tex2D(_node_5782,TRANSFORM_TEX(node_165, _node_5782));
                o.Emission = (node_3679.a*(saturate((min(pow((1.0 - pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_8632)),1.0),(pow(1.0-max(0,dot(normalDirection, viewDirection)),_node_8632_copy)*node_3679.rgb*(node_3679.a*_node_2697)))*1.4))*i.vertexColor.rgb*_node_9102_copy.rgb*1.5*i.vertexColor.a));
                
                float3 diffColor = float3(0,0,0);
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
