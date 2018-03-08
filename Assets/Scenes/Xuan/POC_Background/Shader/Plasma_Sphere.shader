// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:419,x:33568,y:32873,varname:node_419,prsc:2|emission-2178-OUT,alpha-3925-OUT;n:type:ShaderForge.SFN_Tex2d,id:7984,x:31714,y:32527,ptovrint:False,ptlb:node_7984,ptin:_node_7984,varname:node_7984,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:f1b5df310926b284cadcaeda17ed2676,ntxv:0,isnm:False|UVIN-4656-UVOUT;n:type:ShaderForge.SFN_RemapRange,id:2814,x:31964,y:32509,varname:node_2814,prsc:2,frmn:0,frmx:1,tomn:1,tomx:1|IN-7984-R;n:type:ShaderForge.SFN_TexCoord,id:592,x:31041,y:32647,varname:node_592,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Vector1,id:2190,x:31054,y:32579,varname:node_2190,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:5559,x:31286,y:32612,varname:node_5559,prsc:2|A-2190-OUT,B-592-UVOUT;n:type:ShaderForge.SFN_Panner,id:4656,x:31477,y:32602,varname:node_4656,prsc:2,spu:1,spv:1|UVIN-5559-OUT;n:type:ShaderForge.SFN_Multiply,id:8922,x:32181,y:32527,varname:node_8922,prsc:2|A-2814-OUT,B-5534-OUT;n:type:ShaderForge.SFN_Vector1,id:5534,x:31964,y:32686,varname:node_5534,prsc:2,v1:0.1;n:type:ShaderForge.SFN_TexCoord,id:8426,x:31100,y:32924,varname:node_8426,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_RemapRange,id:5919,x:31322,y:32896,varname:node_5919,prsc:2,frmn:0,frmx:1,tomn:1,tomx:1|IN-8426-UVOUT;n:type:ShaderForge.SFN_ComponentMask,id:6761,x:31540,y:32800,varname:node_6761,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-5919-OUT;n:type:ShaderForge.SFN_Length,id:7834,x:31540,y:33032,varname:node_7834,prsc:2|IN-5919-OUT;n:type:ShaderForge.SFN_ArcTan2,id:3912,x:31753,y:32800,varname:node_3912,prsc:2,attp:2|A-6761-G,B-6761-R;n:type:ShaderForge.SFN_Add,id:9032,x:32453,y:32593,varname:node_9032,prsc:2|A-8922-OUT,B-3912-OUT;n:type:ShaderForge.SFN_Append,id:2145,x:32362,y:33016,varname:node_2145,prsc:2|A-9791-OUT,B-9032-OUT;n:type:ShaderForge.SFN_Multiply,id:1663,x:31891,y:32986,varname:node_1663,prsc:2|A-7834-OUT,B-7069-OUT;n:type:ShaderForge.SFN_Add,id:9791,x:32094,y:33098,varname:node_9791,prsc:2|A-1663-OUT,B-7949-OUT;n:type:ShaderForge.SFN_Vector1,id:7069,x:31822,y:33121,varname:node_7069,prsc:2,v1:0.01;n:type:ShaderForge.SFN_Time,id:3585,x:31264,y:33212,varname:node_3585,prsc:2;n:type:ShaderForge.SFN_Multiply,id:7949,x:31465,y:33259,varname:node_7949,prsc:2|A-3585-T,B-5050-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5050,x:31264,y:33359,ptovrint:False,ptlb:speed,ptin:_speed,varname:node_5050,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Power,id:2525,x:31729,y:33328,varname:node_2525,prsc:2|VAL-7834-OUT,EXP-507-OUT;n:type:ShaderForge.SFN_ValueProperty,id:507,x:31465,y:33462,ptovrint:False,ptlb:circle_thick,ptin:_circle_thick,varname:node_507,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:6;n:type:ShaderForge.SFN_Multiply,id:6118,x:31957,y:33431,varname:node_6118,prsc:2|A-2525-OUT,B-5132-OUT;n:type:ShaderForge.SFN_ValueProperty,id:5132,x:31729,y:33498,ptovrint:False,ptlb:ccccccc,ptin:_ccccccc,varname:node_5132,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Tex2d,id:4422,x:32564,y:33016,ptovrint:False,ptlb:node_4422,ptin:_node_4422,varname:node_4422,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:45906d38608b22c4095d6ab84df1df41,ntxv:0,isnm:False|UVIN-2145-OUT;n:type:ShaderForge.SFN_Multiply,id:7579,x:32019,y:33304,varname:node_7579,prsc:2|A-4422-G,B-2525-OUT;n:type:ShaderForge.SFN_Add,id:4682,x:32227,y:33304,varname:node_4682,prsc:2|A-4422-R,B-7579-OUT,C-6118-OUT;n:type:ShaderForge.SFN_Multiply,id:386,x:32436,y:33304,varname:node_386,prsc:2|A-4682-OUT,B-4541-OUT;n:type:ShaderForge.SFN_Get,id:1559,x:31596,y:33647,varname:node_1559,prsc:2|IN-6287-OUT;n:type:ShaderForge.SFN_Get,id:8744,x:31596,y:33800,varname:node_8744,prsc:2|IN-6287-OUT;n:type:ShaderForge.SFN_Floor,id:9368,x:31843,y:33647,varname:node_9368,prsc:2|IN-1559-OUT;n:type:ShaderForge.SFN_OneMinus,id:3545,x:32049,y:33660,varname:node_3545,prsc:2|IN-9368-OUT;n:type:ShaderForge.SFN_Multiply,id:6998,x:31824,y:33800,varname:node_6998,prsc:2|A-8744-OUT,B-6388-OUT;n:type:ShaderForge.SFN_Clamp01,id:4541,x:32049,y:33800,varname:node_4541,prsc:2|IN-6998-OUT;n:type:ShaderForge.SFN_ValueProperty,id:6388,x:31596,y:33870,ptovrint:False,ptlb:centerGlow,ptin:_centerGlow,varname:node_6388,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_OneMinus,id:8735,x:32274,y:33857,varname:node_8735,prsc:2|IN-4541-OUT;n:type:ShaderForge.SFN_Multiply,id:557,x:32482,y:33796,varname:node_557,prsc:2|A-4541-OUT,B-8735-OUT;n:type:ShaderForge.SFN_Power,id:5989,x:32630,y:33304,varname:node_5989,prsc:2|VAL-386-OUT,EXP-3505-OUT;n:type:ShaderForge.SFN_Vector1,id:3505,x:32535,y:33490,varname:node_3505,prsc:2,v1:1.5;n:type:ShaderForge.SFN_Add,id:4623,x:32860,y:33304,varname:node_4623,prsc:2|A-5989-OUT,B-9717-OUT,C-557-OUT;n:type:ShaderForge.SFN_Multiply,id:9717,x:33139,y:34198,varname:node_9717,prsc:2|A-8112-OUT,B-890-B,C-1266-OUT;n:type:ShaderForge.SFN_Multiply,id:455,x:33084,y:33304,varname:node_455,prsc:2|A-4623-OUT,B-3545-OUT,C-2541-OUT;n:type:ShaderForge.SFN_Vector1,id:2541,x:32970,y:33523,varname:node_2541,prsc:2,v1:2;n:type:ShaderForge.SFN_Clamp01,id:3925,x:33101,y:33041,varname:node_3925,prsc:2|IN-455-OUT;n:type:ShaderForge.SFN_Lerp,id:2178,x:33202,y:32831,varname:node_2178,prsc:2|A-2677-RGB,B-232-RGB,T-3925-OUT;n:type:ShaderForge.SFN_Color,id:2677,x:32877,y:32664,ptovrint:False,ptlb:node_2677,ptin:_node_2677,varname:node_2677,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.2,c3:0.8,c4:1;n:type:ShaderForge.SFN_Color,id:232,x:32866,y:32842,ptovrint:False,ptlb:node_232,ptin:_node_232,varname:node_232,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0,c2:0.7,c3:0.7,c4:1;n:type:ShaderForge.SFN_Clamp01,id:8112,x:32901,y:34198,varname:node_8112,prsc:2|IN-9195-OUT;n:type:ShaderForge.SFN_ValueProperty,id:1266,x:32901,y:34404,ptovrint:False,ptlb:dotGlow,ptin:_dotGlow,varname:node_1266,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0.5;n:type:ShaderForge.SFN_Add,id:9195,x:32724,y:34198,varname:node_9195,prsc:2|A-4050-OUT,B-1567-OUT;n:type:ShaderForge.SFN_Multiply,id:4050,x:32521,y:34111,varname:node_4050,prsc:2|A-4894-R,B-4894-R,C-4894-R;n:type:ShaderForge.SFN_Multiply,id:1567,x:32521,y:34301,varname:node_1567,prsc:2|A-2595-R,B-2595-R,C-2595-R;n:type:ShaderForge.SFN_Tex2d,id:4894,x:32308,y:34111,varname:node_4894,prsc:2,tex:3ecffec63fe09b64889a09895349d47f,ntxv:0,isnm:False|UVIN-7776-OUT,TEX-9948-TEX;n:type:ShaderForge.SFN_Tex2d,id:2595,x:32308,y:34323,varname:node_2595,prsc:2,tex:3ecffec63fe09b64889a09895349d47f,ntxv:0,isnm:False|UVIN-9027-OUT,TEX-9948-TEX;n:type:ShaderForge.SFN_Tex2dAsset,id:9948,x:32099,y:34216,ptovrint:False,ptlb:Noise,ptin:_Noise,varname:node_9948,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:3ecffec63fe09b64889a09895349d47f,ntxv:0,isnm:False;n:type:ShaderForge.SFN_TexCoord,id:2436,x:30901,y:34110,varname:node_2436,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Multiply,id:3801,x:30889,y:34281,varname:node_3801,prsc:2|A-1232-OUT,B-2436-UVOUT;n:type:ShaderForge.SFN_ValueProperty,id:1232,x:30786,y:34462,ptovrint:False,ptlb:centre_size,ptin:_centre_size,varname:node_1232,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:3;n:type:ShaderForge.SFN_Add,id:4209,x:31118,y:34281,varname:node_4209,prsc:2|A-3801-OUT,B-5069-OUT;n:type:ShaderForge.SFN_Multiply,id:2539,x:31031,y:34523,varname:node_2539,prsc:2|A-1232-OUT,B-9796-OUT;n:type:ShaderForge.SFN_Vector1,id:9796,x:30995,y:34723,varname:node_9796,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Subtract,id:8604,x:31215,y:34523,varname:node_8604,prsc:2|A-2539-OUT,B-9796-OUT;n:type:ShaderForge.SFN_Negate,id:5069,x:31378,y:34523,varname:node_5069,prsc:2|IN-8604-OUT;n:type:ShaderForge.SFN_TexCoord,id:1946,x:31227,y:34820,varname:node_1946,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Panner,id:5499,x:31470,y:34690,varname:node_5499,prsc:2,spu:1,spv:1|UVIN-1946-UVOUT;n:type:ShaderForge.SFN_Panner,id:5706,x:31470,y:34884,varname:node_5706,prsc:2,spu:1,spv:1|UVIN-1946-UVOUT;n:type:ShaderForge.SFN_Add,id:3383,x:31660,y:34670,varname:node_3383,prsc:2|A-7369-OUT,B-5499-UVOUT;n:type:ShaderForge.SFN_Add,id:9890,x:31675,y:34884,varname:node_9890,prsc:2|A-7369-OUT,B-5706-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:890,x:31317,y:34281,ptovrint:False,ptlb:uv_Distort,ptin:_uv_Distort,varname:node_890,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:21805d693c5e92e43af3f4f9d6226ab6,ntxv:0,isnm:False|UVIN-4209-OUT;n:type:ShaderForge.SFN_ComponentMask,id:7646,x:31518,y:34281,varname:node_7646,prsc:2,cc1:0,cc2:1,cc3:-1,cc4:-1|IN-890-RGB;n:type:ShaderForge.SFN_RemapRange,id:7369,x:31699,y:34281,varname:node_7369,prsc:2,frmn:0,frmx:1,tomn:-1,tomx:1|IN-7646-OUT;n:type:ShaderForge.SFN_Vector1,id:7275,x:31685,y:34809,varname:node_7275,prsc:2,v1:0.5;n:type:ShaderForge.SFN_Multiply,id:7776,x:31852,y:34658,varname:node_7776,prsc:2|A-3383-OUT,B-7275-OUT;n:type:ShaderForge.SFN_Multiply,id:9027,x:31852,y:34884,varname:node_9027,prsc:2|A-9890-OUT,B-7275-OUT;n:type:ShaderForge.SFN_Set,id:6287,x:31678,y:33123,varname:cir,prsc:2|IN-7834-OUT;proporder:7984-5050-507-5132-4422-6388-232-2677-9948-1232-890-1266;pass:END;sub:END;*/

Shader "Custom/Plasma_Sphere" {
    Properties {
        _node_7984 ("node_7984", 2D) = "white" {}
        _speed ("speed", Float ) = 0.5
        _circle_thick ("circle_thick", Float ) = 6
        _ccccccc ("ccccccc", Float ) = 0.5
        _node_4422 ("node_4422", 2D) = "white" {}
        _centerGlow ("centerGlow", Float ) = 2
        _node_232 ("node_232", Color) = (0,0.7,0.7,1)
        _node_2677 ("node_2677", Color) = (0,0.2,0.8,1)
        _Noise ("Noise", 2D) = "white" {}
        _centre_size ("centre_size", Float ) = 3
        _uv_Distort ("uv_Distort", 2D) = "white" {}
        _dotGlow ("dotGlow", Float ) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
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
                float2 node_5919 = (i.uv0*0.0+1.0);
                float node_7834 = length(node_5919);
                float4 node_3585 = _Time + _TimeEditor;
                float4 node_2692 = _Time + _TimeEditor;
                float2 node_4656 = ((0.5*i.uv0)+node_2692.g*float2(1,1));
                float4 _node_7984_var = tex2D(_node_7984,TRANSFORM_TEX(node_4656, _node_7984));
                float2 node_6761 = node_5919.rg;
                float2 node_2145 = float2(((node_7834*0.01)+(node_3585.g*_speed)),(((_node_7984_var.r*0.0+1.0)*0.1)+((atan2(node_6761.g,node_6761.r)/6.28318530718)+0.5)));
                float4 _node_4422_var = tex2D(_node_4422,TRANSFORM_TEX(node_2145, _node_4422));
                float node_2525 = pow(node_7834,_circle_thick);
                float cir = node_7834;
                float node_4541 = saturate((cir*_centerGlow));
                float node_9796 = 0.5;
                float2 node_4209 = ((_centre_size*i.uv0)+(-1*((_centre_size*node_9796)-node_9796)));
                float4 _uv_Distort_var = tex2D(_uv_Distort,TRANSFORM_TEX(node_4209, _uv_Distort));
                float2 node_7369 = (_uv_Distort_var.rgb.rg*2.0+-1.0);
                float node_7275 = 0.5;
                float2 node_7776 = ((node_7369+(i.uv0+node_2692.g*float2(1,1)))*node_7275);
                float4 node_4894 = tex2D(_Noise,TRANSFORM_TEX(node_7776, _Noise));
                float2 node_9027 = ((node_7369+(i.uv0+node_2692.g*float2(1,1)))*node_7275);
                float4 node_2595 = tex2D(_Noise,TRANSFORM_TEX(node_9027, _Noise));
                float node_3925 = saturate(((pow(((_node_4422_var.r+(_node_4422_var.g*node_2525)+(node_2525*_ccccccc))*node_4541),1.5)+(saturate(((node_4894.r*node_4894.r*node_4894.r)+(node_2595.r*node_2595.r*node_2595.r)))*_uv_Distort_var.b*_dotGlow)+(node_4541*(1.0 - node_4541)))*(1.0 - floor(cir))*2.0));
                float3 emissive = lerp(_node_2677.rgb,_node_232.rgb,node_3925);
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,node_3925);
                UNITY_APPLY_FOG(i.fogCoord, finalRGBA);
                return finalRGBA;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
