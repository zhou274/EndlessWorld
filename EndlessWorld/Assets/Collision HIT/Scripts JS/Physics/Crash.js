#pragma strict

private var Effect_to:GameObject;
var Effect_after:GameObject;
var SoundOfTap:AudioClip;
private var LoopNon:boolean=true;
private var RealySpeed:float;

private var MainCamera:GameObject;
private var VarianceSpeed:float;

function Start(){
MainCamera=GameObject.Find("Main Camera");
Effect_to = gameObject.transform.parent.FindChild("Fireball").gameObject;
RealySpeed = gameObject.transform.parent.transform.localScale.x / 40;
VarianceSpeed=gameObject.transform.parent.transform.localScale.x * Random.Range(-0.007,0.007);
gameObject.GetComponent.<AudioSource>().pitch += VarianceSpeed*30.01;
}



function OnTouchDown(){
Doing();
}

function OnMouseDown(){
if(gameObject.GetComponent.<Crash>().enabled == true){
Doing();
}
}

function Doing(){
if(LoopNon){
LoopNon=false;
gameObject.GetComponent.<BoxCollider>().enabled = false;
Effect_to.GetComponent.<LensFlare>().color = Color32(157,0,0,0);
Effect_to.GetComponent.<LensFlare>().brightness =0.7;
MainCamera.GetComponent.<SystemKilling>().AddHP();
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundOfTap);
Effect_after.SetActive(true);
var Point:int = 0;
while (Point == 0){
if(gameObject.transform.parent.transform.localScale.z>0){
gameObject.transform.parent.transform.localScale.z -= RealySpeed * 1.08+VarianceSpeed;
if(gameObject.transform.parent.transform.localScale.x>0){
MainCamera.GetComponent.<SystemKilling>().JustHP += 0.0025;
}
}else{
MainCamera.GetComponent.<SystemKilling>().AddHPEnd();
gameObject.transform.parent.transform.localScale = Vector3(0,0,0);
Point = 1;
Effect_to.SetActive(false);
yield WaitForSeconds(4);
Destroy(gameObject.transform.parent.gameObject);
}
yield WaitForSeconds(0.05);
}
}
}
