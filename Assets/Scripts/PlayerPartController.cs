using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerPartController : MonoBehaviour {
	public float health;
	public float maxHealth;
	public GameObject uiComponent; //corresponding UI image
	private Image uiImage;
	public int partIdentifier; //1 cockpit, 2 torpedobay, 3 left wing, 4 right wing, 5 thrusters
	public Sprite noDamage;
	public Sprite someDamage;
	public Sprite destroyed;
	public bool isDead; //is this part destroyed?

	public ParticleSystem hitParticles; //on hit
	public ParticleSystem deathParticles; //on destruction

	public float fallSpeed; //how fast the part sinks after detaching
	public Vector3 fallDirection;
	public GameObject bubbleSystem; //particle system to use when sinking
	private bool hasMadeBubbles; //to prevent a gazillion bubble systems from instantiating
	private bool hasMadeDeathExplosion; //to prevent a brazillian death explosions...

	// Use this for initialization
	void Start () {
		hasMadeBubbles = false;
		hasMadeDeathExplosion = false;
		isDead = false;
		fallDirection = new Vector3 (0, 0, 1.0f);
		fallSpeed = 3.0f;

		if (partIdentifier == 1)
			uiComponent = GameObject.FindGameObjectWithTag ("cockpitui");
		else if (partIdentifier == 2)
			uiComponent = GameObject.FindGameObjectWithTag ("torpedobayui");
		else if (partIdentifier == 3)
			uiComponent = GameObject.FindGameObjectWithTag ("leftwingui");
		else if (partIdentifier == 4)
			uiComponent = GameObject.FindGameObjectWithTag ("rightwingui");
		else if (partIdentifier == 5)
			uiComponent = GameObject.FindGameObjectWithTag ("thrustui");
		else
			uiComponent = null;
		
		uiImage = uiComponent.gameObject.GetComponent<Image> ();
		uiImage.sprite = noDamage;
	}
	
	// Update is called once per frame
	void Update () {
		updateUI ();
		if (health <= 0) {
			isDead = true;
			if (!hasMadeDeathExplosion) {
				Instantiate (deathParticles, transform.position, transform.rotation);
				hasMadeDeathExplosion = true;
			}
			killSelf ();
			sink ();
		}

	}

	public void killSelf(){
		if (partIdentifier != 1) {
			gameObject.transform.parent = null;
			if (!hasMadeBubbles) {
				hasMadeBubbles = true;
				GameObject bubbles = (GameObject)Instantiate (bubbleSystem, transform.position, transform.rotation);
				bubbles.transform.parent = gameObject.transform;
				bubbles.transform.eulerAngles = new Vector3 (180, 0, 0);
			}
		}
		else {
			GameObject.Destroy (gameObject);
		}
	}

	public void takeDamage(float dmg){
		health = health - dmg;
		Instantiate (hitParticles, transform.position, transform.rotation);
		if (health > 0 && health < maxHealth)
			uiImage.sprite = someDamage;
	}

	public void updateUI(){
		if (health > 0 && health < maxHealth)
			uiImage.sprite = someDamage;
		else if (health <= 0)
			uiImage.sprite = destroyed;
		else
			uiImage.sprite = noDamage;

	}

	public bool getIsDead(){
		return isDead;
	}

	private void sink(){
		transform.position += fallDirection * Time.deltaTime * fallSpeed;
	}

	public void heal(){
		if (gameObject.transform.parent != null) {
			health = maxHealth;
		}
	}
}
