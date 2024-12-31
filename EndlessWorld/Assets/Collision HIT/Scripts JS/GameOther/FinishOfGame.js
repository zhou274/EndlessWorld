#pragma strict


private var CP_Look: GameObject;
var MessageToPlayer: String;

var NewGamma: int;

private var DontRepeat:boolean=false;

function Start(){
CP_Look = GameObject.Find("CP Look");

}

function OnTriggerEnter(other: Collider){ 
if(other.gameObject.tag=="Camera"){
if(!DontRepeat){ DontRepeat = true;
CP_Look.GetComponent.<UI.Text>().color.a = 1;
CP_Look.GetComponent.<UI.Text>().text = MessageToPlayer;
CP_Look.GetComponent.<Animation>().Play("Finish CP");

yield WaitForSeconds(2);
var Point: int = 0;
while(Point == 0){
if(CP_Look.GetComponent.<UI.Text>().color.a > 0){
CP_Look.GetComponent.<UI.Text>().color.a -= 0.1;
}else{Point = 1;}
yield WaitForSeconds(0.033);
}
CP_Look.GetComponent.<UI.Text>().enabled = false;

if(PlayerPrefs.GetInt("SaveCPAll") < NewGamma){
PlayerPrefs.SetInt("SaveCPAll",NewGamma);
}

PlayerPrefs.SetInt("SaveCPNow",-1);


GameObject.Find("Main Camera").GetComponent.<CameraMove>().SpeedOfCameraDown();
PlayerPrefs.SetInt("Room 1",1);
yield WaitForSeconds(2);

var Music: GameObject = GameObject.Find("Music");
var BlackFirst: GameObject = GameObject.Find("Black Fon 1");
var BlackSecond: GameObject = GameObject.Find("Black Fon 2");
BlackFirst.GetComponent.<UI.Image>().enabled=true;
BlackSecond.GetComponent.<UI.Image>().enabled=true;

while(Point == 1){
if (BlackSecond.GetComponent.<UI.Image>().color.a<1){
BlackFirst.GetComponent.<UI.Image>().color.a += 0.05;
BlackSecond.GetComponent.<UI.Image>().color.a += 0.025;
Music.GetComponent.<AudioSource>().pitch += 0.025;
}else{
Music.GetComponent.<AudioSource>().pitch=0;
Point = 2;
}
yield WaitForSeconds(0.05);
}
yield WaitForSeconds(1);
Application.LoadLevel(GameObject.Find("Canvas Inteface").GetComponent.<PauseGame>().NameOfMenu);
}
}
}