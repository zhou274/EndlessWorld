#pragma strict

@HideInInspector
var BaseSpeed:float;
var ApproxSpeed:float;

@HideInInspector
var AllDistance:float; // All Distance
var OnlyDistance:float; //Distance on this CP
@HideInInspector
var DistanceForUI:int;
@HideInInspector
var OnlyDistanceSaved:float;

@HideInInspector
var RotateAngle:float; //Angle of rotation
@HideInInspector
var RotateSpeed:float; //Speed of rotation

private var DistanceUI:GameObject;
private var DeleteDistanceUI:GameObject;
private var Way_LineUI:GameObject;
private var OnceDelete:int = 0;

function Start(){
BaseSpeed = ApproxSpeed;
Way_LineUI = GameObject.Find("Way Line");
DistanceUI = GameObject.Find("Distance");
DeleteDistanceUI = GameObject.Find("Delete Distance");
UpdateHealth();
}

function NewWayLine(){
OnlyDistanceSaved=0;
var DistanceSave: int = AllDistance;
PlayerPrefs.SetInt("AllDistance",DistanceSave);
}


function DeleteDist(){
if(OnceDelete == 0){ 
OnceDelete = 1;
DeleteDistanceUI.GetComponent.<Animation>().Play("DeleteDistance");
var Point: int = 0;
while(Point == 0){
if(DeleteDistanceUI.GetComponent.<UI.Text>().color.a < 0.77){
DeleteDistanceUI.GetComponent.<UI.Text>().color.a += 0.05;
}else{Point = 1;}
yield WaitForSeconds(0.033);
}
while(Point == 1){
if(DeleteDistanceUI.GetComponent.<UI.Text>().color.a > 0){
DeleteDistanceUI.GetComponent.<UI.Text>().color.a -= 0.05;
}else{Point = 2;}
yield WaitForSeconds(0.033);
}
OnceDelete = 0;
}
}

function SaveDistance(){
if(PlayerPrefs.GetInt("AllDistance") < AllDistance){
PlayerPrefs.SetInt("AllDistance",AllDistance);
}
}

function Rotate(){
var Point:int = 0;
while (Point == 0){
if(BaseSpeed > 0.05){
if(RotateAngle>0){
if(RotateSpeed<0){RotateAngle += RotateSpeed;}
else{RotateAngle -= RotateSpeed;}
gameObject.transform.Rotate(0,0,RotateSpeed);
}else{Point = 1;}
}else{Point = 1;}
yield WaitForSeconds(0.033);
}
}

function FixedUpdate () {
gameObject.transform.position.z += BaseSpeed;
AllDistance += BaseSpeed;
OnlyDistanceSaved += BaseSpeed;

Way_LineUI.GetComponent.<UI.Image>().fillAmount = OnlyDistanceSaved/OnlyDistance;

if(BaseSpeed<ApproxSpeed){BaseSpeed += BaseSpeed * 0.01;}
if(BaseSpeed>ApproxSpeed){BaseSpeed -= BaseSpeed * 0.01;}
}

function UpdateHealth(){
var Point: int = 0;
while(Point == 0){
DistanceForUI = AllDistance;

if(DistanceForUI<1000){
DistanceUI.GetComponent.<UI.Text>().text = DistanceForUI.ToString();
}
if(DistanceForUI>=1000 && DistanceForUI<10000){
DistanceUI.GetComponent.<UI.Text>().text=DistanceForUI.ToString().Substring(0,1)+" "+DistanceForUI.ToString().Substring(1);
}
if(DistanceForUI>=10000 && DistanceForUI<100000){
DistanceUI.GetComponent.<UI.Text>().text=DistanceForUI.ToString().Substring(0,2)+" "+DistanceForUI.ToString().Substring(2);
}
if(DistanceForUI>100000){
DistanceUI.GetComponent.<UI.Text>().text=DistanceForUI.ToString().Substring(0,3)+" "+DistanceForUI.ToString().Substring(3);
}

yield WaitForSeconds(0.1);
}
}

function SpeedOfCameraDown(){
var Point:int = 0;
while(Point == 0){
if(ApproxSpeed>0){
ApproxSpeed -= 0.005;
BaseSpeed -= 0.005;
}else{Point = 1;}
yield WaitForSeconds(0.033);
}
}
