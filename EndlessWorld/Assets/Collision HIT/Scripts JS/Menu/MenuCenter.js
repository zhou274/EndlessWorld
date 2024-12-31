#pragma strict


private var SoundActive:int=0;
var SoundYes:Sprite;
var SoundNot:Sprite;
var MaxDistance:float;
var LevelName:String;
var LinkToRate:String;
var Coninue:Sprite;
private var HaveAccess: int = 0;

function Start(){
		if(PlayerPrefs.GetInt("Chance") > 0 && PlayerPrefs.GetInt("SaveCPNow") > 0){
			GameObject.Find("Checkpoint Go").GetComponent.<UI.Image>().sprite = Coninue;
			HaveAccess = 1;
		}
if(PlayerPrefs.HasKey("SoundActive")){
SoundActive=PlayerPrefs.GetInt("SoundActive");
}else{SoundActive=1;}
DoingSound();


if(PlayerPrefs.HasKey("AllDistance")){}else{PlayerPrefs.SetInt("AllDistance",0);}
GameObject.Find("Distance").GetComponent.<UI.Text>().text=PlayerPrefs.GetInt("AllDistance").ToString();
if(PlayerPrefs.GetInt("AllDistance")>=1000&&PlayerPrefs.GetInt("AllDistance")<10000){
GameObject.Find("Distance").GetComponent.<UI.Text>().text=PlayerPrefs.GetInt("AllDistance").ToString().Substring(0,1)+" "+PlayerPrefs.GetInt("AllDistance").ToString().Substring(1);
}
if(PlayerPrefs.GetInt("AllDistance")>=10000){
GameObject.Find("Distance").GetComponent.<UI.Text>().text=PlayerPrefs.GetInt("AllDistance").ToString().Substring(0,2)+" "+PlayerPrefs.GetInt("AllDistance").ToString().Substring(2);
}

if(PlayerPrefs.GetInt("AllDistance")>MaxDistance){
GameObject.Find("Way Infinity").GetComponent.<UI.Text>().enabled=true;
}
var fAm: float = PlayerPrefs.GetFloat("SaveCPAll") * 0.111;
GameObject.Find("Way Line").GetComponent.<UI.Image>().fillAmount=fAm;
yield WaitForSeconds(0.2);
var Point: int = 0;
var Black: GameObject = GameObject.Find("Black");
while(Point == 0){
if(Black.GetComponent.<UI.Image>().color.a>0){
Black.GetComponent.<UI.Image>().color.a -= 0.1;
}else{Point=1; Black.GetComponent.<UI.Image>().enabled=false;}
yield WaitForSeconds(0.05);
}
if(PlayerPrefs.HasKey("RateEnd")){}else{PlayerPrefs.SetInt("RateEnd",0);}
if(PlayerPrefs.GetInt("AllDistance") > 3000 && PlayerPrefs.GetInt("RateEnd") != 1){
GameObject.Find("Rate").GetComponent.<UI.Image>().enabled = true;
}

}

function TapOnGo(){OnGo();}
function OnGo(){
PlayerPrefs.SetInt("Chance",3);
PlayerPrefs.SetInt("ContinueTrue",0);
PlayerPrefs.SetInt("SaveCPNow",0);
var Black: GameObject = GameObject.Find("Black");
var Loading: GameObject = GameObject.Find("loading");
var MusicR: GameObject = GameObject.Find("Music");
var Triangle: GameObject = GameObject.Find("triangle");
MusicR.GetComponent.<Animation>().Stop();
Black.GetComponent.<UI.Image>().enabled=true;
var Point: int=0;
while(Point == 0){
if(Black.GetComponent.<UI.Image>().color.a<1){
Black.GetComponent.<UI.Image>().color.a += 0.1;
MusicR.GetComponent.<AudioSource>().volume -= 0.1;
}else{Point=1;}
yield WaitForSeconds(0.05);
}
Triangle.GetComponent.<UI.Image>().enabled=true;
Loading.GetComponent.<UI.Text>().enabled=true;
while(Point == 1){
if(Loading.GetComponent.<UI.Text>().color.a<1){
Loading.GetComponent.<UI.Text>().color.a += 0.05;

Triangle.GetComponent.<UI.Image>().color.a += 0.05;
}else{Point=2; Application.LoadLevel(LevelName);}
yield WaitForSeconds(0.05);
}
}

function TapOnSound(){
if(SoundActive==0){SoundActive=1;DoingSound();
}else{SoundActive=0;DoingSound();}
}

function TapOnExit(){
Application.Quit();
}


//

function DoingSound(){
if(SoundActive==0){AudioListener.pause=true;
GameObject.Find("Sound").GetComponent.<UI.Image>().sprite=SoundNot;}
if(SoundActive==1){AudioListener.pause=false;
GameObject.Find("Sound").GetComponent.<UI.Image>().sprite=SoundYes;}
PlayerPrefs.SetInt("SoundActive",SoundActive);
}

function RateNow(){
PlayerPrefs.SetInt("RateEnd",1);
Application.OpenURL(LinkToRate);
}

function TapOnGoContunie(){Continue();}

	function Continue(){
		if (HaveAccess == 1) {
			PlayerPrefs.SetInt("Chance",(PlayerPrefs.GetInt("Chance")-1));
			PlayerPrefs.SetInt("ContinueTrue",1);
			var Black: GameObject = GameObject.Find("Black");
			var Loading: GameObject = GameObject.Find("loading");
			var MusicR: GameObject = GameObject.Find("Music");
			var Triangle: GameObject = GameObject.Find("triangle");
			MusicR.GetComponent.<Animation>().Stop();
			Black.GetComponent.<UI.Image>().enabled=true;
			var Point: int =0;
			while(Point == 0){
				if(Black.GetComponent.<UI.Image>().color.a<1){
					Black.GetComponent.<UI.Image>().color.a += 0.1;
					MusicR.GetComponent.<AudioSource>().volume -= 0.1;
				}else{Point=1;}
				yield WaitForSeconds(0.05);
			}
			Triangle.GetComponent.<UI.Image>().enabled=true;
			Loading.GetComponent.<UI.Text>().enabled=true;
			while(Point == 1){
				if(Loading.GetComponent.<UI.Text>().color.a<1){
					Loading.GetComponent.<UI.Text>().color.a += 0.05;
					Triangle.GetComponent.<UI.Image>().color.a += 0.05;
				}else{Point=2; Application.LoadLevel(LevelName);}
				yield WaitForSeconds(0.05);
			}
		}

	}