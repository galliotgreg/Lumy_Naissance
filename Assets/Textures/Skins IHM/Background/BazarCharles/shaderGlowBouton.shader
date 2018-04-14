// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33034,y:31962,varname:node_4013,prsc:2|emission-5488-OUT;n:type:ShaderForge.SFN_TexCoord,id:729,x:31421,y:31830,varname:node_729,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:5488,x:32675,y:31923,varname:node_5488,prsc:2|A-8756-RGB,B-8574-RGB,T-2851-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7590,x:31834,y:32000,varname:node_7590,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-7842-OUT;n:type:ShaderForge.SFN_Clamp01,id:2851,x:32465,y:32043,varname:node_2851,prsc:2|IN-4151-OUT;n:type:ShaderForge.SFN_Time,id:5932,x:31569,y:32101,varname:node_5932,prsc:2;n:type:ShaderForge.SFN_Sin,id:4151,x:32269,y:32073,varname:node_4151,prsc:2|IN-2156-OUT;n:type:ShaderForge.SFN_Vector1,id:4180,x:31599,y:32304,cmnt:Translation Speed,varname:node_4180,prsc:2,v1:0.4;n:type:ShaderForge.SFN_Multiply,id:7182,x:31805,y:32218,varname:node_7182,prsc:2|A-5932-T,B-4180-OUT;n:type:ShaderForge.SFN_Add,id:2156,x:32054,y:32052,varname:node_2156,prsc:2|A-7590-OUT,B-7182-OUT;n:type:ShaderForge.SFN_RemapRange,id:7842,x:31635,y:31858,varname:node_7842,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-729-UVOUT;n:type:ShaderForge.SFN_Color,id:8756,x:32432,y:31607,ptovrint:False,ptlb:A_copy,ptin:_A_copy,varname:_A_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.2906574,c2:0.8084174,c3:0.9411765,c4:1;n:type:ShaderForge.SFN_Color,id:8574,x:32304,y:31835,ptovrint:False,ptlb:B_copy,ptin:_B_copy,varname:_B_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0,c3:0,c4:1;proporder:8756-8574;pass:END;sub:END;*/

Shader "Shader Forge/shaderCharles" {
    Properties {
        _A_copy ("A_copy", Color) = (0.2906574,0.8084174,0.9411765,1)
        _B_copy ("B_copy", Color) = (0,0,0,1)
    }
    SubShader {
        Tags {
            "RenderType"="Opaque"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase_fullshadows
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _A_copy;
            uniform float4 _B_copy;
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
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
////// Lighting:
////// Emissive:
                float2 node_7842 = (i.uv0*2.0+-1.0);
                float4 node_5932 = _Time + _TimeEditor;
                float node_2156 = (node_7842.r+(node_5932.g*0.4));
                float3 node_5488 = lerp(_A_copy.rgb,_B_copy.rgb,saturate(sin(node_2156)));
                float3 emissive = node_5488;
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
