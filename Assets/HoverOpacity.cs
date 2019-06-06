using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverOpacity : MonoBehaviour {
	public UnityEngine.UI.Image img;
	public float opacity = 0f;
	private bool hov = false;
	private bool clck = false;
	public float speed = 5f;
	// Update is called once per frame
	public void hover () {
		hov = true;
		//opacity = 0.4f;
	}
	public void offclick() {clck=false;}
	public void click(){
		//opacity=1f;
		clck=true;
	}
	public void off () {
		//opacity = 0f;
		hov = false;
		clck=false;
	}

	void Update () {

		Color color=img.color;
		color.a = opacity;
		img.color=color;
		if(hov && opacity<0.4f)
			opacity=opacity+Time.deltaTime*speed;
		if(!hov && opacity>0f)
			opacity=opacity-Time.deltaTime*speed;
		if(clck && opacity<1f)
			opacity=opacity+Time.deltaTime*speed;
	}

}
