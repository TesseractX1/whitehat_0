#pragma strict
var sensitivity : float=200;
var speed : float=400;
var frontSpeed : float=1000;

function Update () {
	transform.eulerAngles.z=0;
	if(Input.GetMouseButton(1)){
		transform.Rotate(-Input.GetAxis("Mouse Y")*2*Time.fixedDeltaTime*sensitivity,Input.GetAxis("Mouse X")*2*Time.fixedDeltaTime*sensitivity,0);
	}
	if(Input.GetMouseButton(2)){
		transform.Translate(-Input.GetAxis("Mouse X")*2*Time.fixedDeltaTime*speed,-Input.GetAxis("Mouse Y")*2*Time.fixedDeltaTime*speed,0);
	}
	if(Input.GetKey(KeyCode.W)){
		transform.Translate(Vector3.forward*Time.fixedDeltaTime*frontSpeed);
	}
	if(Input.GetKey(KeyCode.S)){
		transform.Translate(Vector3.back*Time.fixedDeltaTime*frontSpeed);
	}
	if(Input.GetAxis("Mouse ScrollWheel")!=0){
		transform.Translate(Vector3.forward*Input.GetAxis("Mouse ScrollWheel")*Time.fixedDeltaTime*frontSpeed);
    }
}