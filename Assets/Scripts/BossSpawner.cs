using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {

    public GameObject spawnee;
    public GameObject player;
    public float delayToSpawn; //when player is detected, how much of a wait until spawn?
    public bool spawning;
    public bool bossDead;
	public float distanceToLockScreen; //how far spawner from player to start locking player movement for boss fight?

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spawning = false;
        bossDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
		manageSpawning ();
	}

    public void spawn()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
    }

//    void OnTriggerEnter2D(Collider2D other)
//    {
//        GameObject target = other.gameObject;
//        if (target.tag == "Cockpit" && !spawning)
//        {
//            spawning = true;
//            Invoke("spawn", delayToSpawn);
//        }
//		if (bossDead)
//			bossIsDeadToPlayer ();
//		else {
//			bossInRangeToPlayer ();
//			print ("in range");
//		}
//    }

    private void bossInRangeToPlayer()
    {
        if(player != null)
            player.SendMessage("inBossRange");
    }
    private void bossIsDeadToPlayer()
    {
        if (player != null)
            player.SendMessage("bossIsDead");
    }

    public void switchBossDead()
    {
        bossDead = true;
    }

	private float getDistanceToPlayer(GameObject player){
		float distance = Vector3.Distance (transform.position, player.transform.position);
		return distance;
	}

	private bool isPlayerInRange(GameObject player){
		float distance = getDistanceToPlayer (player);
		print (distance);
		if (distance <= distanceToLockScreen)
			return true;
		else
			return false;
	}

	private void manageSpawning(){
		if (isPlayerInRange (player) && !spawning) {
			spawning = true;
			Invoke("spawn", delayToSpawn);
		}
		if (bossDead)
			bossIsDeadToPlayer ();
		if (!bossDead && isPlayerInRange(player)) {
			bossInRangeToPlayer ();

		}
	}
}
