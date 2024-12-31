#pragma strict

var SoundPlay:AudioClip;
private var Non:boolean=false;
var AddForces:Vector3;
var AddRotation:Vector3;
var WaitSeconds:float;
var AddForces2:Vector3;
var AddRotation2:Vector3;
var Contacts:GameObject;

private var Other: boolean = false;

function Start(){
gameObject.GetComponent.<AudioSource>().volume = 0.6;
}


function OnMouseDown(){
if(gameObject.GetComponent.<CubesTap>().enabled == true){
if(!Non){Non=true; Work();}
}
}
function OnTouchDown(){if(!Non){Non=true; Work();}}

function Work(){
GameObject.Find("Main Camera").GetComponent.<CameraMove>().DeleteDist();
GameObject.Find("Main Camera").GetComponent.<CameraMove>().AllDistance -= 5;
GameObject.Find("Delete Distance").GetComponent.<UI.Text>().text = "-5";
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundPlay);
gameObject.GetComponent.<Rigidbody>().isKinematic = false;
if(gameObject.GetComponent.<Animation>()){gameObject.GetComponent.<Animation>().Stop();}
if(gameObject.transform.parent.GetComponent.<Animation>()){gameObject.transform.parent.GetComponent.<Animation>().Stop();}
gameObject.GetComponent.<Rigidbody>().AddForce(AddForces*1);
gameObject.GetComponent.<Rigidbody>().angularVelocity += AddRotation*1;
SecondWork();
if(Contacts){
Contacts.GetComponent.<CubesTap>().SecondWork();
if(Contacts.GetComponent.<Animation>()){Contacts.GetComponent.<Animation>().Play();}
}
}

function SecondWork(){
if(!Other){ Other = true;
yield WaitForSeconds(WaitSeconds);
gameObject.GetComponent.<Rigidbody>().AddForce(AddForces2*1);
gameObject.GetComponent.<Rigidbody>().angularVelocity += AddRotation2*1;
}
}