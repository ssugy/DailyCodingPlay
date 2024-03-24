//Sci-fi radar based on the work of gmunk for Oblivion
//http://work.gmunk.com/OBLIVION-GFX

#ifdef GL_ES
precision mediump float;
#endif

uniform float3      iResolution;           // viewport resolution (in pixels)
uniform float     u_time ;                 // shader playback time (in seconds)
uniform float     iTimeDelta;            // render time (in seconds)
uniform float     iFrameRate;            // shader frame rate
uniform int       iFrame;                // shader playback frame
uniform float     iChannelTime[4];       // channel playback time (in seconds)
uniform float3      iChannelResolution[4]; // channel resolution (in pixels)
uniform float4      iMouse;                // mouse pixel coords. xy: current (if MLB down), zw: click
//uniform samplerXX iChannel0..3;          // input channel. XX = 2D/Cube
uniform float4      iDate;                 // (year, month, day, time in seconds)


#define SMOOTH(r,R) (1.0-smoothstep(R-1.0,R+1.0, r))
#define RANGE(a,b,x) ( step(a,x)*(1.0-step(b,x)) )
#define RS(a,b,x) ( smoothstep(a-1.0,a+1.0,x)*(1.0-smoothstep(b-1.0,b+1.0,x)) )
#define M_PI 3.1415926535897932384626433832795

#define blue1 float3(0.74,0.95,1.00)
#define blue2 float3(0.87,0.98,1.00)
#define blue3 float3(0.35,0.76,0.83)
#define blue4 float3(0.953,0.969,0.89)
#define red   float3(1.00,0.38,0.227)

#define MOV(a,b,c,d,t) (float2(a*cos(t)+b*cos(0.1*(t)), c*sin(t)+d*cos(0.1*(t))))

float movingLine(float2 uv, float2 center, float radius)
{
    //angle of the line
    float theta0 = 90.0 * u_time;
    float2 d = uv - center;
    float r = sqrt( dot( d, d ) );
    if(r<radius)
    {
        //compute the distance to the line theta=theta0
        float2 p = radius*float2(cos(theta0*M_PI/180.0),
                            -sin(theta0*M_PI/180.0));
        float l = length( d - p*clamp( dot(d,p)/dot(p,p), 0.0, 1.0) );
    	d = normalize(d);
        //compute gradient based on angle difference to theta0
   	 	float theta = fmod(180.0*atan2(d.y,d.x)/M_PI+theta0,360.0);
        float gradient = clamp(1.0-theta/90.0,0.0,1.0);
        return SMOOTH(l,1.0)+0.5*gradient;
    }
    else return 0.0;
}

float circle(float2 uv, float2 center, float radius, float width)
{
    float r = length(uv - center);
    return SMOOTH(r-width/2.0,radius)-SMOOTH(r+width/2.0,radius);
}

float circle2(float2 uv, float2 center, float radius, float width, float opening)
{
    float2 d = uv - center;
    float r = sqrt( dot( d, d ) );
    d = normalize(d);
    if( abs(d.y) > opening )
	    return SMOOTH(r-width/2.0,radius)-SMOOTH(r+width/2.0,radius);
    else
        return 0.0;
}
float circle3(float2 uv, float2 center, float radius, float width)
{
    float2 d = uv - center;
    float r = sqrt( dot( d, d ) );
    d = normalize(d);
    float theta = 180.0*(atan2(d.y,d.x)/M_PI);
    return smoothstep(2.0, 2.1, abs(fmod(theta+2.0,45.0)-2.0)) *
        lerp( 0.5, 1.0, step(45.0, abs(fmod(theta, 180.0)-90.0)) ) *
        (SMOOTH(r-width/2.0,radius)-SMOOTH(r+width/2.0,radius));
}

float triangles(float2 uv, float2 center, float radius)
{
    float2 d = uv - center;
    return RS(-8.0, 0.0, d.x-radius) * (1.0-smoothstep( 7.0+d.x-radius,9.0+d.x-radius, abs(d.y)))
         + RS( 0.0, 8.0, d.x+radius) * (1.0-smoothstep( 7.0-d.x-radius,9.0-d.x-radius, abs(d.y)))
         + RS(-8.0, 0.0, d.y-radius) * (1.0-smoothstep( 7.0+d.y-radius,9.0+d.y-radius, abs(d.x)))
         + RS( 0.0, 8.0, d.y+radius) * (1.0-smoothstep( 7.0-d.y-radius,9.0-d.y-radius, abs(d.x)));
}

float _cross(float2 uv, float2 center, float radius)
{
    float2 d = uv - center;
    int x = int(d.x);
    int y = int(d.y);
    float r = sqrt( dot( d, d ) );
    if( (r<radius) && ( (x==y) || (x==-y) ) )
        return 1.0;
    else return 0.0;
}
float dots(float2 uv, float2 center, float radius)
{
    float2 d = uv - center;
    float r = sqrt( dot( d, d ) );
    if( r <= 2.5 )
        return 1.0;
    if( ( r<= radius) && ( (abs(d.y+0.5)<=1.0) && ( fmod(d.x+1.0, 50.0) < 2.0 ) ) )
        return 1.0;
    else if ( (abs(d.y+0.5)<=1.0) && ( r >= 50.0 ) && ( r < 115.0 ) )
        return 0.5;
    else
	    return 0.0;
}
float bip1(float2 uv, float2 center)
{
    return SMOOTH(length(uv - center),3.0);
}
float bip2(float2 uv, float2 center)
{
    float r = length(uv - center);
    float R = 8.0+fmod(87.0*u_time, 80.0);
    return (0.5-0.5*cos(30.0*u_time)) * SMOOTH(r,5.0)
        + SMOOTH(6.0,r)-SMOOTH(8.0,r)
        + smoothstep(max(8.0,R-20.0),R,r)-SMOOTH(R,r);
}
void mainImage( out float4 fragColor, in float2 fragCoord )
{
    float2 iResolution = float2(1100,1200);
    float3 finalColor;
	float2 uv = fragCoord.xy;
    //center of the image
    float2 c = iResolution.xy/2.0;
    finalColor = float(0.3*_cross(uv, c, 240.0) );
    finalColor += ( circle(uv, c, 100.0, 1.0)
                  + circle(uv, c, 165.0, 1.0) ) * blue1;
    finalColor += (circle(uv, c, 240.0, 2.0) );//+ dots(uv,c,240.0)) * blue4;
    finalColor += circle3(uv, c, 313.0, 4.0) * blue1;
    finalColor += triangles(uv, c, 315.0 + 30.0*sin(u_time)) * blue2;
    finalColor += movingLine(uv, c, 240.0) * blue3;
    finalColor += circle(uv, c, 10.0, 1.0) * blue3;
    finalColor += 0.7 * circle2(uv, c, 262.0, 1.0, 0.5+0.2*cos(u_time)) * blue3;
    if( length(uv-c) < 240.0 )
    {
        //animate some bips with random movements
    	float2 p = 130.0*MOV(1.3,1.0,1.0,1.4,3.0+0.1*u_time);
   		finalColor += bip1(uv, c+p) * float3(1,1,1);
        p = 130.0*MOV(0.9,-1.1,1.7,0.8,-2.0+sin(0.1*u_time)+0.15*u_time);
        finalColor += bip1(uv, c+p) * float3(1,1,1);
        p = 50.0*MOV(1.54,1.7,1.37,1.8,sin(0.1*u_time+7.0)+0.2*u_time);
        finalColor += bip2(uv,c+p) * red;
    }

    fragColor = float4( finalColor, 1.0 );
}

// Collect input from the vertex shader. 
// The COLOR semantic must match the semantic in the vertex shader code.
struct PixelShaderInput
{
    float4 pos : SV_Position;
    float4 color : COLOR; // Color for the pixel
};

// Set the pixel color value for the renter target. 
float4 main(PixelShaderInput input) : SV_Target
{
    //mainImage(input.color, input.pos.xy);
    //return input.color;
    return float4(1,1,1,1);
}