Shader "Unlit/Story"
{
    Properties
    {
        _image_01( "Texture1", 2D ) = "white" {}
        _image_02( "Texture2", 2D ) = "white" {}
        _image_03( "Texture3", 2D ) = "white" {}
        _image_04 ("Texture4", 2D) = "white" {}

        _speed( "speed",Range( -1,1 ) ) = 0.2
        _is_change( "_is_change", Float ) = 0.0
            //_idx("idx",Range( -1,3 ) ) = 3.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
#pragma vertex vert
#pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                
                float4 vertex : SV_POSITION;
            };

            sampler2D _image_01;
            sampler2D _image_02;
            sampler2D _image_03;
            sampler2D _image_04;
            float4 _MainTex_ST;
            float _speed;
            float _is_change;
            float _idx = 3.0f;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos( v.vertex );
                o.uv = v.uv;
                return o;
            }

            fixed4 frag( v2f i ) : SV_Target
            {
                fixed4 col = tex2D( _image_01, i.uv );
            /*float idx = 0.0f;
                if ( idx > 0.0f ) {

                } else {
                    col = tex2D( _image_01, i.uv );
                }*/
                // sample the texture
                
            float curPos = _Time.y * _speed;
            if ( curPos < i.uv.x  ) {
                col = tex2D( _image_02, i.uv );
               // col = tex2D( _image_02, i.uv );
               /* if ( idx == 1.0 ) {
                    col = tex2D( _image_02, i.uv );
                } else if( idx == 2.0 ){
                    col = tex2D( _image_03, i.uv );
                } else if ( idx == 3.0 ) {
                    col = tex2D( _image_04, i.uv );
                }*/
            }
            /*if ( curPos >= 1 ) {
                _idx++;
            }*/
            return col;
            }
            ENDCG
        }
    }
}