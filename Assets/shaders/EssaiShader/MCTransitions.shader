// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:1,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:0,bdst:1,dpts:2,wrdp:True,dith:0,atcv:False,rfrpo:False,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:False,qofs:0,qpre:2,rntp:3,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True,fsmp:False;n:type:ShaderForge.SFN_Final,id:4795,x:34320,y:32697,varname:node_4795,prsc:2|emission-2393-OUT,clip-5317-OUT;n:type:ShaderForge.SFN_Tex2d,id:6074,x:34052,y:32916,ptovrint:False,ptlb:MainTex,ptin:_MainTex,varname:_MainTex,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:0d360c028b0c4324aa29797cd4b26038,ntxv:0,isnm:False|UVIN-3993-OUT;n:type:ShaderForge.SFN_Multiply,id:2393,x:33980,y:33086,varname:node_2393,prsc:2|A-6074-RGB,B-2053-RGB,C-797-RGB,D-4856-OUT;n:type:ShaderForge.SFN_VertexColor,id:2053,x:33748,y:33144,varname:node_2053,prsc:2;n:type:ShaderForge.SFN_Color,id:797,x:33748,y:33305,ptovrint:True,ptlb:Color,ptin:_TintColor,varname:_TintColor,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:0.006325698,c2:0.8602941,c3:0.7896208,c4:1;n:type:ShaderForge.SFN_Slider,id:3979,x:32123,y:32908,ptovrint:False,ptlb:Dissolve,ptin:_Dissolve,varname:_Metallic_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:0,max:1;n:type:ShaderForge.SFN_Append,id:9504,x:32160,y:33157,varname:node_9504,prsc:2|A-3484-OUT,B-1271-OUT;n:type:ShaderForge.SFN_Add,id:47,x:32592,y:33161,varname:node_47,prsc:2|A-4471-OUT,B-8820-UVOUT;n:type:ShaderForge.SFN_Time,id:3529,x:32160,y:33379,varname:node_3529,prsc:2;n:type:ShaderForge.SFN_Multiply,id:4471,x:32363,y:33161,varname:node_4471,prsc:2|A-9504-OUT,B-3529-T;n:type:ShaderForge.SFN_TexCoord,id:8820,x:32363,y:33389,varname:node_8820,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:3484,x:31893,y:33125,ptovrint:False,ptlb:U,ptin:_U,varname:node_568,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:1271,x:31881,y:33329,ptovrint:False,ptlb:V,ptin:_V,varname:node_4882,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_Append,id:7790,x:32160,y:33578,varname:node_7790,prsc:2|A-3896-OUT,B-904-OUT;n:type:ShaderForge.SFN_Time,id:4097,x:32160,y:33800,varname:node_4097,prsc:2;n:type:ShaderForge.SFN_Multiply,id:3195,x:32363,y:33582,varname:node_3195,prsc:2|A-7790-OUT,B-4097-T;n:type:ShaderForge.SFN_TexCoord,id:6846,x:32363,y:33810,varname:node_6846,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_ValueProperty,id:904,x:31842,y:33817,ptovrint:False,ptlb:Vspeed,ptin:_Vspeed,varname:_V_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:0;n:type:ShaderForge.SFN_ValueProperty,id:3896,x:31854,y:33613,ptovrint:False,ptlb:Uspeed,ptin:_Uspeed,varname:_U_copy,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Add,id:6151,x:32592,y:33702,varname:node_6151,prsc:2|A-3195-OUT,B-6846-UVOUT;n:type:ShaderForge.SFN_Tex2d,id:6948,x:33407,y:33490,varname:_node_7692_copy,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-6151-OUT,TEX-1189-TEX;n:type:ShaderForge.SFN_OneMinus,id:1864,x:32517,y:32902,varname:node_1864,prsc:2|IN-3979-OUT;n:type:ShaderForge.SFN_RemapRange,id:936,x:32763,y:32885,varname:node_936,prsc:2,frmn:0,frmx:1,tomn:-0.5,tomx:0.3|IN-1864-OUT;n:type:ShaderForge.SFN_Add,id:9374,x:33061,y:32920,varname:node_9374,prsc:2|A-936-OUT,B-1376-R;n:type:ShaderForge.SFN_Tex2dAsset,id:1189,x:32629,y:33369,ptovrint:False,ptlb:node_2468,ptin:_node_2468,varname:node_2468,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False;n:type:ShaderForge.SFN_Tex2d,id:1376,x:32860,y:33170,varname:node_2467,prsc:2,tex:28c7aad1372ff114b90d330f8a2dd938,ntxv:0,isnm:False|UVIN-47-OUT,TEX-1189-TEX;n:type:ShaderForge.SFN_Add,id:4349,x:33124,y:33184,varname:node_4349,prsc:2|A-936-OUT,B-6948-R;n:type:ShaderForge.SFN_Multiply,id:4012,x:33285,y:33008,varname:node_4012,prsc:2|A-9374-OUT,B-4349-OUT;n:type:ShaderForge.SFN_RemapRange,id:9326,x:33451,y:33100,varname:node_9326,prsc:2,frmn:0,frmx:0.5,tomn:-5,tomx:5|IN-4012-OUT;n:type:ShaderForge.SFN_Clamp01,id:8213,x:33549,y:32941,varname:node_8213,prsc:2|IN-9326-OUT;n:type:ShaderForge.SFN_Append,id:3993,x:33792,y:32941,varname:node_3993,prsc:2|A-8213-OUT,B-3766-OUT;n:type:ShaderForge.SFN_Vector1,id:3766,x:33568,y:33234,varname:node_3766,prsc:2,v1:10;n:type:ShaderForge.SFN_ValueProperty,id:4856,x:33820,y:33624,ptovrint:False,ptlb:Opacity,ptin:_Opacity,varname:node_4856,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:2;n:type:ShaderForge.SFN_Multiply,id:5317,x:33941,y:32690,varname:node_5317,prsc:2|A-8341-OUT,B-6074-R;n:type:ShaderForge.SFN_ValueProperty,id:8341,x:33792,y:32841,ptovrint:False,ptlb:node_8341,ptin:_node_8341,varname:node_8341,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,v1:1;proporder:6074-797-3979-3484-1271-904-3896-1189-4856-8341;pass:END;sub:END;*/

Shader "Shader Forge/TransitionsMCPart" {
    Properties {
        _MainTex ("MainTex", 2D) = "white" {}
        _TintColor ("Color", Color) = (0.006325698,0.8602941,0.7896208,1)
        _Dissolve ("Dissolve", Range(0, 1)) = 0
        _U ("U", Float ) = 0
        _V ("V", Float ) = 0
        _Vspeed ("Vspeed", Float ) = 0
        _Uspeed ("Uspeed", Float ) = 2
        _node_2468 ("node_2468", 2D) = "white" {}
        _Opacity ("Opacity", Float ) = 2
        _node_8341 ("node_8341", Float ) = 1
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
    }
    SubShader {
        Tags {
            "Queue"="AlphaTest"
            "RenderType"="TransparentCutout"
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
            #pragma only_renderers d3d9 d3d11 glcore gles gles3 metal d3d11_9x 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform sampler2D _MainTex; uniform float4 _MainTex_ST;
            uniform float4 _TintColor;
            uniform float _Dissolve;
            uniform float _U;
            uniform float _V;
            uniform float _Vspeed;
            uniform float _Uspeed;
            uniform sampler2D _node_2468; uniform float4 _node_2468_ST;
            uniform float _Opacity;
            uniform float _node_8341;
            struct VertexInput {
                float4 vertex : POSITION;
                float2 texcoord0 : TEXCOORD0;
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
                o.pos = UnityObjectToClipPos( v.vertex );
                return o;
            }
            float4 frag(VertexOutput i) : COLOR {
                float node_936 = ((1.0 - _Dissolve)*0.8+-0.5);
                float4 node_3529 = _Time + _TimeEditor;
                float2 node_47 = ((float2(_U,_V)*node_3529.g)+i.uv0);
                float4 node_2467 = tex2D(_node_2468,TRANSFORM_TEX(node_47, _node_2468));
                float4 node_4097 = _Time + _TimeEditor;
                float2 node_6151 = ((float2(_Uspeed,_Vspeed)*node_4097.g)+i.uv0);
                float4 _node_7692_copy = tex2D(_node_2468,TRANSFORM_TEX(node_6151, _node_2468));
                float2 node_3993 = float2(saturate((((node_936+node_2467.r)*(node_936+_node_7692_copy.r))*20.0+-5.0)),10.0);
                float4 _MainTex_var = tex2D(_MainTex,TRANSFORM_TEX(node_3993, _MainTex));
                clip((_node_8341*_MainTex_var.r) - 0.5);
////// Lighting:
////// Emissive:
                float3 emissive = (_MainTex_var.rgb*i.vertexColor.rgb*_TintColor.rgb*_Opacity);
                float3 finalColor = emissive;
                return fixed4(finalColor,1);
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
