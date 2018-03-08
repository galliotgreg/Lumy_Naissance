// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:4266,x:33633,y:33135,varname:node_4266,prsc:2|emission-308-OUT,alpha-4974-OUT;n:type:ShaderForge.SFN_Tex2d,id:2737,x:31778,y:32591,ptovrint:False,ptlb:node_7984,ptin:_node_7984,varname:node_7984,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f1b5df310926b284cadcaeda17ed2676,ntxv:0,isnm:False|UVIN-3713-UVOUT;n:type:ShaderForge.SFN_RemapRange,id:9155,x:32028,y:32573,varname:node_9155,prsc:2,frmn:0,frmx:1,tomn:1,tomx:1|IN-2737-R;n:type:ShaderForge.SFN_TexCoord,id:4167,x:31105,y:32711,varname:node_4167,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:3890,x:31118,y:32643,varname:node_3890,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:7897,x:31350,y:32676,varname:node_7897,prsc:2|A-3890-OUT,B-4167-UVOUT;n:type:ShaderForge.SFN_Panner,id:3713,x:31541,y:32666,varname:node_3713,prsc:2,spu:1,spv:1|UVIN-7897-OUT;n:type:ShaderForge.SFN_Multiply,id:7527,x:32245,y:32591,varname:node_7527,prsc:2|A-9155-OUT,B-5744-OUT;n:type:ShaderForge.SFN_Vector1,id:5744,x:32028,y:32750,varname:node_5744,prsc:2,v1:0.1;n:type:ShaderForge.SFN_TexCoord,id:5100,x:31164,y:32988,varname:node_5100,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:3521,x:31386,y:32960,varname:node_3521,prsc:2,frmn:0,frmx:1,tomn:1,tomx:1|IN-5100-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:4316,x:31604,y:32864,varname:node_4316,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-3521-OUT;n:type:ShaderForge.SFN_Length,id:2488,x:31604,y:33096,varname:node_2488,prsc:2|IN-3521-OUT;n:type:ShaderForge.SFN_ArcTan2,id:6156,x:31817,y:32864,varname:node_6156,prsc:2,attp:2|A-4316-G,B-4316-R;n:type:ShaderForge.SFN_Add,id:862,x:32517,y:32657,varname:node_862,prsc:2|A-7527-OUT,B-6156-OUT;n:type:ShaderForge.SFN_Append,id:4255,x:32426,y:33080,varname:node_4255,prsc:2|A-4415-OUT,B-862-OUT;n:type:ShaderForge.SFN_Multiply,id:7948,x:31955,y:33050,varname:node_7948,prsc:2|A-2488-OUT,B-6621-OUT;n:type:ShaderForge.SFN_Add,id:4415,x:32158,y:33162,varname:node_4415,prsc:2|A-7948-OUT,B-7010-OUT;n:type:ShaderForge.SFN_Vector1,id:6621,x:31886,y:33185,varname:node_6621,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Time,id:3914,x:31328,y:33276,varname:node_3914,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7010,x:31529,y:33323,varname:node_7010,prsc:2|A-3914-T,B-2683-OUT;n:type:ShaderForge.SFN_ValueProperty,id:2683,x:31328,y:33423,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_5050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Power,id:2049,x:31793,y:33392,varname:node_2049,prsc:2|VAL-2488-OUT,EXP-6025-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6025,x:31529,y:33526,ptovrint:False,ptlb:circle_thick,ptin:_circle_thick,varname:node_507,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Multiply,id:9524,x:32021,y:33495,varname:node_9524,prsc:2|A-2049-OUT,B-7734-OUT;n:type:ShaderForge.SFN_ValueProperty,id:7734,x:31793,y:33562,ptovrint:False,ptlb:ccccccc,ptin:_ccccccc,varname:node_5132,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:8110,x:32628,y:33080,ptovrint:False,ptlb:node_4422,ptin:_node_4422,varname:node_4422,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:45906d38608b22c4095d6ab84df1df41,ntxv:0,isnm:False|UVIN-4255-OUT;n:type:ShaderForge.SFN_Multiply,id:4658,x:32083,y:33368,varname:node_4658,prsc:2|A-8110-G,B-2049-OUT;n:type:ShaderForge.SFN_Add,id:4685,x:32291,y:33368,varname:node_4685,prsc:2|A-8110-R,B-4658-OUT,C-9524-OUT;n:type:ShaderForge.SFN_Multiply,id:3491,x:32500,y:33368,varname:node_3491,prsc:2|A-4685-OUT,B-5845-OUT;n:type:ShaderForge.SFN_Get,id:2358,x:31660,y:33711,varname:node_2358,prsc:2|IN-3586-OUT;n:type:ShaderForge.SFN_Get,id:291,x:31660,y:33864,varname:node_291,prsc:2|IN-3586-OUT;n:type:ShaderForge.SFN_Floor,id:2536,x:31907,y:33711,varname:node_2536,prsc:2|IN-2358-OUT;n:type:ShaderForge.SFN_OneMinus,id:7651,x:32113,y:33724,varname:node_7651,prsc:2|IN-2536-OUT;n:type:ShaderForge.SFN_Multiply,id:7560,x:31888,y:33864,varname:node_7560,prsc:2|A-291-OUT,B-5363-OUT;n:type:ShaderForge.SFN_Clamp01,id:5845,x:32113,y:33864,varname:node_5845,prsc:2|IN-7560-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5363,x:31660,y:33934,ptovrint:False,ptlb:centerGlow,ptin:_centerGlow,varname:node_6388,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_OneMinus,id:9815,x:32338,y:33921,varname:node_9815,prsc:2|IN-5845-OUT;n:type:ShaderForge.SFN_Multiply,id:8304,x:32546,y:33860,varname:node_8304,prsc:2|A-5845-OUT,B-9815-OUT;n:type:ShaderForge.SFN_Power,id:512,x:32694,y:33368,varname:node_512,prsc:2|VAL-3491-OUT,EXP-9664-OUT;n:type:ShaderForge.SFN_Vector1,id:9664,x:32599,y:33554,varname:node_9664,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Add,id:7903,x:32924,y:33368,varname:node_7903,prsc:2|A-512-OUT,B-6392-OUT,C-8304-OUT;n:type:ShaderForge.SFN_Multiply,id:6392,x:33203,y:34262,varname:node_6392,prsc:2|A-8423-OUT,B-6249-B,C-537-OUT;n:type:ShaderForge.SFN_Multiply,id:6284,x:33148,y:33368,varname:node_6284,prsc:2|A-7903-OUT,B-7651-OUT,C-8201-OUT;n:type:ShaderForge.SFN_Vector1,id:8201,x:33034,y:33587,varname:node_8201,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:4974,x:33165,y:33105,varname:node_4974,prsc:2|IN-6284-OUT;n:type:ShaderForge.SFN_Lerp,id:308,x:33266,y:32895,varname:node_308,prsc:2|A-4401-RGB,B-1610-RGB,T-4974-OUT;n:type:ShaderForge.SFN_Color,id:4401,x:32941,y:32728,ptovrint:False,ptlb:node_2677,ptin:_node_2677,varname:node_2677,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.2,c3:0.8,c4:1;n:type:ShaderForge.SFN_Color,id:1610,x:32930,y:32906,ptovrint:False,ptlb:node_232,ptin:_node_232,varname:node_232,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.7,c3:0.7,c4:1;n:type:ShaderForge.SFN_Clamp01,id:8423,x:32965,y:34262,varname:node_8423,prsc:2|IN-939-OUT;n:type:ShaderForge.SFN_ValueProperty,id:537,x:32965,y:34468,ptovrint:False,ptlb:dotGlow,ptin:_dotGlow,varname:node_1266,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:939,x:32788,y:34262,varname:node_939,prsc:2|A-7638-OUT,B-1456-OUT;n:type:ShaderForge.SFN_Multiply,id:7638,x:32585,y:34175,varname:node_7638,prsc:2|A-7322-R,B-7322-R,C-7322-R;n:type:ShaderForge.SFN_Multiply,id:1456,x:32585,y:34365,varname:node_1456,prsc:2|A-6279-R,B-6279-R,C-6279-R;n:type:ShaderForge.SFN_Tex2d,id:7322,x:32372,y:34175,varname:node_4894,prsc:2,tex:3ecffec63fe09b64889a09895349d47f,ntxv:0,isnm:False|TEX-5245-TEX;n:type:ShaderForge.SFN_Tex2d,id:6279,x:32372,y:34387,varname:node_2595,prsc:2,tex:3ecffec63fe09b64889a09895349d47f,ntxv:0,isnm:False|TEX-5245-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:5245,x:32163,y:34280,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_9948,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3ecffec63fe09b64889a09895349d47f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:6773,x:30965,y:34174,varname:node_6773,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:9445,x:30953,y:34345,varname:node_9445,prsc:2|A-9842-OUT,B-6773-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:9842,x:30850,y:34526,ptovrint:False,ptlb:centre_size,ptin:_centre_size,varname:node_1232,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Add,id:8005,x:31182,y:34345,varname:node_8005,prsc:2|A-9445-OUT,B-1378-OUT;n:type:ShaderForge.SFN_Multiply,id:6230,x:31095,y:34587,varname:node_6230,prsc:2|A-9842-OUT,B-7880-OUT;n:type:ShaderForge.SFN_Vector1,id:7880,x:31059,y:34787,varname:node_7880,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Subtract,id:6541,x:31279,y:34587,varname:node_6541,prsc:2|A-6230-OUT,B-7880-OUT;n:type:ShaderForge.SFN_Negate,id:1378,x:31442,y:34587,varname:node_1378,prsc:2|IN-6541-OUT;n:type:ShaderForge.SFN_TexCoord,id:5772,x:31291,y:34884,varname:node_5772,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:1241,x:31534,y:34754,varname:node_1241,prsc:2,spu:1,spv:1|UVIN-5772-UVOUT;n:type:ShaderForge.SFN_Panner,id:5532,x:31534,y:34948,varname:node_5532,prsc:2,spu:1,spv:1|UVIN-5772-UVOUT;n:type:ShaderForge.SFN_Add,id:3714,x:31724,y:34734,varname:node_3714,prsc:2|A-8020-OUT,B-1241-UVOUT;n:type:ShaderForge.SFN_Add,id:1651,x:31739,y:34948,varname:node_1651,prsc:2|A-8020-OUT,B-5532-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6249,x:31381,y:34345,ptovrint:False,ptlb:uv_Distort,ptin:_uv_Distort,varname:node_890,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:21805d693c5e92e43af3f4f9d6226ab6,ntxv:0,isnm:False|UVIN-8005-OUT;n:type:ShaderForge.SFN_ComponentMask,id:3862,x:31582,y:34345,varname:node_3862,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-6249-RGB;n:type:ShaderForge.SFN_RemapRange,id:8020,x:31763,y:34345,varname:node_8020,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-3862-OUT;n:type:ShaderForge.SFN_Vector1,id:7094,x:31749,y:34873,varname:node_7094,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:1875,x:31916,y:34722,varname:node_1875,prsc:2|A-3714-OUT,B-7094-OUT;n:type:ShaderForge.SFN_Multiply,id:7869,x:31916,y:34948,varname:node_7869,prsc:2|A-1651-OUT,B-7094-OUT;n:type:ShaderForge.SFN_Set,id:3586,x:31742,y:33187,varname:node_3586,prsc:2|IN-2488-OUT;proporder:2737-2683-6025-7734-8110-5363-4401-1610-537-5245-9842-6249;pass:END;sub:END;*/

Shader "Custom/NewSurfaceShader" {
    Properties {
        _node_7984 ("node_7984", 2D) = "white" {}
        _speed ("speed", Float ) = 0.5
        _circle_thick ("circle_thick", Float ) = 6
        _ccccccc ("ccccccc", Float ) = 0.5
        _node_4422 ("node_4422", 2D) = "white" {}
        _centerGlow ("centerGlow", Float ) = 2
        _node_2677 ("node_2677", Color) = (0,0.2,0.8,1)
        _node_232 ("node_232", Color) = (0,0.7,0.7,1)
        _dotGlow ("dotGlow", Float ) = 0.5
        _Noise ("Noise", 2D) = "white" {}
        _centre_size ("centre_size", Float ) = 3
        _uv_Distort ("uv_Distort", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            uniform sampler2D _node_7984; uniform float4 _node_7984_ST;
            uniform float _speed;
            uniform float _circle_thick;
            uniform float _ccccccc;
            uniform sampler2D _node_4422; uniform float4 _node_4422_ST;
            uniform float _centerGlow;
            uniform float4 _node_2677;
            uniform float4 _node_232;
            uniform float _dotGlow;
            uniform sampler2D _Noise; uniform float4 _Noise_ST;
            uniform float _centre_size;
            uniform sampler2D _uv_Distort; uniform float4 _uv_Distort_ST;
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
                float2 node_3521 = (i.uv0*0.0+1.0);
                float node_2488 = length(node_3521);
                float4 node_3914 = _Time + _TimeEditor;
                float4 node_1872 = _Time + _TimeEditor;
                float2 node_3713 = ((0.5*i.uv0)+node_1872.g*float2(1,1));
                float4 _node_7984_var = tex2D(_node_7984,TRANSFORM_TEX(node_3713, _node_7984));
                float2 node_4316 = node_3521.rg;
                float2 node_4255 = float2(((node_2488*0.01)+(node_3914.g*_speed)),(((_node_7984_var.r*0.0+1.0)*0.1)+((atan2(node_4316.g,node_4316.r)/6.28318530718)+0.5)));
                float4 _node_4422_var = tex2D(_node_4422,TRANSFORM_TEX(node_4255, _node_4422));
                float node_2049 = pow(node_2488,_circle_thick);
                float node_3586 = node_2488;
                float node_5845 = saturate((node_3586*_centerGlow));
                float4 node_4894 = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float4 node_2595 = tex2D(_Noise,TRANSFORM_TEX(i.uv0, _Noise));
                float node_7880 = 0.5;
                float2 node_8005 = ((_centre_size*i.uv0)+(-1*((_centre_size*node_7880)-node_7880)));
                float4 _uv_Distort_var = tex2D(_uv_Distort,TRANSFORM_TEX(node_8005, _uv_Distort));
                float node_4974 = saturate(((pow(((_node_4422_var.r+(_node_4422_var.g*node_2049)+(node_2049*_ccccccc))*node_5845),1.5)+(saturate(((node_4894.r*node_4894.r*node_4894.r)+(node_2595.r*node_2595.r*node_2595.r)))*_uv_Distort_var.b*_dotGlow)+(node_5845*(1.0 - node_5845)))*(1.0 - floor(node_3586))*2.0));
                float3 emissive = lerp(_node_2677.rgb,_node_232.rgb,node_4974);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_4974);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
