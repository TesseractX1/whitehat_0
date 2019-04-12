using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public float speed = 1f;
	private Camera camera;
	private Vector2 relativeMouse;
	public float edgeScrollingSpeed = 20;
	private Vector2 cameraScroll;

	void Start () {
		camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		//RMB Scrolling
		if(Input.GetMouseButton(2)){
			transform.Translate(-Input.GetAxis("Mouse X")*2*Time.fixedDeltaTime*speed*camera.orthographicSize,-Input.GetAxis("Mouse Y")*2*Time.fixedDeltaTime*speed*camera.orthographicSize,0);
		}

		//Edge Scrolling
		cameraScroll.x = 0; cameraScroll.y = 0;
		relativeMouse.x = Input.mousePosition.x/Screen.width;
		relativeMouse.y = Input.mousePosition.y/Screen.height;
		if(relativeMouse.x<0.05f) cameraScroll.x = (-0.05f+relativeMouse.x)*20;
		if(relativeMouse.y<0.1f) cameraScroll.y = (-0.1f+relativeMouse.y)*10;
		if(relativeMouse.x>0.95f) cameraScroll.x = (relativeMouse.x-0.95f)*20;
		if(relativeMouse.y>0.9f) cameraScroll.y = (relativeMouse.y-0.9f)*10;
		//relativeMouse=relativeMouse*20;
		cameraScroll = Vector2.ClampMagnitude(cameraScroll,1);
		print(cameraScroll);
		transform.Translate(cameraScroll*Time.fixedDeltaTime*edgeScrollingSpeed);

		//transform.Translate(Vector3.left*Time.fixedDeltaTime*(0.05f-relativeMouse.x)*edgeScrollingSpeed);

		camera.orthographicSize-=Input.GetAxis("Mouse ScrollWheel")*camera.orthographicSize;
		camera.orthographicSize=Mathf.Clamp(camera.orthographicSize,5,20);
		//camera.orthographicSize = Mathf.Clamp(camera.orthographicSize,20,5);
	}
}
