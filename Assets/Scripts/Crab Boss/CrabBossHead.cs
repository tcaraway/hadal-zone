using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossHead : MonoBehaviour {

    public GameObject leftClaw;
    public GameObject rightClaw;
    public GameObject leftEye;
    public GameObject rightEye;

    private int phase; //1 or 2

    public Sprite angryHead; //Sprite used in phase 2
    public Sprite normalHead; //Sprite used in phase 1
    public Sprite angryHeadHit; //Sprite used in phase 2 when hit
    public Sprite normalHeadHit; //Sprite used in phase 1 when hit

    public int health;
    public ParticleSystem hitParticles;
    public GameObject deathExplosion;

	// Use this for initialization
	void Start () {
        phase = 1;
	}
	
	// Update is called once per frame
	void Update () {
        changePhase();
        checkDeath();
	}

    private bool checkParts()
    {
        bool partsDead = false;
        if(leftEye.GetComponent<CrabBossEyes>().getIsDead() && rightEye.GetComponent<CrabBossEyes>().getIsDead() && leftClaw == null && rightClaw == null)
        {
            partsDead = true;
        }
        return partsDead;
    }

    private void checkDeath()
    {
        if(health <= 0)
        {
            Instantiate(deathExplosion, transform.position, transform.rotation);
            Destroy(leftEye.gameObject);
            Destroy(rightEye.gameObject);
            Destroy(gameObject);
        }
    }
    private void changePhase()
    {
        if(checkParts() && phase == 1)
        {
            phase = 2;
            leftEye.GetComponent<CrabBossEyes>().secondPhase();
            rightEye.GetComponent<CrabBossEyes>().secondPhase();
            GetComponent<SpriteRenderer>().sprite = angryHead;
        }
    }

    public void spawnHitParticles()
    {
        if (checkParts() && phase == 2)
            Instantiate(hitParticles, transform.position, transform.rotation);
    }


    public void takeDamage(int dmg)
    {
        if (checkParts() && phase == 2)
        {
            health = health - dmg;
            takeDamageVisual();
        }
    }

    private void takeDamageVisual()
    {
        if (phase == 1)
        {
            GetComponent<SpriteRenderer>().sprite = normalHeadHit;
            Invoke("normalHeadChange", 0.1f);
        }
        if(phase == 2)
        {
            GetComponent<SpriteRenderer>().sprite = angryHeadHit;
            Invoke("angryHeadChange", 0.1f);
        }
    }

    private void normalHeadChange()
    {
        GetComponent<SpriteRenderer>().sprite = normalHead;
    }

    private void angryHeadChange()
    {
        GetComponent<SpriteRenderer>().sprite = angryHead;
    }
}
