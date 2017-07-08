using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {

	public float speed;
	private Rigidbody2D boltRB;
	public GameObject hitSplash;
	public float lifeTime;

	// Use this for initialization
	void Start () {
		boltRB = GetComponent<Rigidbody2D> ();
		boltRB.velocity = transform.right * speed;
		Invoke ("killSelf", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject target = other.gameObject;
		if (target.tag == "Enemy") {
			target.SendMessage ("takeDamage", 1.0f);
			target.SendMessage ("spawnHitParticles");
			Instantiate (hitSplash, transform.position, transform.rotation);
			Destroy (gameObject);
		}
		if (isTargetHittable(target)) {
			Instantiate (hitSplash, transform.position, transform.rotation);
			Destroy (gameObject);
		}
	}

	void killSelf(){
		Destroy (gameObject);
	}

	private bool isTargetHittable(GameObject target){
		bool isHittable = true;
		if (target.tag != "EnemyBolt")
			isHittable = false;
		if (target.tag != "TorpedoBay")
			isHittable = false;
		if (target.tag != "PlayerLaser")
			isHittable = false;
		if (target.tag != "EnemySpawner")
			isHittable = false;
		if (target.tag != "Shield")
			isHittable = false;
		if (target.tag != "pickup")
			isHittable = false;

		return isHittable;
	}

}
