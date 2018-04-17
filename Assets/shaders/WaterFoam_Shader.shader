// Shader created with Shader Forge v1.37 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.37;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,cgin:,lico:1,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,imps:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:0,bsrc:3,bdst:7,dpts:2,wrdp:False,dith:0,atcv:False,rfrpo:True,rfrpn:Refraction,coma:15,ufog:False,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:False,fgod:False,fgor:False,fgmd:0,fgcr:0.5,fgcg:0.5,fgcb:0.5,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:False,fnfb:False,fsmp:False;n:type:ShaderForge.SFN_Final,id:3138,x:32719,y:32712,varname:node_3138,prsc:2|emission-7241-RGB,alpha-7907-OUT;n:type:ShaderForge.SFN_Color,id:7241,x:32116,y:32495,ptovrint:False,ptlb:Color,ptin:_Color,varname:node_7241,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:1,c3:1,c4:1;n:type:ShaderForge.SFN_Multiply,id:5464,x:32303,y:32829,varname:node_5464,prsc:2|A-1360-R,B-1997-A;n:type:ShaderForge.SFN_Tex2d,id:1360,x:32051,y:32734,ptovrint:False,ptlb:node_1360,ptin:_node_1360,varname:node_1360,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:4869f99b56204184fa2492daa1c843b8,ntxv:0,isnm:False|UVIN-6244-UVOUT;n:type:ShaderForge.SFN_VertexColor,id:1997,x:32076,y:33024,varname:node_1997,prsc:2;n:type:ShaderForge.SFN_Panner,id:6244,x:31837,y:32783,varname:node_6244,prsc:2,spu:0,spv:0.07|UVIN-5139-OUT;n:type:ShaderForge.SFN_TexCoord,id:7489,x:31316,y:32876,varname:node_7489,prsc:2,uv:0,uaff:False;n:type:ShaderForge.SFN_Subtract,id:7907,x:32515,y:32922,varname:node_7907,prsc:2|A-5464-OUT,B-6562-OUT;n:type:ShaderForge.SFN_Vector1,id:6562,x:32300,y:33143,varname:node_6562,prsc:2,v1:0.2;n:type:ShaderForge.SFN_Add,id:8324,x:31520,y:33020,varname:node_8324,prsc:2|A-7489-V,B-5192-OUT;n:type:ShaderForge.SFN_Append,id:5139,x:31655,y:32839,varname:node_5139,prsc:2|A-7489-U,B-8324-OUT;n:type:ShaderForge.SFN_Vector1,id:5192,x:31290,y:33098,varname:node_5192,prsc:2,v1:0.5;proporder:7241-1360;pass:END;sub:END;*/

Shader "Shader Forge/WaterFoam_Shader" {
    Properties {
        _Color ("Color", Color) = (1,1,1,1)
        _node_1360 ("node_1360", 2D) = "white" {}
        [HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5
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
            Blend SrcAlpha OneMinusSrcAlpha
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma only_renderers d3d9 d3d11 glcore gles 
            #pragma target 3.0
            uniform float4 _TimeEditor;
            uniform float4 _Color;
            uniform sampler2D _node_1360; uniform float4 _node_1360_ST;
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
////// Lighting:
////// Emissive:
                float3 emissive = _Color.rgb;
                float3 finalColor = emissive;
                float4 node_7726 = _Time + _TimeEditor;
                float2 node_6244 = (float2(i.uv0.r,(i.uv0.g+0.5))+node_7726.g*float2(0,0.07));
                float4 _node_1360_var = tex2D(_node_1360,TRANSFORM_TEX(node_6244, _node_1360));
                return fixed4(finalColor,((_node_1360_var.r*i.vertexColor.a)-0.2));
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
    CustomEditor "ShaderForgeMaterialInspector"
}
