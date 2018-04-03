// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:False,qofs:0,qpre:1,rntp:1,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4013,x:33312,y:32551,varname:node_4013,prsc:2|emission-4153-RGB;n:type:ShaderForge.SFN_Tex2d,id:4153,x:33045,y:32639,varname:_node_901,prsc:2,tex:5460ce9a94a888d49b2ef077fe772df7,ntxv:0,isnm:False|UVIN-4679-OUT,TEX-1127-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:6132,x:32553,y:32952,ptovrint:False,ptlb:node_6132,ptin:_node_6132,varname:_node_6132,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3e54f5471adcf2348a7637785b97461f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:6351,x:31722,y:32581,varname:node_6351,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:6256,x:31923,y:32583,varname:node_6256,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-6351-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:5442,x:32085,y:32583,varname:node_5442,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6256-OUT;n:type:ShaderForge.SFN_ArcTan2,id:9710,x:32438,y:32597,varname:node_9710,prsc:2,attp:2|A-5442-G,B-9121-OUT;n:type:ShaderForge.SFN_Slider,id:2895,x:31884,y:32902,ptovrint:False,ptlb:node_2895,ptin:_node_2895,varname:_node_2895,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1,max:1;n:type:ShaderForge.SFN_Append,id:4679,x:32702,y:32596,varname:node_4679,prsc:2|A-9710-OUT,B-470-OUT;n:type:ShaderForge.SFN_Vector1,id:470,x:32594,y:32822,varname:node_470,prsc:2,v1:0;n:type:ShaderForge.SFN_Multiply,id:9121,x:32270,y:32775,varname:node_9121,prsc:2|A-5442-R,B-2895-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:1127,x:32828,y:32771,ptovrint:False,ptlb:node_1127,ptin:_node_1127,varname:_node_1127,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:5460ce9a94a888d49b2ef077fe772df7,ntxv:0,isnm:False;proporder:6132-2895-1127;pass:END;sub:END;*/

Shader "Shader Forge/shaderCharlesDegrade" {
    Properties {
        _node_6132 ("node_6132", 2D) = "white" {}
        _node_2895 ("node_2895", Range(0, 1)) = 1
        _node_1127 ("node_1127", 2D) = "white" {}
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
            uniform float _node_2895;
            uniform sampler2D _node_1127; uniform float4 _node_1127_ST;
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
                float2 node_5442 = (i.uv0*2.0+-1.0).rg;
                float2 node_4679 = float2(((atan2(node_5442.g,(node_5442.r*_node_2895))/6.28318530718)+0.5),0.0);
                float4 _node_901 = tex2D(_node_1127,TRANSFORM_TEX(node_4679, _node_1127));
                float3 emissive = _node_901.rgb;
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
