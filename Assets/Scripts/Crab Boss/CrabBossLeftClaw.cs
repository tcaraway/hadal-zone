using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossLeftClaw : MonoBehaviour
{

    public float health;
    public GameObject crabBoss;
    public float rotateSpeed;
    public ParticleSystem hitParticles;
    public GameObject deathExplosion;
    public Sprite leftClawNormal; //Sprite used normally
    public Sprite leftClawHit; //Sprite used when hit

    //laser components
    public GameObject laserBlast;
    public BoxCollider2D laserCollider;
    public Sprite laserChargingSprite;
    public Sprite laserActiveSprite;

    public float chargingTime; //how many seconds spent charging
    public float activeTime; //how many seconds spent w/ active laser
    public float inactiveTime; //how many second spent w/ inactive laser
    private bool isCharging;
    private bool isActive;
    private bool isInactive;

    public float dmg;

    // Use this for initialization
    void Start()
    {
        isCharging = false;
        isActive = false;
        isInactive = true;
    }

    // Update is called once per frame
    void Update()
    {
        checkDeath();
        rotate();
        checkState();
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
        transform.rotation = Quaternion.Euler(0f, 0f, -42.0f * Mathf.Sin(Time.time * rotateSpeed) - 45.0f);
    }

    public void killSelf()
    {
        Destroy(gameObject);
    }

    private void takeDamageVisual()
    {
        GetComponent<SpriteRenderer>().sprite = leftClawHit;
        Invoke("normalClaw", 0.1f);
    }

    private void normalClaw()
    {
        GetComponent<SpriteRenderer>().sprite = leftClawNormal;
    }


    private void laserInactive()
    {
        laserBlast.GetComponent<SpriteRenderer>().sprite = null;
        laserCollider.enabled = false;
    }

    private void laserCharging()
    {
        laserBlast.GetComponent<SpriteRenderer>().sprite = laserChargingSprite;
        laserCollider.enabled = false;
    }

    private void laserFiring()
    {
        laserBlast.GetComponent<SpriteRenderer>().sprite = laserActiveSprite;
        laserCollider.enabled = true;
    }

    private void laserLoop()
    {
        Invoke("laserCharging", inactiveTime);
        Invoke("laserFiring", chargingTime);
        Invoke("laserInactive", activeTime);
    }

    private void checkState()
    {
        Sprite currentSprite = laserBlast.GetComponent<SpriteRenderer>().sprite;
        if (currentSprite == null || currentSprite == laserChargingSprite)
        {
            laserCollider.enabled = false;
        }
        else
        {
            laserCollider.enabled = true;
        }
    }


   
}
