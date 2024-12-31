#pragma strict

var SoundPlay:AudioClip;
var AddForces:Vector3;
var AddRotation:Vector3;
var Attempt:int;

function Start(){
gameObject.GetComponent.<AudioSource>().volume = 0.8;
}

function OnMouseDown(){
if(gameObject.GetComponent.<CubesManyTap>().enabled == true){
if(Attempt > 0){Work(); Attempt--;}
else{
gameObject.GetComponent.<CubesManyTap>().enabled=false;
}
}
}

function Work(){
GameObject.Find("Main Camera").GetComponent.<CameraMove>().DeleteDist();
GameObject.Find("Main Camera").GetComponent.<CameraMove>().AllDistance -= 1;
GameObject.Find("Delete Distance").GetComponent.<UI.Text>().text = "-1";
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundPlay);
gameObject.GetComponent.<Rigidbody>().AddForce(AddForces*1);
gameObject.GetComponent.<Rigidbody>().angularVelocity += AddRotation*1;
}