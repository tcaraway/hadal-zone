using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject spawnee; //indiana
	public float amountToSpawn;
	public float spawnRate; // lower = higher rate
	private float nextSpawn;
	public bool spawning; //is this spawner currently able to spawn?
	public GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		spawning = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (spawning)
			spawn ();
	}
		
	public void spawn(){
		if (amountToSpawn > 0 && Time.time > nextSpawn) {
			nextSpawn = Time.time + spawnRate;
			Instantiate (spawnee, transform.position, transform.rotation);
			amountToSpawn--;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		GameObject target = other.gameObject;
		if (target.tag == "Cockpit" && amountToSpawn > 0) {
			spawning = true;
		}
	}
		
}
