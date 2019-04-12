using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
	public float speed = 1f;
	private Camera camera;

	void Start () {
		camera = GetComponent<Camera>();
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButton(2)){
			transform.Translate(-Input.GetAxis("Mouse X")*2*Time.fixedDeltaTime*speed*camera.orthographicSize,-Input.GetAxis("Mouse Y")*2*Time.fixedDeltaTime*speed*camera.orthographicSize,0);
		}
		camera.orthographicSize-=Input.GetAxis("Mouse ScrollWheel")*camera.orthographicSize;
		camera.orthographicSize=Mathf.Clamp(camera.orthographicSize,5,20);
		//camera.orthographicSize = Mathf.Clamp(camera.orthographicSize,20,5);
	}
}
