#pragma strict

var SoundPlay:AudioClip;
private var Non:boolean=false;
var AddForces_1:Vector3;
var Wait:float;
var AddForces_2:Vector3;
var AddRotation_2:Vector3;
var ExplosionPartice:GameObject;

function OnMouseDown(){
if(gameObject.GetComponent.<CubesTapExplosion>().enabled == true){
if(!Non){Non=true; Work();}
}
}
function OnTouchDown(){if(!Non){Non=true; Work();}}

function Work(){
GameObject.Find("Main Camera").GetComponent.<CameraMove>().DeleteDist();
GameObject.Find("Main Camera").GetComponent.<CameraMove>().AllDistance -= 3;
GameObject.Find("Delete Distance").GetComponent.<UI.Text>().text = "-3";
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundPlay);
if(gameObject.GetComponent.<Animation>()){gameObject.GetComponent.<Animation>().Stop();}
if(gameObject.transform.parent.GetComponent.<Animation>()){gameObject.transform.parent.GetComponent.<Animation>().Stop();}
gameObject.GetComponent.<Rigidbody>().AddForce(AddForces_1*1);
yield WaitForSeconds(Wait);
gameObject.GetComponent.<Rigidbody>().velocity = Vector3.zero;
ExplosionPartice.SetActive(true);
gameObject.GetComponent.<Rigidbody>().AddForce(AddForces_2*1);
gameObject.GetComponent.<Rigidbody>().angularVelocity = AddRotation_2*1;
}