#pragma strict

var NameOfLevel:String;
var NameOfMenu:String;
var VolumeOfMusic:float;
var PausePanel:GameObject;
private var Lock:int=0;
private var BlackFirst:GameObject;
private var BlackSecond:GameObject;
private var BlackThird:GameObject;
private var MusicObject:GameObject;
private var SaveTime:float;
private var SavePitch:float;
private var FindCamera:GameObject;


function Start(){
BlackFirst = GameObject.Find("Black Fon 1");
BlackSecond = GameObject.Find("Black Fon 2");
BlackThird = GameObject.Find("Black Fon 3");
MusicObject = GameObject.Find("Music");
FindCamera = GameObject.Find("Main Camera");
var Point: int = 0;
while(Point == 0){
if (BlackFirst.GetComponent.<UI.Image>().color.a > 0){
BlackFirst.GetComponent.<UI.Image>().color.a -= 0.05;
BlackSecond.GetComponent.<UI.Image>().color.a -= 0.05;
if(MusicObject.GetComponent.<AudioSource>().volume<VolumeOfMusic){
MusicObject.GetComponent.<AudioSource>().volume += 0.05;
}
}else{
Point = 1;
BlackFirst.GetComponent.<UI.Image>().enabled=false;
BlackSecond.GetComponent.<UI.Image>().enabled=false;
}
yield WaitForSeconds(0.05);
}
}

function OnPause(){if(Lock == 0){ Lock=1; TapOnPause();}}
function TapBeyond(){if(Lock == 0){ Lock=1; TapOnBeyond();}}
function TapReset(){if(Lock == 0){ TapOnReset();}}
function TapMenu(){if(Lock == 0){ TapOnMenu();}}

function TapOnPause(){
PausePanel.SetActive(true);
FindCamera.GetComponent.<CameraMove>().SaveDistance();
GameObject.Find("Pause Table").GetComponent.<UI.Image>().enabled=true;
gameObject.GetComponent.<Animation>().Play("PauseInGameTrue");
GameObject.Find("NoTap").GetComponent.<BoxCollider>().enabled=true;
BlackThird.GetComponent.<UI.Image>().enabled=true;
SavePitch = MusicObject.GetComponent.<AudioSource>().pitch;
var Point: int = 0;
while (Point == 0){
if(MusicObject.GetComponent.<AudioSource>().pitch > 0.1){
MusicObject.GetComponent.<AudioSource>().pitch -= 0.1;
BlackThird.GetComponent.<UI.Image>().color.a += 0.025;
}else{Point = 1;}
yield WaitForSeconds(0.033);
}
MusicObject.GetComponent.<AudioSource>().pitch=0;
VolumeOfMusic = MusicObject.GetComponent.<AudioSource>().volume;
Lock=0;
if(Time.timeScale < 1){
yield WaitForSeconds(0.3);
}
SaveTime = Time.timeScale;
AudioListener.pause=true;
Time.timeScale=0;
}

function TapOnBeyond(){ 
Time.timeScale = SaveTime;
if(PlayerPrefs.GetInt("SoundActive") == 1){
AudioListener.pause=false;
}
gameObject.GetComponent.<Animation>().Play("PauseInGameFalse");
GameObject.Find("NoTap").GetComponent.<BoxCollider>().enabled=false;
var Point: int = 0;
while (Point == 0){
if(MusicObject.GetComponent.<AudioSource>().pitch < 1){
MusicObject.GetComponent.<AudioSource>().pitch += 0.1;
BlackThird.GetComponent.<UI.Image>().color.a -= 0.025;
}else{Point = 1;
BlackThird.GetComponent.<UI.Image>().enabled=false;
}
yield WaitForSeconds(0.033);
}
MusicObject.GetComponent.<AudioSource>().pitch = SavePitch;
GameObject.Find("Pause Table").GetComponent.<UI.Image>().enabled=false;
while (Point == 1){
if(MusicObject.GetComponent.<AudioSource>().volume<VolumeOfMusic){
MusicObject.GetComponent.<AudioSource>().volume += 0.01;
}else{Point = 2;}
yield WaitForSeconds(0.05);
}
Lock=0;
PausePanel.SetActive(false);
}

function TapOnReset(){
Time.timeScale = SaveTime;
if(PlayerPrefs.GetInt("SoundActive") == 1){
AudioListener.pause=false;
}
FindCamera.GetComponent.<CameraMove>().enabled=false;
gameObject.GetComponent.<Animation>().Play("PauseInGameFalseEnd");
BlackFirst.GetComponent.<UI.Image>().enabled=true;
BlackSecond.GetComponent.<UI.Image>().enabled=true;
var Point: int = 0;
while(Point < 2){
if (BlackSecond.GetComponent.<UI.Image>().color.a<1){
if(Point == 0){
if(MusicObject.GetComponent.<AudioSource>().pitch < 0.5){
MusicObject.GetComponent.<AudioSource>().pitch += 0.1;
}else{Point = 1;}
}
if(Point == 1){
if(MusicObject.GetComponent.<AudioSource>().pitch > 0){
MusicObject.GetComponent.<AudioSource>().pitch -= 0.1;
}
}
BlackFirst.GetComponent.<UI.Image>().color.a += 0.1;
BlackThird.GetComponent.<UI.Image>().color.a -= 0.025;
BlackSecond.GetComponent.<UI.Image>().color.a += 0.05;
}else{
Point = 2;
MusicObject.GetComponent.<AudioSource>().pitch = 0;
Application.LoadLevel(NameOfLevel);
}
yield WaitForSeconds(0.04);
}
PausePanel.SetActive(false);
}

function TapOnMenu(){
Time.timeScale = SaveTime;
if(PlayerPrefs.GetInt("SoundActive") == 1){
AudioListener.pause=false;
}
FindCamera.GetComponent.<CameraMove>().enabled=false;
gameObject.GetComponent.<Animation>().Play("PauseInGameFalseEnd");;
BlackFirst.GetComponent.<UI.Image>().enabled=true;
BlackSecond.GetComponent.<UI.Image>().enabled=true;
MusicObject.GetComponent.<AudioSource>().pitch = 1.1;
var Point: int = 0;
while(Point < 2){
if (BlackSecond.GetComponent.<UI.Image>().color.a<1){
if(Point == 0){
if(MusicObject.GetComponent.<AudioSource>().pitch < 0.5){
MusicObject.GetComponent.<AudioSource>().pitch += 0.1;
}else{Point = 1;}
}
if(Point == 1){
if(MusicObject.GetComponent.<AudioSource>().pitch > 0){
MusicObject.GetComponent.<AudioSource>().pitch -= 0.1;
}
}
MusicObject.GetComponent.<AudioSource>().pitch -= 0.04;
BlackFirst.GetComponent.<UI.Image>().color.a += 0.1;
BlackThird.GetComponent.<UI.Image>().color.a -= 0.025;
BlackSecond.GetComponent.<UI.Image>().color.a += 0.05;
}else{
Point = 2;
MusicObject.GetComponent.<AudioSource>().pitch = 0;
Application.LoadLevel(NameOfMenu);
}
yield WaitForSeconds(0.04);
}
PausePanel.SetActive(false);
}