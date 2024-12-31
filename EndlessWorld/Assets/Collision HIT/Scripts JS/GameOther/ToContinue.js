#pragma strict

var RoomsSet:GameObject[];
var ZPositionOfCamera: Vector3[];

function Start(){
if(PlayerPrefs.GetInt("ContinueTrue") == 1){
Destroy(RoomsSet[0]);
Destroy(RoomsSet[1]);
yield WaitForSeconds(0.1);
SetANewParameters();
}
}

function SetANewParameters(){
var SaveCPNow: int = PlayerPrefs.GetInt("SaveCPNow");
//var SaveCPNow: int = 5;
GameObject.Find("prop with Camera").transform.position = ZPositionOfCamera[SaveCPNow];
var NewPart_1: GameObject = Instantiate(RoomsSet[SaveCPNow*2]);
var NewPart_2: GameObject = Instantiate(RoomsSet[SaveCPNow*2+1]);

NewPart_1.transform.parent = gameObject.transform;
NewPart_2.transform.parent = gameObject.transform;

NewPart_1.name = NewPart_1.name.Substring(0,(NewPart_1.name.Length-7));
NewPart_2.name = NewPart_2.name.Substring(0,(NewPart_2.name.Length-7));


RenderSettings.fogColor = GameObject.Find("Main Camera").GetComponent.<EffectsLoad>().ColorOfFog[SaveCPNow];
GameObject.Find("Main Camera").GetComponent.<Camera>().backgroundColor = RenderSettings.fogColor;
GameObject.Find("Directional light 2").GetComponent.<Light>().color = GameObject.Find("Main Camera").GetComponent.<EffectsLoad>().ColorOfDL[SaveCPNow];
GameObject.Find("Directional light 2").GetComponent.<Light>().intensity = GameObject.Find("Main Camera").GetComponent.<EffectsLoad>().IntensityOfDL[SaveCPNow];
GameObject.Find("Main Camera").GetComponent.<SystemKilling>().JustHP = PlayerPrefs.GetFloat("RealHealth");
GameObject.Find("Main Camera").GetComponent.<CameraMove>().AllDistance = PlayerPrefs.GetFloat("RealDistance");
GameObject.Find("Main Camera").GetComponent.<CameraMove>().OnlyDistance = PlayerPrefs.GetFloat("RealDistanceOnly");
GameObject.Find("Main Camera").GetComponent.<CameraMove>().OnlyDistanceSaved = PlayerPrefs.GetFloat("RealDistanceOnlySaved");
GameObject.Find("AmazeOther").GetComponent.<UI.Text>().text = (SaveCPNow+1).ToString();
}