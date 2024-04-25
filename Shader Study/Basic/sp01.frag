#ifdef GL_ES
precision mediump float;
#endif

// 유니폼은 glsl에서 이런식으로 초기화 할 수 없다.
//uniform vec2 iResolution = vec2(0,0);

vec3 rgb(float r,float g,float b){
	return vec3(r/255.,g/255.,b/255.);
}

/**
* Draw a circle at vec2 `pos` with radius `rad` and
* color `color`.
*/
vec4 circle(vec2 uv,vec2 pos,float rad,vec3 color){
	float d=length(pos-uv)-rad;
	float t=clamp(d,0.,1.);
	return vec4(color,1.-t);
}

void mainImage(out vec4 fragColor,in vec2 fragCoord){
	vec2 iResolution=vec2(1100,1300);// 이렇게 상수로 집어넣으면 된다.
	
	vec2 uv=fragCoord.xy;
	vec2 center=iResolution.xy*.5;
	float radius=.25*iResolution.y;
	
	// Background layer
	vec4 layer1=vec4(rgb(210.,222.,228.),1.);
	
	// Circle
	vec3 red=rgb(225.,95.,60.);
	vec4 layer2=circle(uv,center,radius,red);
	
	// Blend the two
	fragColor=mix(layer1,layer2,layer2.a);
}

void main(){
	gl_FragColor=vec4(0.,0.,0.,1.);
	mainImage(gl_FragColor,gl_FragCoord.xy);
}