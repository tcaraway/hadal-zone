using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossEyes : MonoBehaviour {

    //THIS IS ALL FUCKED UP. EYE ONLY BECOMES ANGRY IF BOTH CLAWS DIE FIRST, AND EVEN THEN ONLY THE SECOND EYE BECOMES ANGRY

    public float health;
    public float secondPhaseMaxHealth;
    public GameObject crabBoss;
    public Transform shotSpawn;
    public ParticleSystem hitParticles;
    public GameObject deathExplosion;
    public GameObject laser;
    public Sprite angryEyeSprite; //change to this if second phase has started
    public Sprite deadEyeSprite; //change to this if dead
    public bool isDead; //is eye inactive?
    public GameObject leftClaw;
    public GameObject rightClaw;
    public GameObject otherEye;

    public Sprite eyeNormal; //Sprite used normally (either original, or angry)
    public Sprite eyeHit; //Sprite used when hit;

    public float shotRotationSpeed;
    private float nextFire;
    public float fireRate;


    // Use this for initialization
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
        shoot();
        rotate();
    }

    public void takeDamage(float dmg)
    {
        if (!isDead)
        {
            health = health - dmg;
            takeDamageVisual();
        }
    }

    public void checkDeath()
    {
        if(health <= 0)
        {
            blackenEye();
            isDead = true;
        }
    }

    public void spawnHitParticles()
    {
        Instantiate(hitParticles, transform.position, transform.rotation);
    }

    public void killSelf()
    {
        Destroy(gameObject);
    }

    public void blackenEye()
    {
        GetComponent<SpriteRenderer>().sprite = deadEyeSprite;
    }

    public void angryEye()
    {
        GetComponent<SpriteRenderer>().sprite = angryEyeSprite;
    }

    public void normalEye()
    {
        GetComponent<SpriteRenderer>().sprite = eyeNormal;
    }

    //checks to see if other sprite is dead
    public bool isOtherEyeDead()
    {
        Sprite otherDeadSprite = otherEye.GetComponent<CrabBossEyes>().getDeadSprite();
        Sprite otherCurrentSprite = otherEye.GetComponent<CrabBossEyes>().getCurrentSprite();
        if (otherCurrentSprite == otherDeadSprite)
            return true;
        else
            return false;
  
    }

    public Sprite getCurrentSprite()
    {
        Sprite current = GetComponent<SpriteRenderer>().sprite;
        return current;
    }

    public Sprite getDeadSprite()
    {
        return deadEyeSprite;
    }

    public bool getIsDead()
    {
        return isDead;
    }

    //called after first death, second phase of fight
    public void secondPhase()
    {
        eyeNormal = angryEyeSprite;
        health = secondPhaseMaxHealth;
        isDead = false;
        angryEye();
    }

    public void takeDamageVisual()
    {
        GetComponent<SpriteRenderer>().sprite = eyeHit;
        Invoke("normalEye", 0.1f);
    }

    public void rotate()
    {
        shotSpawn.transform.rotation = Quaternion.Euler(0f, 0f, -42.0f * Mathf.Sin(Time.time * shotRotationSpeed));
    }

    private void shoot()
    {
        if (Time.time > nextFire && !isDead)
        {
            nextFire = Time.time + fireRate;
            Instantiate(laser, shotSpawn.position, shotSpawn.rotation);
        }
    }
}
