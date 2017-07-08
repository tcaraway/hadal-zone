using UnityEngine;
using System.Collections;

public class HealthPickupController : MonoBehaviour {

	public Sprite sprite1;
	public Sprite sprite2;
	private bool spriteSwitcher;
	public GameObject player;

	// Use this for initialization
	void Start () {
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprite1;
		spriteSwitcher = false;
	}
	
	// Update is called once per frame
	void Update () {
		player = GameObject.FindGameObjectWithTag ("Player");
		flicker ();
	}


	void OnTriggerEnter2D(Collider2D other){
		GameObject collidee = other.gameObject;
		if (isTagPlayer(collidee.tag)) {
			player.SendMessage ("heal");
			Destroy (gameObject);
		}
	}

	//is this tag part of the player?
	bool isTagPlayer(string tag){
		if (tag == "Cockpit")
			return true;
		else if (tag == "RightWing")
			return true;
		else if (tag == "LeftWing")
			return true;
		else if (tag == "TorpedoBay")
			return true;
		else if (tag == "Left Thruster")
			return true;
		else if (tag == "Right Thruster")
			return true;
		else
			return false;
	}

	private void flicker(){
		if (!spriteSwitcher) {
			Invoke ("changeSprite2", 1.0f);
		} else {
			Invoke ("changeSprite1", 1.0f);
		}
	}

	private void changeSprite1(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprite1;
		spriteSwitcher = false;
	}

	private void changeSprite2(){
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprite2;
		spriteSwitcher = true;
	}
}
