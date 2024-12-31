#pragma strict

var Rope:GameObject;
private var LineRope:LineRenderer;

var ArrayPoint:GameObject[];
var AddForce:Vector3;
var SoundOfTap:AudioClip;
var OneForce:boolean=false;

function Start(){
LineRope = Rope.GetComponent.<LineRenderer>();
Rope.SetActive(true);
}


function OnMouseDown(){
if(gameObject.GetComponent.<CubesRope>().enabled == true){
Work();
}
}
function OnTouchDown(){Work();}

function Work(){
GameObject.Find("Main Camera").GetComponent.<CameraMove>().DeleteDist();
GameObject.Find("Main Camera").GetComponent.<CameraMove>().AllDistance -= 1;
GameObject.Find("Delete Distance").GetComponent.<UI.Text>().text = "-1";
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundOfTap);
if(!OneForce){
if(gameObject.transform.localPosition.z<0){
gameObject.GetComponent.<Rigidbody>().velocity = 
gameObject.GetComponent.<Rigidbody>().velocity - AddForce*1;
}else{
gameObject.GetComponent.<Rigidbody>().velocity = 
gameObject.GetComponent.<Rigidbody>().velocity + AddForce*1;
}
}else{
gameObject.GetComponent.<Rigidbody>().velocity = 
gameObject.GetComponent.<Rigidbody>().velocity + AddForce*1;
}
}

function Update(){
var Point:int = 0;
var Recorder:int = 0;
while(Point == 0){
if(Recorder<ArrayPoint.Length){
LineRope.SetPosition(Recorder,ArrayPoint[Recorder].transform.position);
Recorder++;
}else{Point=1;}
}
}