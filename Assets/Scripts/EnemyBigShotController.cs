using UnityEngine;
using System.Collections;

public class EnemyBigShotController : MonoBehaviour {

	public float speed;
	public float dmg;
	public float lifeTime;
	public Rigidbody2D boltRB;

	// Use this for initialization
	void Start () {
		boltRB = GetComponent<Rigidbody2D> ();
		boltRB.velocity = transform.right * speed;
		Invoke ("killSelf", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void killSelf(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject target = other.gameObject;
		if (target.tag == "Cockpit") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			//Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "RightWing") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			//Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "LeftWing") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			//Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "TorpedoBay") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			//Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "Left Thruster") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			//Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "Right Thruster") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			//Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else {
		}

		if (target.tag != "PlayerLaser" && target.tag != "Enemy" && target.tag != "EnemyBolt" && target.tag != "EnemySpawner" && target.tag != "pickup") {
			Destroy (gameObject);
		}
	}
}
