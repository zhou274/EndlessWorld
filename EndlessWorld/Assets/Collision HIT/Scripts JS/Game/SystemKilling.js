#pragma strict

@HideInInspector
var JustHP:float;

private var LineOfHP:GameObject;
private var BadlyNaw:int;
private var SecLine_zero:GameObject;

@HideInInspector
var SoundOfCollision:AudioClip;

@HideInInspector
var NoDeleteHP:boolean=false;

private var PointSec_0:GameObject;
private var PointSec_1:GameObject;
private var PointSec_2:GameObject;
private var PointSec_3:GameObject;

private var PWC:GameObject;
private var FB:GameObject;
private var NT:GameObject;
private var Mus:GameObject;
private var B1:GameObject;
private var B2:GameObject;

function Start(){
PointSec_0 = GameObject.Find("Security 0");
PointSec_1 = GameObject.Find("Security 1");
PointSec_2 = GameObject.Find("Security 2");
PointSec_3 = GameObject.Find("Security 3");
LineOfHP = GameObject.Find("Sec Line");
SecLine_zero = GameObject.Find("Sec Line 0");
PWC = GameObject.Find("prop with Camera");
FB = GameObject.Find("Fon Badly");
NT = GameObject.Find("NoTap");
Mus = GameObject.Find("Music");
B1 = GameObject.Find("Black Fon 1");
B2 = GameObject.Find("Black Fon 2");
JustHP = 1;
RenderUp();
}

function OnTriggerEnter(other:Collider){ 
if(gameObject.GetComponent.<SystemKilling>().enabled == true){
if(other.gameObject.tag=="CollisionWith"){
if(BadlyNaw==0){ BadlyNaw=1; 
CollisionWith();
}
}
}
}


function CollisionWith(){
if(JustHP>=1){
JustHP -= 0.99; CollisionGood();
}
else{JustHP = 0; CollisionBad();
}
yield WaitForSeconds(2);
BadlyNaw=0;
}

function AddHP(){
SecLine_zero.GetComponent.<UI.Image>().color.a = 1;
NoDeleteHP=true;
}

function AddHPEnd(){
var Point:int =0;
while(Point == 0){
if(SecLine_zero.GetComponent.<UI.Image>().color.a>0.3){
SecLine_zero.GetComponent.<UI.Image>().color.a -= 0.05;
}else{Point = 1; NoDeleteHP=false;}
yield WaitForSeconds(0.05);
}
}

function CollisionGood(){
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundOfCollision);
PWC.GetComponent.<Animation>().Play();
var Point:int = 0;
FB.GetComponent.<UI.Image>().enabled=true;
FB.GetComponent.<UI.Image>().color.a=1;
while(Point == 0){
if(FB.GetComponent.<UI.Image>().color.a>0){
FB.GetComponent.<UI.Image>().color.a -= 0.04;
}else{
Point = 1;
FB.GetComponent.<UI.Image>().enabled=false;
}
yield WaitForSeconds(0.05);
}
}

function CollisionBad(){
gameObject.GetComponent.<CameraMove>().SaveDistance();
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundOfCollision);
NT.GetComponent.<BoxCollider>().enabled=true;
PWC.GetComponent.<Animation>().Play();
FB.GetComponent.<UI.Image>().enabled=true;
FB.GetComponent.<UI.Image>().color.a=1;
gameObject.GetComponent.<CameraMove>().SpeedOfCameraDown();
yield WaitForSeconds(2);
B1.GetComponent.<UI.Image>().enabled=true;
B2.GetComponent.<UI.Image>().enabled=true;
var Point: int = 0;
while(Point == 0){
if (B2.GetComponent.<UI.Image>().color.a<1){
B1.GetComponent.<UI.Image>().color.a += 0.05;
B2.GetComponent.<UI.Image>().color.a += 0.025;
Mus.GetComponent.<AudioSource>().pitch -= 0.025;
}else{
Mus.GetComponent.<AudioSource>().pitch=0;
Point = 1;
}
yield WaitForSeconds(0.05);
}
yield WaitForSeconds(1);
Application.LoadLevel(GameObject.Find("Canvas Inteface").GetComponent.<PauseGame>().NameOfMenu);
}







function FixedUpdate(){
if(NoDeleteHP==false){
if(JustHP>1.05&&JustHP<2){
JustHP -= 0.0005;
}
if(JustHP>=2){
JustHP -= 0.001;
}
}
}

function RenderUp(){
var Point: int = 0;
while(Point == 0){
LineOfHP.GetComponent.<UI.Image>().fillAmount = JustHP/3;

if(JustHP<=0){
PointSec_0.GetComponent.<UI.Image>().color.a=0.3;
PointSec_1.GetComponent.<UI.Image>().color.a=0.3;
PointSec_2.GetComponent.<UI.Image>().color.a=0.3;
PointSec_3.GetComponent.<UI.Image>().color.a=0.3;
}
if(JustHP>0&&JustHP<1){
PointSec_0.GetComponent.<UI.Image>().color.a=1;
PointSec_1.GetComponent.<UI.Image>().color.a=0.3;
PointSec_2.GetComponent.<UI.Image>().color.a=0.3;
PointSec_3.GetComponent.<UI.Image>().color.a=0.3;
}
if(JustHP>=1&&JustHP<2){
PointSec_0.GetComponent.<UI.Image>().color.a=1;
PointSec_1.GetComponent.<UI.Image>().color.a=1;
PointSec_2.GetComponent.<UI.Image>().color.a=0.3;
PointSec_3.GetComponent.<UI.Image>().color.a=0.3;
}
if(JustHP>=2&&JustHP<3){
PointSec_0.GetComponent.<UI.Image>().color.a=1;
PointSec_1.GetComponent.<UI.Image>().color.a=1;
PointSec_2.GetComponent.<UI.Image>().color.a=1;
PointSec_3.GetComponent.<UI.Image>().color.a=0.3;
}
if(JustHP>=3){
PointSec_0.GetComponent.<UI.Image>().color.a=1;
PointSec_1.GetComponent.<UI.Image>().color.a=1;
PointSec_2.GetComponent.<UI.Image>().color.a=1;
PointSec_3.GetComponent.<UI.Image>().color.a=1;
}
yield WaitForSeconds(0.1);
}
}