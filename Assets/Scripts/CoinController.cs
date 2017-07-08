using UnityEngine;
using System.Collections;

public class CoinController : MonoBehaviour {

	private GameObject gameController;
	public Sprite sprite1;
	public Sprite sprite2;
	private bool spriteSwitcher;

	// Use this for initialization
	void Start () {
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprite1;
		spriteSwitcher = false;
	}
	
	// Update is called once per frame
	void Update () {
		flicker ();
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject collidee = other.gameObject;
		if (isTagPlayer(collidee.tag)) {
			gameController.SendMessage ("addCoins", 1);
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
