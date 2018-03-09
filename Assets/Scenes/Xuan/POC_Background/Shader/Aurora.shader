// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:7814,x:35354,y:31975,varname:node_7814,prsc:2|emission-8097-OUT,voffset-9001-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:117,x:33039,y:32163,ptovrint:False,ptlb:Mix,ptin:_Mix,varname:node_117,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3e2dc0bb3bee18a48a0d667eb62e4584,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:2916,x:33227,y:32835,varname:node_2916,prsc:2,tex:3e2dc0bb3bee18a48a0d667eb62e4584,ntxv:0,isnm:False|UVIN-4485-OUT,TEX-117-TEX;n:type:ShaderForge.SFN_Multiply,id:8367,x:33516,y:32857,varname:node_8367,prsc:2|A-2916-R,B-9268-OUT;n:type:ShaderForge.SFN_Append,id:9001,x:33750,y:32838,varname:node_9001,prsc:2|A-9180-OUT,B-8367-OUT;n:type:ShaderForge.SFN_Vector1,id:9180,x:33580,y:33014,varname:node_9180,prsc:2,v1:0;n:type:ShaderForge.SFN_TexCoord,id:9790,x:32160,y:32066,varname:node_9790,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:9997,x:32313,y:32820,ptovrint:False,ptlb:VO_SPD_U,ptin:_VO_SPD_U,varname:node_9997,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.3;n:type:ShaderForge.SFN_ValueProperty,id:8015,x:32313,y:32922,ptovrint:False,ptlb:VO_SPD_V,ptin:_VO_SPD_V,varname:node_8015,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:1192,x:32498,y:32820,varname:node_1192,prsc:2|A-9997-OUT,B-8015-OUT;n:type:ShaderForge.SFN_Time,id:8467,x:32556,y:32186,varname:node_8467,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7045,x:32708,y:32820,varname:node_7045,prsc:2|A-1192-OUT,B-8467-T;n:type:ShaderForge.SFN_Add,id:7810,x:32941,y:32820,varname:node_7810,prsc:2|A-9790-UVOUT,B-7045-OUT;n:type:ShaderForge.SFN_ValueProperty,id:9268,x:33363,y:33014,ptovrint:False,ptlb:VO_STRENGTH,ptin:_VO_STRENGTH,varname:node_9268,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.8;n:type:ShaderForge.SFN_Tex2d,id:8410,x:33247,y:32163,varname:node_8410,prsc:2,tex:3e2dc0bb3bee18a48a0d667eb62e4584,ntxv:0,isnm:False|TEX-117-TEX;n:type:ShaderForge.SFN_ValueProperty,id:9974,x:32327,y:31818,ptovrint:False,ptlb:Indentation_SPD_V,ptin:_Indentation_SPD_V,varname:_VO_SPD_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:9697,x:32327,y:31716,ptovrint:False,ptlb:Indentation_SPD_U,ptin:_Indentation_SPD_U,varname:_VO_SPD_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.04;n:type:ShaderForge.SFN_Append,id:5394,x:32512,y:31716,varname:node_5394,prsc:2|A-9697-OUT,B-9974-OUT;n:type:ShaderForge.SFN_Multiply,id:7657,x:32714,y:31716,varname:node_7657,prsc:2|A-5394-OUT,B-8467-T;n:type:ShaderForge.SFN_Add,id:9844,x:33003,y:31742,varname:node_9844,prsc:2|A-9790-UVOUT,B-7657-OUT;n:type:ShaderForge.SFN_Tex2d,id:759,x:33272,y:31760,varname:node_759,prsc:2,tex:3e2dc0bb3bee18a48a0d667eb62e4584,ntxv:0,isnm:False|UVIN-7138-OUT,TEX-117-TEX;n:type:ShaderForge.SFN_Multiply,id:2244,x:34364,y:32152,varname:node_2244,prsc:2|A-7509-OUT,B-8410-G;n:type:ShaderForge.SFN_Add,id:5225,x:33518,y:31822,varname:node_5225,prsc:2|A-1728-OUT,B-759-B;n:type:ShaderForge.SFN_ValueProperty,id:1728,x:33518,y:31729,ptovrint:False,ptlb:Indentation_add,ptin:_Indentation_add,varname:node_1728,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:515,x:33001,y:31123,varname:node_515,prsc:2|A-9790-UVOUT,B-1155-OUT;n:type:ShaderForge.SFN_Multiply,id:1155,x:32708,y:31131,varname:node_1155,prsc:2|A-9182-OUT,B-8467-T;n:type:ShaderForge.SFN_Append,id:9182,x:32506,y:31131,varname:node_9182,prsc:2|A-5899-OUT,B-378-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5899,x:32321,y:31131,ptovrint:False,ptlb:Lines_SPD_U,ptin:_Lines_SPD_U,varname:_Indentation_SPD_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:-0.02;n:type:ShaderForge.SFN_ValueProperty,id:378,x:32321,y:31227,ptovrint:False,ptlb:Lines_SPD_V,ptin:_Lines_SPD_V,varname:_Indentation_SPD_V_copy_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Add,id:7509,x:34105,y:31783,varname:node_7509,prsc:2|A-1344-OUT,B-5225-OUT;n:type:ShaderForge.SFN_Tex2d,id:6474,x:33277,y:31123,varname:node_6474,prsc:2,tex:3e2dc0bb3bee18a48a0d667eb62e4584,ntxv:0,isnm:False|UVIN-7785-OUT,TEX-117-TEX;n:type:ShaderForge.SFN_ValueProperty,id:4555,x:33755,y:31473,ptovrint:False,ptlb:Lines_Strength,ptin:_Lines_Strength,varname:node_4555,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2.5;n:type:ShaderForge.SFN_Multiply,id:1344,x:33755,y:31537,varname:node_1344,prsc:2|A-4555-OUT,B-6474-A;n:type:ShaderForge.SFN_Multiply,id:7785,x:33263,y:30885,varname:node_7785,prsc:2|A-4662-OUT,B-515-OUT;n:type:ShaderForge.SFN_ValueProperty,id:3165,x:32774,y:30834,ptovrint:False,ptlb:Lines_Tile_U,ptin:_Lines_Tile_U,varname:node_3165,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:4;n:type:ShaderForge.SFN_ValueProperty,id:6892,x:32774,y:30931,ptovrint:False,ptlb:Lines_Tile_V,ptin:_Lines_Tile_V,varname:_node_3165_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Append,id:4662,x:33017,y:30885,varname:node_4662,prsc:2|A-3165-OUT,B-6892-OUT;n:type:ShaderForge.SFN_ValueProperty,id:8179,x:32784,y:31532,ptovrint:False,ptlb:Indentation_Tile_V,ptin:_Indentation_Tile_V,varname:_Lines_Tile_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_ValueProperty,id:9048,x:32784,y:31435,ptovrint:False,ptlb:Indentation_Tile_U,ptin:_Indentation_Tile_U,varname:_Lines_Tile_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Append,id:4719,x:33027,y:31486,varname:node_4719,prsc:2|A-9048-OUT,B-8179-OUT;n:type:ShaderForge.SFN_Multiply,id:7138,x:33273,y:31486,varname:node_7138,prsc:2|A-4719-OUT,B-9844-OUT;n:type:ShaderForge.SFN_Append,id:5897,x:32977,y:32466,varname:node_5897,prsc:2|A-6665-OUT,B-6193-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6665,x:32734,y:32415,ptovrint:False,ptlb:VO_Tile_U,ptin:_VO_Tile_U,varname:_Indentation_Tile_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;n:type:ShaderForge.SFN_ValueProperty,id:6193,x:32734,y:32512,ptovrint:False,ptlb:VO_Tile_V,ptin:_VO_Tile_V,varname:_Indentation_Tile_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;n:type:ShaderForge.SFN_Multiply,id:4485,x:33221,y:32466,varname:node_4485,prsc:2|A-5897-OUT,B-7810-OUT;n:type:ShaderForge.SFN_Lerp,id:6188,x:34686,y:31684,varname:node_6188,prsc:2|A-1652-RGB,B-4513-RGB,T-9319-OUT;n:type:ShaderForge.SFN_Slider,id:790,x:34065,y:31562,ptovrint:False,ptlb:Color_Slider,ptin:_Color_Slider,varname:node_790,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.25,max:2;n:type:ShaderForge.SFN_Color,id:4513,x:34144,y:31316,ptovrint:False,ptlb:Color_2,ptin:_Color_2,varname:node_4513,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.8882353,c2:0.8602941,c3:1,c4:1;n:type:ShaderForge.SFN_Color,id:1652,x:34140,y:30988,ptovrint:False,ptlb:Color_1,ptin:_Color_1,varname:node_1652,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.9172413,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:9319,x:34439,y:31778,varname:node_9319,prsc:2|A-790-OUT,B-2244-OUT;n:type:ShaderForge.SFN_Multiply,id:8745,x:34851,y:31985,varname:node_8745,prsc:2|A-6188-OUT,B-2244-OUT;n:type:ShaderForge.SFN_ValueProperty,id:546,x:35046,y:31830,ptovrint:False,ptlb:Emissive_Strength,ptin:_Emissive_Strength,varname:node_546,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1.5;n:type:ShaderForge.SFN_Multiply,id:8097,x:35066,y:31985,varname:node_8097,prsc:2|A-546-OUT,B-8745-OUT;proporder:117-9268-9997-8015-9974-9697-1728-5899-378-4555-3165-6892-8179-9048-790-4513-1652-546-6665-6193;pass:END;sub:END;*/

Shader "Custom/Aurora" {
    Properties {
        _Mix ("Mix", 2D) = "white" {}
        _VO_STRENGTH ("VO_STRENGTH", Float ) = 0.8
        _VO_SPD_U ("VO_SPD_U", Float ) = 0.3
        _VO_SPD_V ("VO_SPD_V", Float ) = 0
        _Indentation_SPD_V ("Indentation_SPD_V", Float ) = 0
        _Indentation_SPD_U ("Indentation_SPD_U", Float ) = 0.04
        _Indentation_add ("Indentation_add", Float ) = 0.5
        _Lines_SPD_U ("Lines_SPD_U", Float ) = -0.02
        _Lines_SPD_V ("Lines_SPD_V", Float ) = 0
        _Lines_Strength ("Lines_Strength", Float ) = 2.5
        _Lines_Tile_U ("Lines_Tile_U", Float ) = 4
        _Lines_Tile_V ("Lines_Tile_V", Float ) = 1
        _Indentation_Tile_V ("Indentation_Tile_V", Float ) = 1
        _Indentation_Tile_U ("Indentation_Tile_U", Float ) = 3
        _Color_Slider ("Color_Slider", Range(0, 2)) = 0.25
        _Color_2 ("Color_2", Color) = (0.8882353,0.8602941,1,1)
        _Color_1 ("Color_1", Color) = (0,0.9172413,1,1)
        _Emissive_Strength ("Emissive_Strength", Float ) = 1.5
        _VO_Tile_U ("VO_Tile_U", Float ) = 1.5
        _VO_Tile_V ("VO_Tile_V", Float ) = 1
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        LOD 200
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend One One
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Mix; uniform float4 _Mix_ST;
            uniform float _VO_SPD_U;
            uniform float _VO_SPD_V;
            uniform float _VO_STRENGTH;
            uniform float _Indentation_SPD_V;
            uniform float _Indentation_SPD_U;
            uniform float _Indentation_add;
            uniform float _Lines_SPD_U;
            uniform float _Lines_SPD_V;
            uniform float _Lines_Strength;
            uniform float _Lines_Tile_U;
            uniform float _Lines_Tile_V;
            uniform float _Indentation_Tile_V;
            uniform float _Indentation_Tile_U;
            uniform float _VO_Tile_U;
            uniform float _VO_Tile_V;
            uniform float _Color_Slider;
            uniform float4 _Color_2;
            uniform float4 _Color_1;
            uniform float _Emissive_Strength;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                UNITY_FOG_COORDS(1)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_8467 = _Time + _TimeEditor;
                float2 node_4485 = (float2(_VO_Tile_U,_VO_Tile_V)*(o.uv0+(float2(_VO_SPD_U,_VO_SPD_V)*node_8467.g)));
                float4 node_2916 = tex2Dlod(_Mix,float4(TRANSFORM_TEX(node_4485, _Mix),0.0,0));
                v.vertex.xyz += float3(float2(0.0,(node_2916.r*_VO_STRENGTH)),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_8467 = _Time + _TimeEditor;
                float2 node_7785 = (float2(_Lines_Tile_U,_Lines_Tile_V)*(i.uv0+(float2(_Lines_SPD_U,_Lines_SPD_V)*node_8467.g)));
                float4 node_6474 = tex2D(_Mix,TRANSFORM_TEX(node_7785, _Mix));
                float2 node_7138 = (float2(_Indentation_Tile_U,_Indentation_Tile_V)*(i.uv0+(float2(_Indentation_SPD_U,_Indentation_SPD_V)*node_8467.g)));
                float4 node_759 = tex2D(_Mix,TRANSFORM_TEX(node_7138, _Mix));
                float4 node_8410 = tex2D(_Mix,TRANSFORM_TEX(i.uv0, _Mix));
                float node_2244 = (((_Lines_Strength*node_6474.a)+(_Indentation_add+node_759.b))*node_8410.g);
                float3 emissive = (_Emissive_Strength*(lerp(_Color_1.rgb,_Color_2.rgb,(_Color_Slider*node_2244))*node_2244));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "ShadowCaster"
            Tags {
                "LightMode"="ShadowCaster"
            }
            Offset 1, 1
            Cull Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_SHADOWCASTER
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _Mix; uniform float4 _Mix_ST;
            uniform float _VO_SPD_U;
            uniform float _VO_SPD_V;
            uniform float _VO_STRENGTH;
            uniform float _VO_Tile_U;
            uniform float _VO_Tile_V;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
                float2 uv0 : TEXCOORD1;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                float4 node_8467 = _Time + _TimeEditor;
                float2 node_4485 = (float2(_VO_Tile_U,_VO_Tile_V)*(o.uv0+(float2(_VO_SPD_U,_VO_SPD_V)*node_8467.g)));
                float4 node_2916 = tex2Dlod(_Mix,float4(TRANSFORM_TEX(node_4485, _Mix),0.0,0));
                v.vertex.xyz += float3(float2(0.0,(node_2916.r*_VO_STRENGTH)),0.0);
                o.pos = UnityObjectToClipPos( v.vertex );
                TRANSFER_SHADOW_CASTER(o)
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                SHADOW_CASTER_FRAGMENT(i)
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
