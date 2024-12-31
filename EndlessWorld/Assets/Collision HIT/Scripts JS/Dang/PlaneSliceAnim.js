#pragma strict

private var Once:boolean=true;
var NameOfAnimation:AnimationClip;
var DoubleTime:boolean=false;
var SoundOfTap:AudioClip;
var StartObject:GameObject;
var ArrayOfPieces:GameObject[];


function OnMouseDown(){
if(gameObject.GetComponent.<PlaneSliceAnim>().enabled == true){
gameObject.transform.parent.GetComponent.<PlaneSliceAnim>().Work();
}
}
function OnTouchDown(){gameObject.transform.parent.GetComponent.<PlaneSliceAnim>().Work();}


function Work(){
if(Once){
GameObject.Find("Main Camera").GetComponent.<CameraMove>().DeleteDist();
GameObject.Find("Main Camera").GetComponent.<CameraMove>().AllDistance -= 4;
GameObject.Find("Delete Distance").GetComponent.<UI.Text>().text = "-4";
Once=false;
StartObject.GetComponent.<MeshRenderer>().enabled=false;
var Point: int = 0;
while (Point<ArrayOfPieces.Length){
ArrayOfPieces[Point].GetComponent.<MeshRenderer>().enabled=true;
Point++;
}
gameObject.GetComponent.<AudioSource>().PlayOneShot(SoundOfTap);
gameObject.GetComponent.<Animation>().Play(NameOfAnimation.name);
yield WaitForSeconds(NameOfAnimation.length);
if(DoubleTime){yield WaitForSeconds(NameOfAnimation.length);}
gameObject.GetComponent.<Animation>().Stop();
while (Point>0){
ArrayOfPieces[Point-1].GetComponent.<MeshRenderer>().enabled=false;
Point--;
}
StartObject.GetComponent.<MeshRenderer>().enabled=true;
Once=true;
}

}