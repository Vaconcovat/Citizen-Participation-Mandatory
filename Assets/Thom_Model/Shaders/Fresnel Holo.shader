// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

// Shader created with Shader Forge v1.26 
// Shader Forge (c) Neat Corporation / Joachim Holmer - http://www.acegikmo.com/shaderforge/
// Note: Manually altering this data may prevent you from opening it in Shader Forge
/*SF_DATA;ver:1.26;sub:START;pass:START;ps:flbk:,iptp:0,cusa:False,bamd:0,lico:0,lgpr:1,limd:0,spmd:1,trmd:0,grmd:0,uamb:True,mssp:True,bkdf:False,hqlp:False,rprd:False,enco:False,rmgx:True,rpth:0,vtps:0,hqsc:True,nrmq:1,nrsp:0,vomd:0,spxs:False,tesm:0,olmd:1,culm:2,bsrc:3,bdst:6,dpts:2,wrdp:False,dith:0,rfrpo:True,rfrpn:Refraction,coma:15,ufog:True,aust:True,igpj:True,qofs:0,qpre:3,rntp:2,fgom:False,fgoc:True,fgod:False,fgor:False,fgmd:0,fgcr:0,fgcg:0,fgcb:0,fgca:1,fgde:0.01,fgrn:0,fgrf:300,stcl:False,stva:128,stmr:255,stmw:255,stcp:6,stps:0,stfa:0,stfz:0,ofsf:0,ofsu:0,f2p0:False,fnsp:True,fnfb:True;n:type:ShaderForge.SFN_Final,id:4795,x:33208,y:32693,varname:node_4795,prsc:2|normal-9379-RGB,emission-3782-OUT;n:type:ShaderForge.SFN_Fresnel,id:1760,x:32114,y:32729,varname:node_1760,prsc:2|EXP-2595-OUT;n:type:ShaderForge.SFN_Slider,id:2595,x:31777,y:32820,ptovrint:False,ptlb:Rim 1 Power,ptin:_Rim1Power,varname:node_2595,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.136755,max:10;n:type:ShaderForge.SFN_Color,id:1173,x:31904,y:32609,ptovrint:False,ptlb:Rim 1 Volour,ptin:_Rim1Volour,varname:node_1173,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0,c3:0,c4:1;n:type:ShaderForge.SFN_Multiply,id:5359,x:32276,y:32647,varname:node_5359,prsc:2|A-1173-RGB,B-1760-OUT;n:type:ShaderForge.SFN_Fresnel,id:5859,x:32114,y:32896,varname:node_5859,prsc:2|EXP-4606-OUT;n:type:ShaderForge.SFN_Color,id:573,x:31897,y:33135,ptovrint:False,ptlb:Rim 2 Colour,ptin:_Rim2Colour,varname:node_573,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,c1:1,c2:0.724138,c3:0,c4:1;n:type:ShaderForge.SFN_Slider,id:4606,x:31777,y:32940,ptovrint:False,ptlb:Rim 2 Power ,ptin:_Rim2Power,varname:node_4606,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,min:0,cur:2.5641,max:10;n:type:ShaderForge.SFN_Multiply,id:5305,x:32277,y:32987,varname:node_5305,prsc:2|A-5859-OUT,B-573-RGB;n:type:ShaderForge.SFN_Add,id:3782,x:32607,y:32685,varname:node_3782,prsc:2|A-5359-OUT,B-5305-OUT;n:type:ShaderForge.SFN_Tex2d,id:9379,x:33001,y:32606,ptovrint:False,ptlb:node_9379,ptin:_node_9379,varname:node_9379,prsc:2,glob:False,taghide:False,taghdr:False,tagprd:False,tagnsco:False,tagnrm:False,tex:bb70d89c0504be04d8392869d2d0c1ed,ntxv:3,isnm:True;proporder:2595-1173-573-4606-9379;pass:END;sub:END;*/

Shader "Shader Forge/Fresnel Holo" {
    Properties {
        _Rim1Power ("Rim 1 Power", Range(0, 10)) = 2.136755
        _Rim1Volour ("Rim 1 Volour", Color) = (1,0,0,1)
        _Rim2Colour ("Rim 2 Colour", Color) = (1,0.724138,0,1)
        _Rim2Power ("Rim 2 Power ", Range(0, 10)) = 2.5641
        _node_9379 ("node_9379", 2D) = "bump" {}
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
            Blend SrcAlpha OneMinusSrcColor
            Cull Off
            ZWrite Off
            
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #define UNITY_PASS_FORWARDBASE
            #include "UnityCG.cginc"
            #pragma multi_compile_fwdbase
            #pragma multi_compile_fog
            #pragma exclude_renderers gles3 metal d3d11_9x xbox360 xboxone ps3 ps4 psp2 
            #pragma target 3.0
            uniform float _Rim1Power;
            uniform float4 _Rim1Volour;
            uniform float4 _Rim2Colour;
            uniform float _Rim2Power;
            uniform sampler2D _node_9379; uniform float4 _node_9379_ST;
            struct VertexInput {
                float4 vertex : POSITION;
                float3 normal : NORMAL;
                float4 tangent : TANGENT;
                float2 texcoord0 : TEXCOORD0;
            };
            struct VertexOutput {
                float4 pos : SV_POSITION;
                float2 uv0 : TEXCOORD0;
                float4 posWorld : TEXCOORD1;
                float3 normalDir : TEXCOORD2;
                float3 tangentDir : TEXCOORD3;
                float3 bitangentDir : TEXCOORD4;
                UNITY_FOG_COORDS(5)
            };
            VertexOutput vert (VertexInput v) {
                VertexOutput o = (VertexOutput)0;
                o.uv0 = v.texcoord0;
                o.normalDir = UnityObjectToWorldNormal(v.normal);
                o.tangentDir = normalize( mul( unity_ObjectToWorld, float4( v.tangent.xyz, 0.0 ) ).xyz );
                o.bitangentDir = normalize(cross(o.normalDir, o.tangentDir) * v.tangent.w);
                o.posWorld = mul(unity_ObjectToWorld, v.vertex);
                o.pos = mul(UNITY_MATRIX_MVP, v.vertex );
                UNITY_TRANSFER_FOG(o,o.pos);
                return o;
            }
            float4 frag(VertexOutput i, float facing : VFACE) : COLOR {
                float isFrontFace = ( facing >= 0 ? 1 : 0 );
                float faceSign = ( facing >= 0 ? 1 : -1 );
                i.normalDir = normalize(i.normalDir);
                i.normalDir *= faceSign;
                float3x3 tangentTransform = float3x3( i.tangentDir, i.bitangentDir, i.normalDir);
                float3 viewDirection = normalize(_WorldSpaceCameraPos.xyz - i.posWorld.xyz);
                float3 _node_9379_var = UnpackNormal(tex2D(_node_9379,TRANSFORM_TEX(i.uv0, _node_9379)));
                float3 normalLocal = _node_9379_var.rgb;
                float3 normalDirection = normalize(mul( normalLocal, tangentTransform )); // Perturbed normals
////// Lighting:
////// Emissive:
                float3 emissive = ((_Rim1Volour.rgb*pow(1.0-max(0,dot(normalDirection, viewDirection)),_Rim1Power))+(pow(1.0-max(0,dot(normalDirection, viewDirection)),_Rim2Power)*_Rim2Colour.rgb));
                float3 finalColor = emissive;
                fixed4 finalRGBA = fixed4(finalColor,1);
                UNITY_APPLY_FOG_COLOR(i.fogCoord, finalRGBA, fixed4(0,0,0,1));
                return finalRGBA;
            }
            ENDCG
        }
    }
    CustomEditor "ShaderForgeMaterialInspector"
}
