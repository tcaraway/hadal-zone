using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossRightClaw : MonoBehaviour {

    public float health;
    public GameObject crabBoss;
    public float rotateSpeed;
    public Transform shotSpawn;
    public ParticleSystem hitParticles;
    public GameObject deathExplosion;
    public GameObject laser;

    public Sprite rightClawNormal; //Sprite used normally
    public Sprite rightClawHit; //Sprite used when hit

    private float nextFire;
    public float fireRate;
    private bool readyToFire;
    public int maxBurstFireAmount; //max shots fired in a burst
    private int currentBurstShotCount; //how many shots have been fired in this burst?
    public float timeBetweenBursts;

    // Use this for initialization
    void Start () {
        readyToFire = true;
        currentBurstShotCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        checkDeath();
        rotate();
        if(readyToFire)
            shoot();
	}

    public void takeDamage(float dmg)
    {
        health = health - dmg;
        takeDamageVisual();
    }

    public void checkDeath()
    {
        if (health <= 0)
        {
            Instantiate(deathExplosion, transform.position, transform.rotation);
            killSelf();
        }
    }

    public void spawnHitParticles()
    {
        Instantiate(hitParticles, transform.position, transform.rotation);
    }


    public void rotate()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, -42.0f * Mathf.Sin(Time.time * rotateSpeed));
    }

    public void killSelf()
    {
        Destroy(gameObject);
    }

    private void takeDamageVisual()
    {
        GetComponent<SpriteRenderer>().sprite = rightClawHit;
        Invoke("normalClaw", 0.1f);
    }

    private void normalClaw()
    {
        GetComponent<SpriteRenderer>().sprite = rightClawNormal;
    }

    private void shoot()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(laser, shotSpawn.position, shotSpawn.rotation);
            currentBurstShotCount = currentBurstShotCount + 1;
        }
        if(currentBurstShotCount > maxBurstFireAmount)
        {
            disableBurst();
            Invoke("enableBurst", timeBetweenBursts);
        }
    }

    private void disableBurst()
    {
        if(readyToFire)
            readyToFire = false;
    }

    private void enableBurst()
    {
        if(!readyToFire)
            readyToFire = true;
        resetBurstAmount();
    }

    private void resetBurstAmount()
    {
        currentBurstShotCount = 0;
    }
}
