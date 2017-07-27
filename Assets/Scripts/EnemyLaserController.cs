using UnityEngine;
using System.Collections;

public class EnemyLaserController : MonoBehaviour {

	public float speed;
	public float dmg;
	private Rigidbody2D boltRB;
	public GameObject hitSplash;
	public float lifeTime;
    public bool isCrabboBoss; //BAD PRACTICE (should make new class for this...)

	// Use this for initialization
	void Start () {
		boltRB = GetComponent<Rigidbody2D> ();
        if(!isCrabboBoss)
		    boltRB.velocity = transform.right * speed;
        else
            boltRB.velocity = (transform.up * -1f) * speed;
        Invoke ("killSelf", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject target = other.gameObject;
		if (target.tag == "Cockpit") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "RightWing") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "LeftWing") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "TorpedoBay") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "Left Thruster") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else if (target.tag == "Right Thruster") {
			PlayerPartController partScript = target.GetComponent<PlayerPartController> ();
			Instantiate (hitSplash, transform.position, transform.rotation);
			partScript.takeDamage (dmg);
		} else {
		}

		if (target.tag != "PlayerLaser" && target.tag != "Enemy" && target.tag != "EnemyBolt" && target.tag != "EnemySpawner" && target.tag != "pickup" && target.tag != "CrabboSpawner") {
			Destroy (gameObject);
		}
	}

	void killSelf(){
		Destroy (gameObject);
	}
}
