#pragma strict

var StopObject:GameObject;


function OnTouchDown(){Doing();}
function OnMouseDown(){Doing();}


function Doing(){
gameObject.transform.parent = StopObject.transform;
}