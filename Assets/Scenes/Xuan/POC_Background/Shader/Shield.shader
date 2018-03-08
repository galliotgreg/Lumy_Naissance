// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32631,y:32404,varname:node_2865,prsc:2|emission-7440-OUT;n:type:ShaderForge.SFN_Tex2d,id:1230,x:31843,y:32677,ptovrint:False,ptlb:node_1230,ptin:_node_1230,varname:node_1230,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:b5da36e34cb54394ca362696b93e5807,ntxv:0,isnm:False|UVIN-7000-UVOUT;n:type:ShaderForge.SFN_Fresnel,id:6596,x:31801,y:32848,varname:node_6596,prsc:2|EXP-6083-OUT;n:type:ShaderForge.SFN_Multiply,id:9815,x:32055,y:32677,varname:node_9815,prsc:2|A-1230-RGB,B-6596-OUT,C-6091-OUT;n:type:ShaderForge.SFN_Clamp01,id:8405,x:32226,y:32677,varname:node_8405,prsc:2|IN-9815-OUT;n:type:ShaderForge.SFN_Color,id:8047,x:32226,y:32460,ptovrint:False,ptlb:node_8047,ptin:_node_8047,varname:node_8047,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.7508412,c2:0.7941176,c3:0.07006922,c4:1;n:type:ShaderForge.SFN_SceneColor,id:9450,x:32056,y:32304,varname:node_9450,prsc:2;n:type:ShaderForge.SFN_Clamp01,id:6994,x:32226,y:32304,varname:node_6994,prsc:2|IN-9450-RGB;n:type:ShaderForge.SFN_Lerp,id:7440,x:32445,y:32505,varname:node_7440,prsc:2|A-6994-OUT,B-8047-RGB,T-8405-OUT;n:type:ShaderForge.SFN_Slider,id:6083,x:31457,y:32866,ptovrint:False,ptlb:Fresnel strench,ptin:_Fresnelstrench,varname:node_6083,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.215539,max:3;n:type:ShaderForge.SFN_Slider,id:6091,x:31644,y:33044,ptovrint:False,ptlb:Transparent,ptin:_Transparent,varname:node_6091,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2,max:2;n:type:ShaderForge.SFN_TexCoord,id:6486,x:31101,y:32404,varname:node_6486,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:9131,x:31553,y:32489,varname:node_9131,prsc:2,spu:0,spv:1|UVIN-6486-UVOUT,DIST-4800-OUT;n:type:ShaderForge.SFN_Panner,id:7000,x:31801,y:32489,varname:node_7000,prsc:2,spu:1,spv:0|UVIN-9131-UVOUT,DIST-7253-OUT;n:type:ShaderForge.SFN_Multiply,id:4800,x:31352,y:32510,varname:node_4800,prsc:2|A-8822-T,B-5984-OUT;n:type:ShaderForge.SFN_Multiply,id:7253,x:31553,y:32668,varname:node_7253,prsc:2|A-8822-T,B-8642-OUT;n:type:ShaderForge.SFN_Time,id:8822,x:31101,y:32558,varname:node_8822,prsc:2;n:type:ShaderForge.SFN_Slider,id:5984,x:30975,y:32717,ptovrint:False,ptlb:ChangeSpeedvertical,ptin:_ChangeSpeedvertical,varname:node_5984,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.165363,max:1;n:type:ShaderForge.SFN_Slider,id:8642,x:30975,y:32820,ptovrint:False,ptlb:ChangeSpeedHorizontal,ptin:_ChangeSpeedHorizontal,varname:node_8642,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1822442,max:1;proporder:1230-8047-6083-6091-5984-8642;pass:END;sub:END;*/

Shader "Shader Forge/Shield" {
    Properties {
        _node_1230 ("node_1230", 2D) = "white" {}
        _node_8047 ("node_8047", Color) = (0.7508412,0.7941176,0.07006922,1)
        _Fresnelstrench ("Fresnel strench", Range(0, 3)) = 2.215539
        _Transparent ("Transparent", Range(0, 2)) = 2
        _ChangeSpeedvertical ("ChangeSpeedvertical", Range(0, 1)) = 0.165363
        _ChangeSpeedHorizontal ("ChangeSpeedHorizontal", Range(0, 1)) = 0.1822442
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        GrabPass{ }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="ForwardBase"
            }
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
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_1230; uniform float4 _node_1230_ST;
            uniform float4 _node_8047;
            uniform float _Fresnelstrench;
            uniform float _Transparent;
            uniform float _ChangeSpeedvertical;
            uniform float _ChangeSpeedHorizontal;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
                UNITY_FOG_COORDS(4)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
////// Lighting:
////// Emissive:
                float4 node_8822 = _Time + _TimeEditor;
                float2 node_7000 = ((i.uv0+(node_8822.g*_ChangeSpeedvertical)*float2(0,1))+(node_8822.g*_ChangeSpeedHorizontal)*float2(1,0));
                float4 _node_1230_var = tex2D(_node_1230,TRANSFORM_TEX(node_7000, _node_1230));
                float3 emissive = lerp(saturate(sceneColor.rgb),_node_8047.rgb,saturate((_node_1230_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnelstrench)*_Transparent)));
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
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform sampler2D _node_1230; uniform float4 _node_1230_ST;
            uniform float4 _node_8047;
            uniform float _Fresnelstrench;
            uniform float _Transparent;
            uniform float _ChangeSpeedvertical;
            uniform float _ChangeSpeedHorizontal;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float4 screenPos : TEXCOORD3;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                #if UNITY_UV_STARTS_AT_TOP
                    float grabSign = -_ProjectionParams.x;
                #else
                    float grabSign = _ProjectionParams.x;
                #endif
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5;
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float4 node_8822 = _Time + _TimeEditor;
                float2 node_7000 = ((i.uv0+(node_8822.g*_ChangeSpeedvertical)*float2(0,1))+(node_8822.g*_ChangeSpeedHorizontal)*float2(1,0));
                float4 _node_1230_var = tex2D(_node_1230,TRANSFORM_TEX(node_7000, _node_1230));
                o.Emission = lerp(saturate(sceneColor.rgb),_node_8047.rgb,saturate((_node_1230_var.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Fresnelstrench)*_Transparent)));
                
                float3 diffColor = float3(0,0,0);
                float specularMonochrome;
                float3 specColor;
                diffColor = DiffuseAndSpecularFromMetallic( diffColor, 0, specColor, specularMonochrome );
                o.Albedo = diffColor;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
