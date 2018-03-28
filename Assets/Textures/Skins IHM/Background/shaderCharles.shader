// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33354,y:32447,varname:node_4013,prsc:2|emission-5488-OUT;n:type:ShaderForge.SFN_TexCoord,id:729,x:31143,y:32565,varname:node_729,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:5488,x:32829,y:32231,varname:node_5488,prsc:2|A-5419-RGB,B-7594-RGB,T-2851-OUT;n:type:ShaderForge.SFN_Color,id:5419,x:32005,y:32101,ptovrint:False,ptlb:node_5419,ptin:_node_5419,varname:node_5419,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.05640436,c2:0.01265138,c3:0.1323529,c4:1;n:type:ShaderForge.SFN_Color,id:7594,x:32005,y:32264,ptovrint:False,ptlb:node_5419_copy,ptin:_node_5419_copy,varname:_node_5419_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.03389923,c2:0.138993,c3:0.2426471,c4:1;n:type:ShaderForge.SFN_ComponentMask,id:7590,x:31827,y:32528,varname:node_7590,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3432-UVOUT;n:type:ShaderForge.SFN_Clamp01,id:2851,x:32641,y:32442,varname:node_2851,prsc:2|IN-4151-OUT;n:type:ShaderForge.SFN_Time,id:5932,x:31582,y:32692,varname:node_5932,prsc:2;n:type:ShaderForge.SFN_Sin,id:4151,x:32465,y:32503,varname:node_4151,prsc:2|IN-6492-OUT;n:type:ShaderForge.SFN_Vector1,id:4180,x:31582,y:32847,varname:node_4180,prsc:2,v1:0.05;n:type:ShaderForge.SFN_Multiply,id:7182,x:31788,y:32704,varname:node_7182,prsc:2|A-5932-T,B-4180-OUT;n:type:ShaderForge.SFN_Multiply,id:6492,x:32291,y:32524,varname:node_6492,prsc:2|A-2156-OUT,B-6745-OUT;n:type:ShaderForge.SFN_Add,id:2156,x:32063,y:32590,varname:node_2156,prsc:2|A-7590-OUT,B-7182-OUT;n:type:ShaderForge.SFN_Vector1,id:6745,x:32075,y:32791,varname:node_6745,prsc:2,v1:2;n:type:ShaderForge.SFN_Rotator,id:3432,x:31421,y:32547,varname:node_3432,prsc:2|UVIN-729-UVOUT,ANG-332-TSL;n:type:ShaderForge.SFN_Vector1,id:7469,x:31165,y:32745,varname:node_7469,prsc:2,v1:45;n:type:ShaderForge.SFN_Time,id:332,x:31165,y:32830,varname:node_332,prsc:2;proporder:5419-7594;pass:END;sub:END;*/

Shader "Shader Forge/shaderCharles" {
    Properties {
        _node_5419 ("node_5419", Color) = (0.05640436,0.01265138,0.1323529,1)
        _node_5419_copy ("node_5419_copy", Color) = (0.03389923,0.138993,0.2426471,1)
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
            uniform float4 _node_5419;
            uniform float4 _node_5419_copy;
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
                float4 node_332 = _Time + _TimeEditor;
                float node_3432_ang = node_332.r;
                float node_3432_spd = 1.0;
                float node_3432_cos = cos(node_3432_spd*node_3432_ang);
                float node_3432_sin = sin(node_3432_spd*node_3432_ang);
                float2 node_3432_piv = float2(0.5,0.5);
                float2 node_3432 = (mul(i.uv0-node_3432_piv,float2x2( node_3432_cos, -node_3432_sin, node_3432_sin, node_3432_cos))+node_3432_piv);
                float4 node_5932 = _Time + _TimeEditor;
                float3 emissive = lerp(_node_5419.rgb,_node_5419_copy.rgb,saturate(sin(((node_3432.r+(node_5932.g*0.05))*2.0))));
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
