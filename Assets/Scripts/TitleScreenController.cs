using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour {

	Camera mainCamera;
	public Vector3 characterSelectPosition;
	public bool isMovingToCharacterSelection;
	public bool isMovingToTitle;
	public Vector3	startingPosition;

	//sprites that show which piece is selected
	public GameObject leftWingLocation;
	public GameObject rightWingLocation;
	public GameObject thrusterLocation;
	public GameObject specialLocation;

	//sprites attached to center submarine
	public GameObject leftWingAttached;
	public GameObject rightWingAttached;
	public GameObject specialAttached;
	public GameObject thrusterAttached;

	//all the parts that can be / have been unlocked
	public Sprite[] leftWings;
	public Sprite[] rightWings;
	public Sprite[] specials;
	public Sprite[] thrusters;
	public Sprite[] questionMarks; //if a part hasn't been unlocked

	//current index of part selected, starting at 0
	private int currentLeft;
	private int currentRight;
	private int currentSpecial;
	private int currentThruster;

	public bool originalSubSprinning;

	// Use this for initialization
	void Start () {
		leftWingLocation.GetComponent<SpriteRenderer> ().sprite = leftWings [0];
		rightWingLocation.GetComponent<SpriteRenderer> ().sprite = rightWings [0];
		specialLocation.GetComponent<SpriteRenderer> ().sprite = specials [0];
		thrusterLocation.GetComponent<SpriteRenderer> ().sprite = thrusters [0];

		leftWingAttached.GetComponent<SpriteRenderer> ().sprite = leftWings [0];
		rightWingAttached.GetComponent<SpriteRenderer> ().sprite = rightWings [0];
		specialAttached.GetComponent<SpriteRenderer> ().sprite = specials [0];
		thrusterAttached.GetComponent<SpriteRenderer> ().sprite = thrusters [0];

		checkUnlockables (); //populate with question marks if haven't unlocked yet

		isMovingToCharacterSelection = false;
		mainCamera = Camera.main;
		mainCamera.transform.position = startingPosition;
		characterSelectPosition = new Vector3 (26.9f,1.0f,-10.0f);

		currentLeft = 0;
		currentRight = 0;
		currentSpecial = 0;
		currentThruster = 0;

	}
	
	// Update is called once per frame
	void Update () {
		if (isMovingToCharacterSelection)
			goToCharacterSelection ();
		if (isMovingToTitle)
			goToTitle ();
	}

	public void loadFirstlevel(){
		SceneManager.LoadScene ("Coral");
	}

	public void switchToCharacterOn(){
		isMovingToCharacterSelection = true;
		isMovingToTitle = false;
		originalSubSprinning = true;
	}

	public void switchToTitleOn(){
		isMovingToCharacterSelection = false;
		isMovingToTitle = true;
		originalSubSprinning = false;
		//originalSub.transform.rotation
	}
		
	public void goToCharacterSelection(){
		mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, characterSelectPosition, Time.deltaTime*2);
	}

	public void goToTitle(){
		mainCamera.transform.position = Vector3.Lerp (mainCamera.transform.position, startingPosition, Time.deltaTime*2);
	}


	//PROTOTYPE, DOESN'T CHECK FOR ANYTHING LOL
	public void checkUnlockables(){
		leftWings [1] = questionMarks [0];
		leftWings [2] = questionMarks [1];
		leftWings [3] = questionMarks [2];
		leftWings [4] = questionMarks [3];

		rightWings [1] = questionMarks [0];
		rightWings [2] = questionMarks [1];
		rightWings [3] = questionMarks [2];
		rightWings [4] = questionMarks [3];

		specials [1] = questionMarks [0];
		specials [2] = questionMarks [1];
		specials [3] = questionMarks [2];
		specials [4] = questionMarks [3];

		thrusters [1] = questionMarks [0];
		thrusters [2] = questionMarks [1];
		thrusters [3] = questionMarks [2];
		thrusters [4] = questionMarks [3];
	}

	//LEFT OFF HERE, NEED TO MAKE METHODS FOR SELECTING PARTS
	public void rightSelectRight(){
		currentRight++;
		if (currentRight > 4)
			currentRight = 0;
		rightWingLocation.GetComponent<SpriteRenderer> ().sprite = rightWings [currentRight];

	}
	public void rightSelectLeft(){
		currentRight--;
		if (currentRight < 0)
			currentRight = 4;
		rightWingLocation.GetComponent<SpriteRenderer> ().sprite = rightWings [currentRight];
	}

	public void leftSelectRight(){
		currentLeft++;
		if (currentLeft > 4)
			currentLeft = 0;
		leftWingLocation.GetComponent<SpriteRenderer> ().sprite = leftWings [currentLeft];
	}
	public void leftSelectLeft(){
		currentLeft--;
		if (currentLeft < 0)
			currentLeft = 4;
		leftWingLocation.GetComponent<SpriteRenderer> ().sprite = leftWings [currentLeft];
	}

	public void specialSelectLeft(){
		currentSpecial--;
		if (currentSpecial < 0)
			currentSpecial = 4;
		specialLocation.GetComponent<SpriteRenderer> ().sprite = specials [currentSpecial];
	}
	public void specialSelectRight(){
		currentSpecial++;
		if (currentSpecial > 4)
			currentSpecial = 0;
		specialLocation.GetComponent<SpriteRenderer> ().sprite = specials [currentSpecial];
	}

	public void thrusterSelectLeft(){
		currentThruster--;
		if (currentThruster < 0)
			currentThruster = 4;
		thrusterLocation.GetComponent<SpriteRenderer> ().sprite = thrusters [currentThruster];
	}
	public void thrusterSelectRight(){
		currentThruster++;
		if (currentThruster > 4)
			currentThruster = 0;
		thrusterLocation.GetComponent<SpriteRenderer> ().sprite = thrusters [currentThruster];
	}

}
