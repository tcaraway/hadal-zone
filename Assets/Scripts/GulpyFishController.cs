using UnityEngine;
using System.Collections;

public class GulpyFishController : MonoBehaviour {

	public Sprite offFish;
	public Sprite onFish;
	private bool lightOn;
	private SpriteRenderer anglerSprite;

	// Use this for initialization
	void Start () {
		lightOn = false;
		anglerSprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		Invoke ("blink", 2.0f);
	}

	void blink(){
		if (!lightOn) {
			anglerSprite.sprite = onFish;
			lightOn = true;
		} else {
			anglerSprite.sprite = offFish;
			lightOn = false;
		}
	}
}
