#pragma strict

var MainObject:GameObject;
var PlayAnimation:boolean=false;
var AddForces:Vector3;
var AddRotation:Vector3;


private var DontRepeat:boolean = false;


function OnTriggerEnter(other: Collider){ 
if (gameObject.GetComponent.<TriggerControl> ().enabled == true) {
if(other.gameObject.tag=="Camera"){
if(!DontRepeat){ DontRepeat = true;
if(PlayAnimation){
MainObject.GetComponent.<Animation>().Play();
}else{
MainObject.GetComponent.<Rigidbody>().AddForce(AddForces*1);
MainObject.GetComponent.<Rigidbody>().angularVelocity = AddRotation*1;
}
}
}
}
}