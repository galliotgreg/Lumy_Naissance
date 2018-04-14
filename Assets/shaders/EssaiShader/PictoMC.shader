// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:1,cusa:True,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:True,tesm:0,olmd:1,culm:2,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:1873,x:33229,y:32719,varname:node_1873,prsc:2|emission-7484-OUT,alpha-4805-A;n:type:ShaderForge.SFN_Tex2d,id:4805,x:32824,y:32773,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:True,tagnsco:False,tagnrm:False,tex:1187e2173627e854b9fed7d443a44922,ntxv:0,isnm:False|UVIN-3317-OUT;n:type:ShaderForge.SFN_Tex2d,id:6151,x:32311,y:32849,ptovrint:False,ptlb:node_6151,ptin:_node_6151,varname:node_6151,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-9750-UVOUT;n:type:ShaderForge.SFN_Lerp,id:3317,x:32610,y:32773,varname:node_3317,prsc:2|A-1645-UVOUT,B-6151-R,T-6302-OUT;n:type:ShaderForge.SFN_TexCoord,id:1645,x:32207,y:32635,varname:node_1645,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:9750,x:31973,y:32812,varname:node_9750,prsc:2,spu:0.35,spv:0.2|UVIN-2886-UVOUT;n:type:ShaderForge.SFN_Slider,id:6302,x:31987,y:33143,ptovrint:False,ptlb:NoiseIntensity,ptin:_NoiseIntensity,varname:node_6302,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.01874431,max:0.1;n:type:ShaderForge.SFN_TexCoord,id:2886,x:31692,y:32872,varname:node_2886,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Slider,id:664,x:32597,y:32569,ptovrint:False,ptlb:EmissiveIntensity,ptin:_EmissiveIntensity,varname:_node_6302_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:1.068381,max:5;n:type:ShaderForge.SFN_Multiply,id:7484,x:33044,y:32588,varname:node_7484,prsc:2|A-4805-A,B-664-OUT;proporder:4805-6151-6302-664;pass:END;sub:END;*/

Shader "Shader Forge/PictoMC" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _node_6151 ("node_6151", 2D) = "white" {}
        _NoiseIntensity ("NoiseIntensity", Range(0, 0.1)) = 0.01874431
        _EmissiveIntensity ("EmissiveIntensity", Range(0, 5)) = 1.068381
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
        [MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
            "CanUseSpriteAtlas"="True"
            "PreviewType"="Plane"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform sampler2D _node_6151; uniform float4 _node_6151_ST;
            uniform float _NoiseIntensity;
            uniform float _EmissiveIntensity;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
////// Lighting:
////// Emissive:
                float4 node_1294 = _Time + _TimeEditor;
                float2 node_9750 = (i.uv0+node_1294.g*float2(0.35,0.2));
                float4 _node_6151_var = tex2D(_node_6151,TRANSFORM_TEX(node_9750, _node_6151));
                float2 node_3317 = lerp(i.uv0,float2(_node_6151_var.r,_node_6151_var.r),_NoiseIntensity);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_3317, _MainTex));
                float node_7484 = (_MainTex_var.a*_EmissiveIntensity);
                float3 emissive = float3(node_7484,node_7484,node_7484);
                float3 finalColor = emissive;
                return fixed4(finalColor,_MainTex_var.a);
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
            #pragma multi_compile _ PIXELSNAP_ON
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            struct VertexInput {
                float4 vertex : POSITION;
            };
            struct VertexOutput {
                V2F_SHADOW_CASTER;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.pos = UnityObjectToClipPos( v.vertex );
                #ifdef PIXELSNAP_ON
                    o.pos = UnityPixelSnap(o.pos);
                #endif
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
