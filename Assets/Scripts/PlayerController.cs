using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float originalSpeed;
	public float speed;
	public float dashSpeed;
	public float slowerSpeed1;
	public float rotateSpeed;
	public float xMin, xMax, yMin, yMax, zClamp; //values of boundary of player movement
	public float maxShield;
	public float shieldRechargeRate;
	public float shieldDepleteRate;
	private float currentShield;
	private bool shieldCooldown; //is the shield cooling down? once shield hits 0, start cooldown phase
	public Sprite[] shieldSprites;
	private GameObject shieldUI;
	private Image shieldImage;

	public ParticleSystem deathExplosion;

	public GameObject thruster;
	public GameObject leftWing;
	public GameObject rightWing;
	public GameObject torpedoBay;
	public GameObject body;
	public GameObject shield;

	public GameObject prefabThruster;
	public GameObject prefabLeftWing;
	public GameObject prefabRightWing;
	public GameObject prefabSpecial;

	public Sprite thrusterIdle;
	public Sprite leftGunIdle;
	public Sprite rightGunIdle;
	public Sprite thrusterMoving;
	public Sprite leftGunFiring;
	public Sprite rightGunFiring;
	public Sprite cockpitIdle;
	public Sprite cockpitShielding;

	public Transform leftMainShotSpawn;
	public Transform rightMainShotSpawn;
	public Transform torpedoSpawn;
	public Transform alternateShotSpawn1;
	public Transform alternateShotSpawn2;

	public GameObject torpedo;
	public GameObject bolt;
	public float originalFireRate;
	public float fireRate;
	public float alternateFireRate;
	public float torpedoFireRate;
	private float nextFire;
	private float torpedoNextFire;

	private bool shieldActive;

	public CameraScroller cameraScript;
	public Camera mainCamera;

	public GameObject gameController;
	public GameController gameScript;

	public GameObject torpedoUI;
	public Sprite torpedoWaiting;
	public Sprite torpedoReady;

	public Transform leftWingPosition;
	public Transform rightWingPosition;
	public Transform specialPosition;
	public Transform thrusterPosition;


	// Use this for initialization
	void Start () 
	{
		gameController = GameObject.FindGameObjectWithTag ("GameController");
		gameScript = gameController.GetComponent<GameController> ();
		originalFireRate = fireRate;
		originalSpeed = speed;
		shieldActive = false;
		mainCamera = Camera.main;
		cameraScript = mainCamera.GetComponent<CameraScroller> ();

		torpedoUI = GameObject.FindGameObjectWithTag ("torpedoui");
		torpedoUI.gameObject.GetComponent<Image> ().sprite = torpedoReady;

		shieldUI = GameObject.FindGameObjectWithTag ("shieldui");
		shieldImage = shieldUI.gameObject.GetComponent<Image> ();
		shield.SetActive (false);
		currentShield = maxShield;
		shieldCooldown = false;
	}

	
	// Update is called once per frame
	void Update () 
	{
		checkDeath ();
		checkWings ();
		checkThrusters ();
		rotateTorwardsMouse();
		thrusterAnimation ();
		shoot ();
		updateShieldUI ();
		shieldUp ();
		checkParts();
		updateTorpedoUI ();
		shootTorpedo ();
		clampMovement ();
	}

	void FixedUpdate()
	{
		playerMove ();
	}


	//Rotate ship towards mouse
	void rotateTorwardsMouse()
	{
		Vector2 mouse = Input.mousePosition;
		Vector2 screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);
		Vector2 offset = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
		float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
		Quaternion targetRotation = Quaternion.Euler (0, 0, angle);
		GetComponent<Transform> ().rotation = Quaternion.Slerp (GetComponent<Transform> ().rotation, targetRotation, rotateSpeed * Time.deltaTime);
	}


	//change wing sprite to idle
	void changeGunsIdle()
	{
		if(rightWing != null)
			rightWing.GetComponent<SpriteRenderer> ().sprite = rightGunIdle;
		if(leftWing != null)
			leftWing.GetComponent<SpriteRenderer> ().sprite = leftGunIdle;
	}


	//if cockpit is destroyed, player is destroyed
	void checkDeath()
	{
		GameObject cockPit = GameObject.FindGameObjectWithTag("Cockpit");
		if (cockPit == null) {
			Instantiate (deathExplosion, transform.position, transform.rotation);
			Vector3 respawnPos = new Vector3(mainCamera.transform.position.x,mainCamera.transform.position.y,9.0f);
			gameController.SendMessage ("respawn", respawnPos);
			GameObject.Destroy (gameObject);
		}
	}


	//if both wings destroyed, lower fire-rate
	void checkWings()
	{
		if (rightWing.transform.parent == null && leftWing.transform.parent == null)
			fireRate = alternateFireRate;
		else
			fireRate = originalFireRate;
	}


	//checks if part is destroyed, if so set sprite to idle sprite
	void checkParts()
	{
		if(rightWing.transform.parent == null)
			rightWing.GetComponent<SpriteRenderer> ().sprite = rightGunIdle;
		if (leftWing.transform.parent == null)
			leftWing.GetComponent<SpriteRenderer> ().sprite = leftGunIdle;
		if (thruster.transform.parent == null)
			thruster.GetComponent<SpriteRenderer> ().sprite = thrusterIdle;
	}


	//missing thrusters? lower speed
	void checkThrusters()
	{
		if (thruster.transform.parent == null)
			speed = slowerSpeed1;
		else
			speed = originalSpeed;
		
	}


	//player movement
	void playerMove()
	{
		float moveX = Input.GetAxis("Horizontal");
		float moveY = Input.GetAxis ("Vertical");
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveX * speed, moveY * speed);
	}
		

	//handles thruster sprite changes
	void thrusterAnimation()
	{
		Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"),0);
		if (movement != Vector3.zero) {
			if (thruster.transform.parent != null)
				thruster.GetComponent<SpriteRenderer> ().sprite = thrusterMoving;
		} else {
			if (thruster.transform.parent != null)
				thruster.GetComponent<SpriteRenderer> ().sprite = thrusterIdle;
		}
	}


	//player shooting
	//if both wings are destroyed, switch to alternate firing
	void shoot()
	{
		if (Input.GetButton ("Fire1") && Time.time > nextFire && !shieldActive) {
			nextFire = Time.time + fireRate;

			if (rightWing.transform.parent != null) {
				rightWing.GetComponent<SpriteRenderer> ().sprite = rightGunFiring;
				Instantiate (bolt, rightMainShotSpawn.position, rightMainShotSpawn.rotation);
			}
			if (leftWing.transform.parent != null) {
				leftWing.GetComponent<SpriteRenderer> ().sprite = leftGunFiring;
				Instantiate (bolt, leftMainShotSpawn.position, leftMainShotSpawn.rotation);
			}
			if (leftWing.transform.parent == null && rightWing.transform.parent == null) {
				Instantiate (bolt, alternateShotSpawn1.position, alternateShotSpawn1.rotation);
				Instantiate (bolt, alternateShotSpawn2.position, alternateShotSpawn2.rotation);
			}
			Invoke ("changeGunsIdle", fireRate);
		}
	}
		

	//handles updates to torpedo sprite in UI
	void updateTorpedoUI()
	{
		//torpedoUI
		if(Time.time > torpedoNextFire && torpedoBay.transform.parent != null)
			torpedoUI.gameObject.GetComponent<Image> ().sprite = torpedoReady;
		if(torpedoBay.transform.parent == null)
			torpedoUI.gameObject.GetComponent<Image> ().sprite = torpedoWaiting;
	}


	//shoots torpedo if available
	void shootTorpedo()
	{
		//torpedo
		if (Input.GetButton ("Fire2") && Time.time > torpedoNextFire && !shieldActive) {
			torpedoNextFire = Time.time + torpedoFireRate;

			if (torpedoBay.transform.parent != null) {
				Instantiate (torpedo, torpedoSpawn.position, torpedoSpawn.rotation);
				torpedoUI.gameObject.GetComponent<Image> ().sprite = torpedoWaiting;
			}
		}
	}


	//updates boundaries of player movement based on camera scroll speed
	void clampMovement()
	{
		yMin = mainCamera.transform.position.y - 10.0f;
		yMax = mainCamera.transform.position.y + 10.0f;
		transform.position = new Vector3 (Mathf.Clamp(transform.position.x,xMin,xMax), Mathf.Clamp(transform.position.y,yMin,yMax),zClamp);
	}

	void shieldUp()
	{
		if (Input.GetKey ("space") && currentShield > 0 && !shieldCooldown) {
			body.GetComponent<SpriteRenderer> ().sprite = cockpitShielding;
			shield.SetActive (true);
			shieldActive = true;
			depleteShield ();
		} else {
            shieldActive = false;
			if (body != null) {
				body.GetComponent<SpriteRenderer> ().sprite = cockpitIdle;
				shield.SetActive (false);
				shieldActive = false;
				rechargeShield ();
			}
		}
	}

	void depleteShield()
	{
		currentShield = currentShield - shieldDepleteRate;
		if (currentShield < 0 && !shieldCooldown) {
			currentShield = 0;
			shieldCooldown = true;
		}
	}

	void rechargeShield()
	{
		currentShield = currentShield + shieldRechargeRate;
		if (currentShield > maxShield)
			currentShield = maxShield;
		if (currentShield == maxShield && shieldCooldown)
			shieldCooldown = false;
	}
		

	void updateShieldUI(){
		if (!shieldCooldown) {
			if (currentShield >= 0.9)
				shieldImage.sprite = shieldSprites [0];
			else if (currentShield >= 0.8 && currentShield < 0.9)
				shieldImage.sprite = shieldSprites [1];
			else if (currentShield >= 0.7 && currentShield < 0.8)
				shieldImage.sprite = shieldSprites [2];
			else if (currentShield >= 0.6 && currentShield < 0.7)
				shieldImage.sprite = shieldSprites [3];
			else if (currentShield >= 0.5 && currentShield < 0.6)
				shieldImage.sprite = shieldSprites [4];
			else if (currentShield >= 0.4 && currentShield < 0.5)
				shieldImage.sprite = shieldSprites [5];
			else if (currentShield >= 0.3 && currentShield < 0.4)
				shieldImage.sprite = shieldSprites [6];
			else if (currentShield >= 0.2 && currentShield < 0.3)
				shieldImage.sprite = shieldSprites [7];
			else if (currentShield >= 0.1 && currentShield < 0.2)
				shieldImage.sprite = shieldSprites [8];
			else 
				shieldImage.sprite = shieldSprites[9];
		} else {
			if(currentShield >= 0  && currentShield < 0.1)
				shieldImage.sprite = shieldSprites[19];
			else if(currentShield >= 0.1 && currentShield < 0.2)
				shieldImage.sprite = shieldSprites[18];
			else if(currentShield >= 0.2 && currentShield < 0.3)
				shieldImage.sprite = shieldSprites[17];
			else if(currentShield >= 0.3 && currentShield < 0.4)
				shieldImage.sprite = shieldSprites[16];
			else if(currentShield >= 0.4 && currentShield < 0.5)
				shieldImage.sprite = shieldSprites[15];
			else if(currentShield >= 0.5 && currentShield < 0.6)
				shieldImage.sprite = shieldSprites[14];
			else if(currentShield >= 0.6 && currentShield < 0.7)
				shieldImage.sprite = shieldSprites[13];
			else if(currentShield >= 0.7 && currentShield < 0.8)
				shieldImage.sprite = shieldSprites[12];
			else if(currentShield >= 0.8 && currentShield < 0.9)
				shieldImage.sprite = shieldSprites[11];
			else 
				shieldImage.sprite = shieldSprites[10];
		}
	}


	public void heal(){
		if (rightWing.transform.parent == null) {
			rightWing = (GameObject)Instantiate (prefabRightWing, rightWingPosition.position, transform.rotation);
			rightWing.transform.parent = gameObject.transform;
		}
		if (leftWing.transform.parent == null) {
			leftWing = (GameObject)Instantiate (prefabLeftWing, leftWingPosition.position, transform.rotation);
			leftWing.transform.parent = gameObject.transform;
		}
		if (thruster.transform.parent == null) {
			thruster = (GameObject)Instantiate (prefabThruster, thrusterPosition.position, transform.rotation);
			thruster.transform.parent = gameObject.transform;
		}
		if (torpedoBay.transform.parent == null) {
			torpedoBay = (GameObject)Instantiate (prefabSpecial, specialPosition.position, transform.rotation);
			torpedoBay.transform.parent = gameObject.transform;
		}

		rightWing.SendMessage ("heal");
		leftWing.SendMessage ("heal");
		torpedoBay.SendMessage ("heal");
		thruster.SendMessage ("heal");
		body.SendMessage ("heal");
	}

    public bool getShieldActive()
    {
        return shieldActive;
    }
}
