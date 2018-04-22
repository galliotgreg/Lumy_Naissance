// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:3,spmd:0,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:True,hqlp:False,rprd:True,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:2865,x:32695,y:32659,varname:node_2865,prsc:2|diff-2953-OUT,diffpow-6974-OUT,spec-3051-OUT,gloss-8307-OUT,normal-640-OUT,emission-7167-OUT,alpha-6594-OUT,refract-5655-OUT;n:type:ShaderForge.SFN_Lerp,id:640,x:31832,y:32555,varname:node_640,prsc:2|A-3760-OUT,B-6754-RGB,T-7814-OUT;n:type:ShaderForge.SFN_ScreenPos,id:2468,x:31594,y:32110,varname:node_2468,prsc:2,sctp:0;n:type:ShaderForge.SFN_Add,id:2953,x:32030,y:31966,varname:node_2953,prsc:2|A-1864-RGB,B-2468-UVOUT;n:type:ShaderForge.SFN_Color,id:1864,x:31614,y:31963,ptovrint:False,ptlb:node_1864,ptin:_node_1864,varname:node_1864,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.1,c2:0.6,c3:0.5,c4:1;n:type:ShaderForge.SFN_Vector1,id:6974,x:31864,y:32158,varname:node_6974,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Vector1,id:3051,x:31835,y:32221,varname:node_3051,prsc:2,v1:8;n:type:ShaderForge.SFN_Vector1,id:8307,x:31864,y:32291,varname:node_8307,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Dot,id:7167,x:32209,y:32778,varname:node_7167,prsc:2,dt:0|A-517-OUT,B-7124-OUT;n:type:ShaderForge.SFN_Fresnel,id:517,x:31714,y:32352,varname:node_517,prsc:2|EXP-7711-OUT;n:type:ShaderForge.SFN_NormalVector,id:7124,x:31919,y:32787,prsc:2,pt:False;n:type:ShaderForge.SFN_Vector1,id:7711,x:31289,y:32274,varname:node_7711,prsc:2,v1:0.9;n:type:ShaderForge.SFN_Slider,id:6594,x:32116,y:32975,ptovrint:False,ptlb:node_6594,ptin:_node_6594,varname:node_6594,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0.1082388,max:1;n:type:ShaderForge.SFN_Vector3,id:3760,x:31441,y:32503,varname:node_3760,prsc:2,v1:0,v2:0,v3:1;n:type:ShaderForge.SFN_Vector1,id:7814,x:31413,y:32721,varname:node_7814,prsc:2,v1:0.1;n:type:ShaderForge.SFN_Tex2d,id:6754,x:31413,y:32852,ptovrint:False,ptlb:node_6754,ptin:_node_6754,varname:node_6754,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c245121c8792be6478a73bb6f316a3f8,ntxv:3,isnm:True|UVIN-4023-OUT;n:type:ShaderForge.SFN_Add,id:4023,x:31073,y:32750,varname:node_4023,prsc:2|A-4445-UVOUT,B-7418-OUT;n:type:ShaderForge.SFN_TexCoord,id:4445,x:30779,y:32549,varname:node_4445,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:7418,x:30779,y:32774,varname:node_7418,prsc:2|A-4635-OUT,B-1441-TSL;n:type:ShaderForge.SFN_Time,id:1441,x:30475,y:32796,varname:node_1441,prsc:2;n:type:ShaderForge.SFN_Append,id:4635,x:30505,y:32626,varname:node_4635,prsc:2|A-9294-OUT,B-5514-OUT;n:type:ShaderForge.SFN_Vector1,id:9294,x:30184,y:32528,varname:node_9294,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:5514,x:30165,y:32661,varname:node_5514,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_ComponentMask,id:3350,x:31633,y:33026,varname:node_3350,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6754-RGB;n:type:ShaderForge.SFN_Add,id:9622,x:31919,y:33121,varname:node_9622,prsc:2|A-3350-OUT,B-4165-OUT;n:type:ShaderForge.SFN_ComponentMask,id:4165,x:31672,y:33228,varname:node_4165,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-9734-RGB;n:type:ShaderForge.SFN_Tex2d,id:9734,x:31274,y:33168,ptovrint:False,ptlb:node_9734,ptin:_node_9734,varname:node_9734,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:c245121c8792be6478a73bb6f316a3f8,ntxv:3,isnm:True|UVIN-4240-OUT;n:type:ShaderForge.SFN_Add,id:4240,x:30954,y:33092,varname:node_4240,prsc:2|A-874-UVOUT,B-4369-OUT;n:type:ShaderForge.SFN_TexCoord,id:874,x:30636,y:32979,varname:node_874,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:4369,x:30701,y:33192,varname:node_4369,prsc:2|A-3147-OUT,B-3918-TSL;n:type:ShaderForge.SFN_Append,id:3147,x:30384,y:33078,varname:node_3147,prsc:2|A-713-OUT,B-7764-OUT;n:type:ShaderForge.SFN_Time,id:3918,x:30367,y:33274,varname:node_3918,prsc:2;n:type:ShaderForge.SFN_Vector1,id:7764,x:30085,y:33113,varname:node_7764,prsc:2,v1:1;n:type:ShaderForge.SFN_Vector1,id:713,x:30075,y:33004,varname:node_713,prsc:2,v1:-0.5;n:type:ShaderForge.SFN_Multiply,id:5655,x:32417,y:33160,varname:node_5655,prsc:2|A-9622-OUT,B-5009-OUT;n:type:ShaderForge.SFN_Slider,id:5009,x:32068,y:33396,ptovrint:False,ptlb:node_5009,ptin:_node_5009,varname:node_5009,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;proporder:1864-6594-6754-9734-5009;pass:END;sub:END;*/

Shader "Shader Forge/WaterV2_Shader" {
    Properties {
        _node_1864 ("node_1864", Color) = (0.1,0.6,0.5,1)
        _node_6594 ("node_6594", Range(0, 1)) = 0.1082388
        _node_6754 ("node_6754", 2D) = "bump" {}
        _node_9734 ("node_9734", 2D) = "bump" {}
        _node_5009 ("node_5009", Range(0, 1)) = 0
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform float4 _node_1864;
            uniform float _node_6594;
            uniform sampler2D _node_6754; uniform float4 _node_6754_ST;
            uniform sampler2D _node_9734; uniform float4 _node_9734_ST;
            uniform float _node_5009;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 screenPos : TEXCOORD7;
                UNITY_FOG_COORDS(8)
                #if defined(LIGHTMAP_ON) || defined(UNITY_SHOULD_SAMPLE_SH)
                    float4 ambientOrLightmapUV : TEXCOORD9;
                #endif
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                #ifdef LIGHTMAP_ON
                    o.ambientOrLightmapUV.xy = v.texcoord1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
                    o.ambientOrLightmapUV.zw = 0;
                #elif UNITY_SHOULD_SAMPLE_SH
                #endif
                #ifdef DYNAMICLIGHTMAP_ON
                    o.ambientOrLightmapUV.zw = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
                #endif
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
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
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_1441 = _Time + _TimeEditor;
                float2 node_4023 = (i.uv0+(float2(1.0,(-0.5))*node_1441.r));
                float3 _node_6754_var = UnpackNormal(tex2D(_node_6754,TRANSFORM_TEX(node_4023, _node_6754)));
                float3 normalLocal = lerp(float3(0,0,1),_node_6754_var.rgb,0.1);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float3 viewReflectDirection = reflect( -viewDirection, normalDirection );
                float4 node_3918 = _Time + _TimeEditor;
                float2 node_4240 = (i.uv0+(float2((-0.5),1.0)*node_3918.r));
                float3 _node_9734_var = UnpackNormal(tex2D(_node_9734,TRANSFORM_TEX(node_4240, _node_9734)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + ((_node_6754_var.rgb.rg+_node_9734_var.rgb.rg)*_node_5009);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(_WorldSpaceLightPos0.xyz);
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = 1;
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.9;
                float perceptualRoughness = 1.0 - 0.9;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
/////// GI Data:
                UnityLight light;
                #ifdef LIGHTMAP_OFF
                    light.color = lightColor;
                    light.dir = lightDirection;
                    light.ndotl = LambertTerm (normalDirection, light.dir);
                #else
                    light.color = half3(0.f, 0.f, 0.f);
                    light.ndotl = 0.0f;
                    light.dir = half3(0.f, 0.f, 0.f);
                #endif
                UnityGIInput d;
                d.light = light;
                d.worldPos = i.posWorld.xyz;
                d.worldViewDir = viewDirection;
                d.atten = attenuation;
                #if defined(LIGHTMAP_ON) || defined(DYNAMICLIGHTMAP_ON)
                    d.ambient = 0;
                    d.lightmapUV = i.ambientOrLightmapUV;
                #else
                    d.ambient = i.ambientOrLightmapUV;
                #endif
                #if UNITY_SPECCUBE_BLENDING || UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMin[0] = unity_SpecCube0_BoxMin;
                    d.boxMin[1] = unity_SpecCube1_BoxMin;
                #endif
                #if UNITY_SPECCUBE_BOX_PROJECTION
                    d.boxMax[0] = unity_SpecCube0_BoxMax;
                    d.boxMax[1] = unity_SpecCube1_BoxMax;
                    d.probePosition[0] = unity_SpecCube0_ProbePosition;
                    d.probePosition[1] = unity_SpecCube1_ProbePosition;
                #endif
                d.probeHDR[0] = unity_SpecCube0_HDR;
                d.probeHDR[1] = unity_SpecCube1_HDR;
                Unity_GlossyEnvironmentData ugls_en_data;
                ugls_en_data.roughness = 1.0 - gloss;
                ugls_en_data.reflUVW = viewReflectDirection;
                UnityGI gi = UnityGlobalIllumination(d, 1, normalDirection, ugls_en_data );
                lightDirection = gi.light.dir;
                lightColor = gi.light.color;
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float node_3051 = 8.0;
                float3 specularColor = float3(node_3051,node_3051,node_3051);
                float specularMonochrome;
                float3 diffuseColor = (_node_1864.rgb+float3(i.screenPos.rg,0.0)); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                half surfaceReduction;
                #ifdef UNITY_COLORSPACE_GAMMA
                    surfaceReduction = 1.0-0.28*roughness*perceptualRoughness;
                #else
                    surfaceReduction = 1.0/(roughness*roughness + 1.0);
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                half grazingTerm = saturate( gloss + specularMonochrome );
                float3 indirectSpecular = (gi.indirect.specular);
                indirectSpecular *= FresnelLerp (specularColor, grazingTerm, NdotV);
                indirectSpecular *= surfaceReduction;
                float3 specular = (directSpecular + indirectSpecular);
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                float3 indirectDiffuse = float3(0,0,0);
                indirectDiffuse += gi.indirect.diffuse;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = (directDiffuse + indirectDiffuse) * diffuseColor;
////// Emissive:
                float node_7167 = dot(pow(1.0-max(0,dot(normalDirection, viewDirection)),0.9),i.normalDir);
                float3 emissive = float3(node_7167,node_7167,node_7167);
/// Final Color:
                float3 finalColor = diffuse + specular + emissive;
                fixed4 finalRGBA = fixed4(lerp(sceneColor.rgb, finalColor,_node_6594),1);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
        Pass {
            Name "FORWARD_DELTA"
            Tags {
                "LightMode"="ForwardAdd"
            }
            Blend One One
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDADD
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "AutoLight.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #pragma multi_compile_fwdadd
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform sampler2D _GrabTexture;
            uniform float4 _TimeEditor;
            uniform float4 _node_1864;
            uniform float _node_6594;
            uniform sampler2D _node_6754; uniform float4 _node_6754_ST;
            uniform sampler2D _node_9734; uniform float4 _node_9734_ST;
            uniform float _node_5009;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float2 uv1 : TEXCOORD1;
                float2 uv2 : TEXCOORD2;
                float4 posWorld : TEXCOORD3;
                float3 normalDir : TEXCOORD4;
                float3 tangentDir : TEXCOORD5;
                float3 bitangentDir : TEXCOORD6;
                float4 screenPos : TEXCOORD7;
                LIGHTING_COORDS(8,9)
                UNITY_FOG_COORDS(10)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                float3 lightColor = _LightColor0.rgb;
                o.pos = UnityObjectToClipPos( v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                o.screenPos = o.pos;
                TRANSFER_VERTEX_TO_FRAGMENT(o)
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
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float4 node_1441 = _Time + _TimeEditor;
                float2 node_4023 = (i.uv0+(float2(1.0,(-0.5))*node_1441.r));
                float3 _node_6754_var = UnpackNormal(tex2D(_node_6754,TRANSFORM_TEX(node_4023, _node_6754)));
                float3 normalLocal = lerp(float3(0,0,1),_node_6754_var.rgb,0.1);
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
                float4 node_3918 = _Time + _TimeEditor;
                float2 node_4240 = (i.uv0+(float2((-0.5),1.0)*node_3918.r));
                float3 _node_9734_var = UnpackNormal(tex2D(_node_9734,TRANSFORM_TEX(node_4240, _node_9734)));
                float2 sceneUVs = float2(1,grabSign)*i.screenPos.xy*0.5+0.5 + ((_node_6754_var.rgb.rg+_node_9734_var.rgb.rg)*_node_5009);
                float4 sceneColor = tex2D(_GrabTexture, sceneUVs);
                float3 lightDirection = normalize(lerp(_WorldSpaceLightPos0.xyz, _WorldSpaceLightPos0.xyz - i.posWorld.xyz,_WorldSpaceLightPos0.w));
                float3 lightColor = _LightColor0.rgb;
                float3 halfDirection = normalize(viewDirection+lightDirection);
////// Lighting:
                float attenuation = LIGHT_ATTENUATION(i);
                float3 attenColor = attenuation * _LightColor0.xyz;
                float Pi = 3.141592654;
                float InvPi = 0.31830988618;
///////// Gloss:
                float gloss = 0.9;
                float perceptualRoughness = 1.0 - 0.9;
                float roughness = perceptualRoughness * perceptualRoughness;
                float specPow = exp2( gloss * 10.0 + 1.0 );
////// Specular:
                float NdotL = saturate(dot( normalDirection, lightDirection ));
                float LdotH = saturate(dot(lightDirection, halfDirection));
                float node_3051 = 8.0;
                float3 specularColor = float3(node_3051,node_3051,node_3051);
                float specularMonochrome;
                float3 diffuseColor = (_node_1864.rgb+float3(i.screenPos.rg,0.0)); // Need this for specular when using metallic
                diffuseColor = EnergyConservationBetweenDiffuseAndSpecular(diffuseColor, specularColor, specularMonochrome);
                specularMonochrome = 1.0-specularMonochrome;
                float NdotV = abs(dot( normalDirection, viewDirection ));
                float NdotH = saturate(dot( normalDirection, halfDirection ));
                float VdotH = saturate(dot( viewDirection, halfDirection ));
                float visTerm = SmithJointGGXVisibilityTerm( NdotL, NdotV, roughness );
                float normTerm = GGXTerm(NdotH, roughness);
                float specularPBL = (visTerm*normTerm) * UNITY_PI;
                #ifdef UNITY_COLORSPACE_GAMMA
                    specularPBL = sqrt(max(1e-4h, specularPBL));
                #endif
                specularPBL = max(0, specularPBL * NdotL);
                #if defined(_SPECULARHIGHLIGHTS_OFF)
                    specularPBL = 0.0;
                #endif
                specularPBL *= any(specularColor) ? 1.0 : 0.0;
                float3 directSpecular = attenColor*specularPBL*FresnelTerm(specularColor, LdotH);
                float3 specular = directSpecular;
/////// Diffuse:
                NdotL = max(0.0,dot( normalDirection, lightDirection ));
                half fd90 = 0.5 + 2 * LdotH * LdotH * (1-gloss);
                float nlPow5 = Pow5(1-NdotL);
                float nvPow5 = Pow5(1-NdotV);
                float3 directDiffuse = ((1 +(fd90 - 1)*nlPow5) * (1 + (fd90 - 1)*nvPow5) * NdotL) * attenColor;
                diffuseColor *= 1-specularMonochrome;
                float3 diffuse = directDiffuse * diffuseColor;
/// Final Color:
                float3 finalColor = diffuse + specular;
                fixed4 finalRGBA = fixed4(finalColor * _node_6594,0);
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
            #define SHOULD_SAMPLE_SH ( defined (LIGHTMAP_OFF) && defined(DYNAMICLIGHTMAP_OFF) )
            #define _GLOSSYENV 1
            #include "UnityCG.cginc"
            #include "Lighting.cginc"
            #include "UnityPBSLighting.cginc"
            #include "UnityStandardBRDF.cginc"
            #include "UnityMetaPass.cginc"
            #pragma fragmentoption ARB_precision_hint_fastest
            #pragma multi_compile_shadowcaster
            #pragma multi_compile LIGHTMAP_OFF LIGHTMAP_ON
            #pragma multi_compile DIRLIGHTMAP_OFF DIRLIGHTMAP_COMBINED DIRLIGHTMAP_SEPARATE
            #pragma multi_compile DYNAMICLIGHTMAP_OFF DYNAMICLIGHTMAP_ON
            #pragma multi_compile_fog
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _node_1864;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float2 texcoord1 : TEXCOORD1;
                float2 texcoord2 : TEXCOORD2;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv1 : TEXCOORD0;
                float2 uv2 : TEXCOORD1;
                float4 posWorld : TEXCOORD2;
                float3 normalDir : TEXCOORD3;
                float4 screenPos : TEXCOORD4;
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv1 = v.texcoord1;
                o.uv2 = v.texcoord2;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = UnityMetaVertexPosition(v.vertex, v.texcoord1.xy, v.texcoord2.xy, unity_LightmapST, unity_DynamicLightmapST );
                o.screenPos = o.pos;
                return o;
            }
            float4 frag(VertexOutput i) : SV_Target {
                i.normalDir = normalize(i.normalDir);
                i.screenPos = float4( i.screenPos.xy / i.screenPos.w, 0, 0 );
                i.screenPos.y *= _ProjectionParams.x;
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 normalDirection = i.normalDir;
                UnityMetaInput o;
                UNITY_INITIALIZE_OUTPUT( UnityMetaInput, o );
                
                float node_7167 = dot(pow(1.0-max(0,dot(normalDirection, viewDirection)),0.9),i.normalDir);
                o.Emission = float3(node_7167,node_7167,node_7167);
                
                float3 diffColor = (_node_1864.rgb+float3(i.screenPos.rg,0.0));
                float node_3051 = 8.0;
                float3 specColor = float3(node_3051,node_3051,node_3051);
                float specularMonochrome = max(max(specColor.r, specColor.g),specColor.b);
                diffColor *= (1.0-specularMonochrome);
                float roughness = 1.0 - 0.9;
                o.Albedo = diffColor + specColor * roughness * roughness * 0.5;
                
                return UnityMetaFragment( o );
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
