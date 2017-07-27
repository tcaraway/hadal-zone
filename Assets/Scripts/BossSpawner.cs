using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour {

    public GameObject spawnee;
    public GameObject player;
    public float delayToSpawn; //when player is detected, how much of a wait until spawn?
    public bool spawning;
    public bool bossDead;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        spawning = false;
        bossDead = false;
    }
	
	// Update is called once per frame
	void Update () {
        player = GameObject.FindGameObjectWithTag("Player");
	}

    public void spawn()
    {
        Instantiate(spawnee, transform.position, transform.rotation);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject;
        if (target.tag == "Cockpit" && !spawning)
        {
            spawning = true;
            Invoke("spawn", delayToSpawn);
        }
        if (bossDead)
            bossIsDeadToPlayer();
        else
            bossInRangeToPlayer();
    }

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
}
