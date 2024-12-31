#pragma strict

var FromPrefab:GameObject;

var NameOfNextPart:String;
private var PivotOfNewPart:Transform;
private var MoreDistance:float;

var NewSpeedOfMove:float;

var IfThisLastPart: boolean=false;
var DistanceOfNewCP:float;
var TextInLB:String;
var NewGamma: int;

private var OldPart:GameObject;
private var MainCam:GameObject;
private var DontRepeat:boolean=false;

function Start(){
PivotOfNewPart = transform.FindChild("Pivot");
OldPart = GameObject.Find(gameObject.transform.parent.name);
MainCam = GameObject.Find("Main Camera");
MoreDistance = -gameObject.transform.localPosition.z;
}

function OnTriggerEnter(other: Collider){ 
if(gameObject.GetComponent.<ResLoader>().enabled == true){
if(other.gameObject.tag=="Camera"){
if(!DontRepeat){ DontRepeat = true;
MainProcess();
}
}
}
}

function MainProcess(){
MainCam.GetComponent.<CameraMove>().ApproxSpeed = NewSpeedOfMove;
MainCam.GetComponent.<CameraMove>().SaveDistance();
var NewPart: GameObject = Instantiate(FromPrefab);
//var NewPart: GameObject = Instantiate(Resources.Load(LinkToNewPart,GameObject));
NewPart.transform.parent = GameObject.Find("LineRooms").transform;
NewPart.name = NewPart.name.Substring(0,(NewPart.name.Length-7));
NewPart.transform.position = PivotOfNewPart.position;
NewPart.transform.rotation = PivotOfNewPart.rotation;
if(IfThisLastPart){Reconstruction();}
yield WaitForSeconds(5);
MainCam.transform.position.z -= MoreDistance;
gameObject.transform.parent.transform.position.z -= MoreDistance;
GameObject.Find(NameOfNextPart).transform.position.z -= MoreDistance;
NewPart.transform.position.z -= MoreDistance;
OldPart.transform.position.z = -1000;
Destroy(OldPart);
}

function Reconstruction(){
MainCam.GetComponent.<CameraMove>().OnlyDistance = DistanceOfNewCP;
MainCam.GetComponent.<CameraMove>().NewWayLine();
GameObject.Find("AmazeOther").GetComponent.<UI.Text>().text = TextInLB;
MainCam.GetComponent.<EffectsLoad>().FirstSettings = NewGamma;
MainCam.GetComponent.<EffectsLoad>().NewSettings();

if(PlayerPrefs.GetFloat("SaveCPAll") < NewGamma){
PlayerPrefs.SetFloat("SaveCPAll",NewGamma);
}

PlayerPrefs.SetInt("SaveCPNow",NewGamma);
PlayerPrefs.SetInt("RealDistance",MainCam.GetComponent.<CameraMove>().DistanceForUI);
PlayerPrefs.SetFloat("RealHealth",MainCam.GetComponent.<SystemKilling>().JustHP);
PlayerPrefs.SetFloat("RealDistanceOnly",MainCam.GetComponent.<CameraMove>().OnlyDistance);
PlayerPrefs.SetFloat("RealDistanceOnlySaved",MainCam.GetComponent.<CameraMove>().OnlyDistanceSaved);

var CPLook: GameObject = GameObject.Find("CP Look");
CPLook.GetComponent.<UI.Text>().color.a = 1;
CPLook.GetComponent.<Animation>().Play("Saved CP");
yield WaitForSeconds(2);
var Point: int = 0;
while(Point == 0){
if(CPLook.GetComponent.<UI.Text>().color.a > 0){
CPLook.GetComponent.<UI.Text>().color.a -= 0.1;
}else{Point = 1;}
yield WaitForSeconds(0.033);
}
CPLook.GetComponent.<UI.Text>().enabled = false;
}