using UnityEngine;
using System.Collections;

public class FishCultist1_Controller : MonoBehaviour {
	
	public float health;
	public GameObject deathExplosion;
	public float speed;
	private float nextFire;
	public float fireRate;
	public GameObject bolt;
	public Transform shotSpawn1;
	public Transform shotSpawn2;
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
		checkDeath ();
		move ();
		shoot ();
	}

	public void takeDamage(float dmg){
		health = health - dmg;
	}

	private void move(){
		transform.position += transform.up * speed * Time.deltaTime * -1;
	}

	private void checkDeath(){
		if (health <= 0) {
			//Drop pickup if have one
			if (hasCoin)
				Instantiate (coinPickup, transform.position, transform.rotation);
			if (hasHealth)
				Instantiate (healthPickup, transform.position, transform.rotation);
			
			Instantiate (deathExplosion, transform.position, transform.rotation);
			GameObject.Destroy (gameObject);
		}
	}

	private void shoot(){
		if (Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (bolt, shotSpawn1.position, shotSpawn1.rotation);
			if (shotSpawn2 != null)
				Instantiate (bolt, shotSpawn2.position, shotSpawn2.rotation);
		}
	}

	public void spawnHitParticles(){
		Instantiate (hitParticles, transform.position, transform.rotation);
	}

}
