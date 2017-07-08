using UnityEngine;
using System.Collections;

public class Cultist_StarController : MonoBehaviour {

	public float health;
	public GameObject deathExplosion;

	public float speed;
	public float rotateSpeed;
	public float distanceToShoot; //distance from player to activate shooting
	private float nextFire;
	public float fireRate;
	public GameObject bolt;
	public Transform leftShotSpawn;
	public Transform rightShotSpawn;
	public ParticleSystem hitParticles; //particles that are thrown out when hit

	//drops
	public GameObject coinPickup;
	public GameObject healthPickup;
	private bool hasCoin; //will drop coin on death?
	private bool hasHealth; //will drop health pickup on death?
	public int coinChance; //chance enemy will drop coin on death (bigger # = higher % chance). Must be 0-100
	public int healthChance; //chance enemy will drop coin on death (bigger # = higher % chance). Must be 0-100


	// Use this for initialization
	void Start () {
		int healthOrCoin = Random.Range (1, 3); //1 for health, 2 for coin
		int coinRoll = Random.Range (0, 100);
		int healthRoll = Random.Range (0, 100);

		if (coinRoll <= coinChance && healthOrCoin == 2)
			hasCoin = true;
		else
			hasCoin = false;

		if (healthRoll <= healthChance && healthOrCoin == 1)
			hasHealth = true;
		else
			hasHealth = false;

	}

	// Update is called once per frame
	void Update () {
		transform.position += transform.up * speed * Time.deltaTime * -1;
		rotateTowardsPlayer ();

		//shoot
		GameObject Player = GameObject.FindGameObjectWithTag ("Cockpit");
		float distanceToPlayer = distanceToShoot + 1; //mostly useless, just need to assign it to something
		if (Player != null)
			distanceToPlayer = Vector3.Distance (transform.position, Player.transform.position);

		if (Player != null && distanceToPlayer <= distanceToShoot) {
			if (Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Instantiate (bolt, leftShotSpawn.position, leftShotSpawn.rotation);
				Instantiate (bolt, rightShotSpawn.position, rightShotSpawn.rotation);
			}
		}

		if (health <= 0) {
			//Drop pickup if have one
			if (hasCoin)
				Instantiate (coinPickup, transform.position, transform.rotation);
			if (hasHealth)
				Instantiate (healthPickup, transform.position, transform.rotation);
			
			Instantiate (deathExplosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	public void takeDamage(float dmg)
	{
		health = health - dmg;
	}

	void rotateTowardsPlayer()
	{
		GameObject Player = GameObject.FindGameObjectWithTag ("Cockpit");
		if (Player != null) {
			Vector3 vectorToTarget = Player.transform.position - transform.position;
			float angle = (Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg) + 90;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp (transform.rotation, q, Time.deltaTime * rotateSpeed);
		}

	}

	public void spawnHitParticles(){
		Instantiate (hitParticles, transform.position, transform.rotation);
	}
}
