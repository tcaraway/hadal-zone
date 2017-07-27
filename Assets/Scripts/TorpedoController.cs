using UnityEngine;
using System.Collections;

public class TorpedoController : MonoBehaviour {

	public float speed;
	public float firstDmg; //damage to first thing hit
	public float secondDmg; //damage to everything in explosion other than first
	public float lifeTime;
	Rigidbody2D torpedoRB;
	public GameObject hitSplash;
	public float explosionRadius;

	// Use this for initialization
	void Start () {
		torpedoRB = GetComponent<Rigidbody2D> ();
		torpedoRB.velocity = transform.right * speed;
		Invoke ("killSelf", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void killSelf(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject target = other.gameObject;
		if (target.tag == "Enemy") {
			target.SendMessage ("takeDamage", firstDmg);
			Collider2D[] colliders = Physics2D.OverlapCircleAll (transform.position, explosionRadius);
			foreach(Collider2D coll in colliders)
			{
				GameObject hitObject = coll.gameObject;
				if (hitObject.tag == "Enemy")
					hitObject.SendMessage ("takeDamage", secondDmg);
			}
		}
		if (target.tag != "EnemyBolt" && target.tag != "PlayerLaser" && target.tag != "EnemySpawner" && target.tag != "CrabboSpawner") {
			Instantiate (hitSplash, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}
}
