// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:0,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.2862745,fgcg:0.627451,fgcb:0.8666667,fgca:1,fgde:0.007,fgrn:100,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:33149,y:32687,varname:node_2865,prsc:2|emission-4411-OUT;n:type:ShaderForge.SFN_Multiply,id:6343,x:31412,y:32978,varname:node_6343,prsc:2|A-6703-UVOUT,B-6813-OUT;n:type:ShaderForge.SFN_Slider,id:358,x:31602,y:33122,ptovrint:False,ptlb:Thickeness,ptin:_Thickeness,varname:node_358,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.7819036,max:1;n:type:ShaderForge.SFN_Panner,id:5577,x:31300,y:32667,varname:node_5577,prsc:2,spu:0.01,spv:0.01|UVIN-6703-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:199,x:31566,y:32680,ptovrint:False,ptlb:node_199,ptin:_node_199,varname:node_199,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c6f974cb4a3d066449f69528719d8a3d,ntxv:0,isnm:False|UVIN-5577-UVOUT;n:type:ShaderForge.SFN_TexCoord,id:6703,x:31043,y:32871,varname:node_6703,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:6813,x:31179,y:33204,varname:node_6813,prsc:2,v1:0.35;n:type:ShaderForge.SFN_RemapRange,id:6845,x:31727,y:32680,varname:node_6845,prsc:2,frmn:0,frmx:1,tomn:0,tomx:0.05|IN-199-G;n:type:ShaderForge.SFN_Add,id:9302,x:31936,y:32756,varname:node_9302,prsc:2|A-6845-OUT,B-6343-OUT;n:type:ShaderForge.SFN_Tex2dAsset,id:296,x:31983,y:32531,ptovrint:False,ptlb:node_296,ptin:_node_296,varname:node_296,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b09d56f82614f7741aa615d8827318bd,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1860,x:32150,y:32639,varname:node_1860,prsc:2,tex:b09d56f82614f7741aa615d8827318bd,ntxv:0,isnm:False|UVIN-9302-OUT,TEX-296-TEX;n:type:ShaderForge.SFN_Subtract,id:934,x:32379,y:32817,varname:node_934,prsc:2|A-1860-RGB,B-5965-OUT;n:type:ShaderForge.SFN_OneMinus,id:5965,x:32044,y:32984,varname:node_5965,prsc:2|IN-358-OUT;n:type:ShaderForge.SFN_Multiply,id:5460,x:32501,y:33046,varname:node_5460,prsc:2|A-9262-OUT,B-5002-A;n:type:ShaderForge.SFN_VertexColor,id:5002,x:32192,y:33255,varname:node_5002,prsc:2;n:type:ShaderForge.SFN_Multiply,id:9262,x:32271,y:33046,varname:node_9262,prsc:2|A-934-OUT,B-122-OUT;n:type:ShaderForge.SFN_Slider,id:122,x:31627,y:33336,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_122,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.3400279,max:1;n:type:ShaderForge.SFN_Color,id:7252,x:32471,y:33301,ptovrint:False,ptlb:node_7252,ptin:_node_7252,varname:node_7252,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.4632353,c2:0.9111561,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:2113,x:32733,y:32975,varname:node_2113,prsc:2|A-5460-OUT,B-7252-RGB;n:type:ShaderForge.SFN_Vector1,id:3510,x:32654,y:32744,varname:node_3510,prsc:2,v1:2;n:type:ShaderForge.SFN_Multiply,id:4411,x:32891,y:32784,varname:node_4411,prsc:2|A-3510-OUT,B-2113-OUT;proporder:358-199-296-122-7252;pass:END;sub:END;*/

Shader "Shader Forge/Caustics_Shader" {
    Properties {
        _Thickeness ("Thickeness", Range(0, 1)) = 0.7819036
        _node_199 ("node_199", 2D) = "white" {}
        _node_296 ("node_296", 2D) = "white" {}
        _Opacity ("Opacity", Range(0, 1)) = 0.3400279
        _node_7252 ("node_7252", Color) = (0.4632353,0.9111561,1,1)
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
            uniform float _Thickeness;
            uniform sampler2D _node_199; uniform float4 _node_199_ST;
            uniform sampler2D _node_296; uniform float4 _node_296_ST;
            uniform float _Opacity;
            uniform float4 _node_7252;
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
                float4 node_1241 = _Time + _TimeEditor;
                float2 node_5577 = (i.uv0+node_1241.g*float2(0.01,0.01));
                float4 _node_199_var = tex2D(_node_199,TRANSFORM_TEX(node_5577, _node_199));
                float2 node_9302 = ((_node_199_var.g*0.05+0.0)+(i.uv0*0.35));
                float4 node_1860 = tex2D(_node_296,TRANSFORM_TEX(node_9302, _node_296));
                float3 emissive = (2.0*((((node_1860.rgb-(1.0 - _Thickeness))*_Opacity)*i.vertexColor.a)*_node_7252.rgb));
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
            uniform float _Thickeness;
            uniform sampler2D _node_199; uniform float4 _node_199_ST;
            uniform sampler2D _node_296; uniform float4 _node_296_ST;
            uniform float _Opacity;
            uniform float4 _node_7252;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
                float4 vertexColor : COLOR;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 vertexColor : COLOR;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.vertexColor = v.vertexColor;
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_9614 = _Time + _TimeEditor;
                float2 node_5577 = (i.uv0+node_9614.g*float2(0.01,0.01));
                float4 _node_199_var = tex2D(_node_199,TRANSFORM_TEX(node_5577, _node_199));
                float2 node_9302 = ((_node_199_var.g*0.05+0.0)+(i.uv0*0.35));
                float4 node_1860 = tex2D(_node_296,TRANSFORM_TEX(node_9302, _node_296));
                o.Emission = (2.0*((((node_1860.rgb-(1.0 - _Thickeness))*_Opacity)*i.vertexColor.a)*_node_7252.rgb));
                
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
