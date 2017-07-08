using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public GameObject[] possibleQuads; //collection of all tiles
	public GameObject[] quadsToUse;    //quads that will be used this run
	public GameObject bossQuad;			//quad where boss spawns
	public GameObject playerSub;
	public int numQuads;             //number of quads to use
	public float startY;	//y position of first quad
	public float startZ;	//z position of first quad
	public float startX;	//x position of first quad
	public float offSety;	//how many units after quad to place next?
	private float bossQuadY; // y position the camera needs to stop at for the boss

	//UI Elements
	public GameObject leftWing; 
	public GameObject rightWing;
	public GameObject gameOver;
	public GameObject restart;
	public GameObject mainMenu;

	public GameObject cockpit;
	public GameObject rightThrust;
	public GameObject leftThrust;
	public GameObject torpedoBay;
	public GameObject coinText;

	public bool isGameOver;
	public int lives; 
	public int wallet; //how many triton coins player has total

	// Use this for initialization
	void Start () {
		isGameOver = false;
		gameOver.SetActive (false);
		restart.SetActive (false);
		mainMenu.SetActive (false);

		//quads' y is 35, so multiply that by # of quads to get bossQuadY
		bossQuadY = numQuads * 35;
		Camera.main.SendMessage ("setStopPosition", bossQuadY);

		quadsToUse = new GameObject[numQuads];
		for (int i = 0; i < numQuads; i++) {
			int randomindex = Random.Range (0, possibleQuads.Length);
			quadsToUse [i] = possibleQuads [randomindex];
		}

		placeQuads ();
		Vector3 playerSpawn = new Vector3 (0.0f, -5.0f, 9.0f);
		Instantiate (playerSub, playerSpawn, transform.rotation);

		wallet = 0;
		coinText = GameObject.FindGameObjectWithTag ("CoinText");
	}
	
	// Update is called once per frame
	void Update () {
		checkGameOver ();
	}

	void placeQuads(){
		float currentY = startY;
		for (int i = 0; i < numQuads; i++) {
			Vector3 quadPosition = new Vector3 (startX, currentY, startZ);
			Instantiate (quadsToUse [i], quadPosition, transform.rotation);
			currentY = currentY + offSety;
		}
		Vector3 bossPosition = new Vector3 (startX, currentY, startZ);
		Instantiate (bossQuad, bossPosition, transform.rotation);
	}


	public void addCoins(int num)
	{
		wallet = wallet + num;
		Text text = coinText.gameObject.GetComponent<Text> ();
		text.text = "" + wallet;
	}

	public void respawn(Vector3 respawnPosition){
		if (lives > 0) {
			Instantiate (playerSub, respawnPosition, transform.rotation);
			lives--;
		} else {
			isGameOver = true;
		}
	}

	public void checkGameOver(){
		if (isGameOver) {
			gameOver.SetActive (true);
			restart.SetActive (true);
			mainMenu.SetActive (true);
			Camera.main.SendMessage ("stopCamera");
		}
	}

	public void restartGame(){
		SceneManager.LoadScene ("Coral");
	}

	public void goToMainMenu(){
		SceneManager.LoadScene ("TitleScreen");
	}
		
}
