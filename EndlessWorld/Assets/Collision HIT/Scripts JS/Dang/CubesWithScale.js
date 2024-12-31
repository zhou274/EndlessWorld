#pragma strict

private var StartScale:Vector3;
var EndScale:Vector3;
var SpeedInt:float;
var TimeOfEndScale:float;

private var Non:boolean=false;

private var ResultSpeed:float;
private var ResultOfScale:float;

var SoundStartChange:AudioClip;
var SoundEndChange:AudioClip;

function Start(){
StartScale = gameObject.transform.localScale;
if(SpeedInt != 0){ ResultSpeed = 0.01 * SpeedInt; } else{ ResultSpeed = 0.01; }
var DeltaScale = StartScale - EndScale;
ResultOfScale = Mathf.Max(DeltaScale.x, DeltaScale.y, DeltaScale.z);
}

function OnMouseDown(){
if(gameObject.GetComponent.<CubesWithScale>().enabled == true){
if(!Non){Non=true; Work();}
}
}
function OnTouchDown(){if(!Non){Non=true; Work();}}

function Work(){
GameObject.Find("Main Camera").GetComponent.<CameraMove>().DeleteDist();
GameObject.Find("Main Camera").GetComponent.<CameraMove>().AllDistance -= 5;
GameObject.Find("Delete Distance").GetComponent.<UI.Text>().text = "-5";
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundStartChange);
var Point: int =0;
var ScaleController:float;

while(Point == 0){
if(ScaleController < ResultOfScale){
if(gameObject.transform.localScale.x > EndScale.x){
gameObject.transform.localScale.x -= ResultSpeed;
}
if(gameObject.transform.localScale.y > EndScale.y){
gameObject.transform.localScale.y -= ResultSpeed;
}
if(gameObject.transform.localScale.z > EndScale.z){
gameObject.transform.localScale.z -= ResultSpeed;
}
ScaleController += ResultSpeed;
}else{Point = 1;}
yield WaitForSeconds(0.033);
}

yield WaitForSeconds(TimeOfEndScale);
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundEndChange);

while(Point == 1){
if(ScaleController > 0){
if(gameObject.transform.localScale.x < StartScale.x){
gameObject.transform.localScale.x += ResultSpeed;
}
if(gameObject.transform.localScale.y < StartScale.y){
gameObject.transform.localScale.y += ResultSpeed;
}
if(gameObject.transform.localScale.z < StartScale.z){
gameObject.transform.localScale.z += ResultSpeed;
}
ScaleController -= ResultSpeed;
}else{Point = 2;}
yield WaitForSeconds(0.033);
}

Non=false;
}