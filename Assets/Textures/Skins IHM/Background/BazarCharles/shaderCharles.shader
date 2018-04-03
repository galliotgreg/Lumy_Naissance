// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33034,y:31962,varname:node_4013,prsc:2|emission-7725-OUT;n:type:ShaderForge.SFN_TexCoord,id:729,x:30864,y:31849,varname:node_729,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Lerp,id:5488,x:32675,y:31923,varname:node_5488,prsc:2|A-5419-RGB,B-7594-RGB,T-2851-OUT;n:type:ShaderForge.SFN_Color,id:5419,x:32242,y:31551,ptovrint:False,ptlb:A,ptin:_A,varname:_A,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.097,c3:0.153,c4:1;n:type:ShaderForge.SFN_Color,id:7594,x:32266,y:31788,ptovrint:False,ptlb:B,ptin:_B,varname:_B,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.01740916,c2:0.0435658,c3:0.1691176,c4:1;n:type:ShaderForge.SFN_ComponentMask,id:7590,x:31710,y:32002,varname:node_7590,prsc:2,cc1:0,cc2:-1,cc3:-1,cc4:-1|IN-3432-UVOUT;n:type:ShaderForge.SFN_Clamp01,id:2851,x:32473,y:32029,varname:node_2851,prsc:2|IN-4151-OUT;n:type:ShaderForge.SFN_Time,id:5932,x:31445,y:32103,varname:node_5932,prsc:2;n:type:ShaderForge.SFN_Sin,id:4151,x:32269,y:32073,varname:node_4151,prsc:2|IN-6492-OUT;n:type:ShaderForge.SFN_Vector1,id:4180,x:31445,y:32299,cmnt:Translation Speed,varname:node_4180,prsc:2,v1:0.4;n:type:ShaderForge.SFN_Multiply,id:7182,x:31681,y:32220,varname:node_7182,prsc:2|A-5932-T,B-4180-OUT;n:type:ShaderForge.SFN_Multiply,id:6492,x:32094,y:32087,varname:node_6492,prsc:2|A-2156-OUT,B-6745-OUT;n:type:ShaderForge.SFN_Add,id:2156,x:31930,y:32054,varname:node_2156,prsc:2|A-7590-OUT,B-7182-OUT;n:type:ShaderForge.SFN_Vector1,id:6745,x:31895,y:32270,cmnt:Degrade softness,varname:node_6745,prsc:2,v1:0.71;n:type:ShaderForge.SFN_Rotator,id:3432,x:31304,y:32021,varname:node_3432,prsc:2|UVIN-7842-OUT,ANG-852-OUT;n:type:ShaderForge.SFN_Time,id:332,x:30879,y:32060,varname:node_332,prsc:2;n:type:ShaderForge.SFN_Vector1,id:1480,x:30879,y:32269,cmnt:Rotation Speed,varname:node_1480,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:852,x:31130,y:32176,varname:node_852,prsc:2|A-332-TSL,B-1480-OUT;n:type:ShaderForge.SFN_RemapRange,id:7842,x:31078,y:31877,varname:node_7842,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-729-UVOUT;n:type:ShaderForge.SFN_Multiply,id:7725,x:32857,y:32111,varname:node_7725,prsc:2|A-5488-OUT,B-5432-OUT;n:type:ShaderForge.SFN_Length,id:8815,x:32268,y:32418,varname:node_8815,prsc:2|IN-8403-OUT;n:type:ShaderForge.SFN_TexCoord,id:8060,x:31632,y:32496,varname:node_8060,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:8403,x:31863,y:32470,varname:node_8403,prsc:2,frmn:-1,frmx:1,tomn:-1,tomx:1|IN-8060-UVOUT;n:type:ShaderForge.SFN_Time,id:3227,x:31705,y:32726,varname:node_3227,prsc:2;n:type:ShaderForge.SFN_Abs,id:6065,x:32439,y:32674,varname:node_6065,prsc:2|IN-8959-OUT;n:type:ShaderForge.SFN_Sin,id:8959,x:32272,y:32674,varname:node_8959,prsc:2|IN-4724-OUT;n:type:ShaderForge.SFN_Multiply,id:4724,x:31910,y:32867,varname:node_4724,prsc:2|A-3227-T,B-3797-OUT;n:type:ShaderForge.SFN_Vector1,id:3797,x:31626,y:32927,cmnt:pulsation,varname:node_3797,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Clamp,id:5432,x:32688,y:32273,varname:node_5432,prsc:2|IN-8815-OUT,MIN-6065-OUT,MAX-9430-OUT;n:type:ShaderForge.SFN_Vector1,id:9430,x:32515,y:32521,varname:node_9430,prsc:2,v1:1;n:type:ShaderForge.SFN_Color,id:8756,x:31887,y:31559,ptovrint:False,ptlb:A_copy,ptin:_A_copy,varname:_A_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.1529412,c3:0.1921569,c4:1;n:type:ShaderForge.SFN_Color,id:8574,x:31887,y:31766,ptovrint:False,ptlb:B_copy,ptin:_B_copy,varname:_B_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.091,c3:0.1921569,c4:1;proporder:5419-7594;pass:END;sub:END;*/

Shader "Shader Forge/shaderCharles" {
    Properties {
        _A ("A", Color) = (0,0.097,0.153,1)
        _B ("B", Color) = (0.01740916,0.0435658,0.1691176,1)
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
            uniform float4 _A;
            uniform float4 _B;
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
                float node_3432_ang = (node_332.r*0.5);
                float node_3432_spd = 1.0;
                float node_3432_cos = cos(node_3432_spd*node_3432_ang);
                float node_3432_sin = sin(node_3432_spd*node_3432_ang);
                float2 node_3432_piv = float2(0.5,0.5);
                float2 node_3432 = (mul((i.uv0*2.0+-1.0)-node_3432_piv,float2x2( node_3432_cos, -node_3432_sin, node_3432_sin, node_3432_cos))+node_3432_piv);
                float4 node_5932 = _Time + _TimeEditor;
                float4 node_3227 = _Time + _TimeEditor;
                float3 emissive = (lerp(_A.rgb,_B.rgb,saturate(sin(((node_3432.r+(node_5932.g*0.4))*0.71))))*clamp(length((i.uv0*1.0+0.0)),abs(sin((node_3227.g*0.1))),1.0));
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
