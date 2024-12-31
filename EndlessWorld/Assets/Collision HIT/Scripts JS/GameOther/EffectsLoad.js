#pragma strict

var ColorOfDL:Color[];
var IntensityOfDL:float[];
var ColorOfFog:Color[];

private var DL2:GameObject;


@HideInInspector
var FirstSettings: int;




function Start(){
DL2 = GameObject.Find("Directional light 2");
}


function NewSettings(){
var Point: int = 0;
var FirstSet: int = 0;
var ReservedFloat: float = 1;
var SetColors:Vector3;



while(Point == 0){
if(ReservedFloat > 0){
if(FirstSet == 0){
SetColors.x = (RenderSettings.fogColor.r - ColorOfFog[FirstSettings].r) / 50;
SetColors.y = (RenderSettings.fogColor.g - ColorOfFog[FirstSettings].g) / 50;
SetColors.z = (RenderSettings.fogColor.b - ColorOfFog[FirstSettings].b) / 50;
FirstSet = 1;
}
RenderSettings.fogColor.r -= SetColors.x;
RenderSettings.fogColor.g -= SetColors.y;
RenderSettings.fogColor.b -= SetColors.z;
gameObject.GetComponent.<Camera>().backgroundColor = RenderSettings.fogColor;

ReservedFloat -= 0.02;
 
}else{Point = 1;}
yield WaitForSeconds(0.03);
}
while(Point == 1){
if(ReservedFloat < 1){
if(FirstSet == 1){
SetColors.x = (DL2.GetComponent.<Light>().color.r - ColorOfDL[FirstSettings].r) / 50;
SetColors.y = (DL2.GetComponent.<Light>().color.g - ColorOfDL[FirstSettings].g) / 50;
SetColors.z = (DL2.GetComponent.<Light>().color.b - ColorOfDL[FirstSettings].b) / 50;
FirstSet = 2;
}
DL2.GetComponent.<Light>().color.r -= SetColors.x;
DL2.GetComponent.<Light>().color.g -= SetColors.y;
DL2.GetComponent.<Light>().color.b -= SetColors.z;
ReservedFloat += 0.02;
}else{Point = 2;}
yield WaitForSeconds(0.03);
}
while(Point == 2){
if(ReservedFloat > 0){
if(FirstSet == 2){
SetColors.x = (DL2.GetComponent.<Light>().intensity - IntensityOfDL[FirstSettings]) / 50;
FirstSet = 3;
}
DL2.GetComponent.<Light>().intensity -= SetColors.x;
ReservedFloat -= 0.02;
}else{Point = 3;}
yield WaitForSeconds(0.03);
}
}