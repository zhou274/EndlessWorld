#pragma strict

var GeneralAngle:int;
var SpeedOfTurn:float;
var AudioPlay:AudioClip;
private var DontRepeat:boolean=false;

function OnTriggerEnter(other: Collider){ 
if(other.gameObject.tag=="Camera"){
if(!DontRepeat){ DontRepeat = true;
other.gameObject.GetComponent.<CameraMove>().RotateSpeed = SpeedOfTurn;
other.gameObject.GetComponent.<CameraMove>().RotateAngle = GeneralAngle;
other.gameObject.GetComponent.<CameraMove>().Rotate();
gameObject.GetComponent.<AudioSource>().PlayOneShot(AudioPlay);
gameObject.GetComponent.<BoxCollider>().enabled = false;
gameObject.transform.parent = GameObject.Find("StaticParent").transform;
}
}
}