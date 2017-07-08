using UnityEngine;
using System.Collections;

public class CameraScroller : MonoBehaviour {

	public float scrollSpeed;
	public float stopPosition; //y position that the camera needs to stop scrolling

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate((Vector3.up * (Time.deltaTime * scrollSpeed)));
		stopCameraAtBoss ();
	}

	public void setScrollSpeed(float newSpeed){
		scrollSpeed = newSpeed;
	}

	public void setStopPosition(float stopPos){
		stopPosition = stopPos;
	}

	public void stopCameraAtBoss(){
		if (transform.position.y >= stopPosition)
			scrollSpeed = 0;
	}

	public void stopCamera(){
		scrollSpeed = 0;
	}
}
