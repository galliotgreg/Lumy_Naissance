// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33261,y:32566,varname:node_4013,prsc:2|emission-2789-RGB;n:type:ShaderForge.SFN_Tex2d,id:2789,x:33079,y:32660,varname:node_2789,prsc:2,tex:3e54f5471adcf2348a7637785b97461f,ntxv:0,isnm:False|UVIN-4163-OUT,TEX-9524-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9524,x:32822,y:32848,ptovrint:False,ptlb:node_9524,ptin:_node_9524,varname:node_9524,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3e54f5471adcf2348a7637785b97461f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:9811,x:31573,y:32453,varname:node_9811,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:8654,x:31763,y:32265,varname:node_8654,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1;n:type:ShaderForge.SFN_ComponentMask,id:2920,x:31970,y:32265,varname:node_2920,prsc:2,cc1:0,cc2:0,cc3:-1,cc4:-1|IN-8654-OUT;n:type:ShaderForge.SFN_Vector1,id:1153,x:31662,y:32974,varname:node_1153,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Time,id:7748,x:31662,y:32823,varname:node_7748,prsc:2;n:type:ShaderForge.SFN_Multiply,id:8400,x:31872,y:32772,varname:node_8400,prsc:2|A-7748-T,B-1153-OUT;n:type:ShaderForge.SFN_Append,id:4163,x:32831,y:32521,varname:node_4163,prsc:2|A-9351-OUT,B-3107-OUT;n:type:ShaderForge.SFN_Vector1,id:3107,x:32656,y:32717,varname:node_3107,prsc:2,v1:0;n:type:ShaderForge.SFN_Sin,id:4837,x:32381,y:32480,varname:node_4837,prsc:2|IN-3237-OUT;n:type:ShaderForge.SFN_Add,id:3237,x:32213,y:32480,varname:node_3237,prsc:2|A-9811-UVOUT,B-8400-OUT;n:type:ShaderForge.SFN_Add,id:9351,x:32571,y:32480,varname:node_9351,prsc:2|A-4837-OUT,B-1685-OUT;n:type:ShaderForge.SFN_Vector1,id:1685,x:32364,y:32732,varname:node_1685,prsc:2,v1:1;proporder:9524;pass:END;sub:END;*/

Shader "Shader Forge/shaderCharles3" {
    Properties {
        _node_9524 ("node_9524", 2D) = "white" {}
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
            uniform sampler2D _node_9524; uniform float4 _node_9524_ST;
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
                float4 node_7748 = _Time + _TimeEditor;
                float3 node_4163 = float3((sin((i.uv0+(node_7748.g*0.5)))+1.0),0.0);
                float4 node_2789 = tex2D(_node_9524,TRANSFORM_TEX(node_4163, _node_9524));
                float3 emissive = node_2789.rgb;
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
