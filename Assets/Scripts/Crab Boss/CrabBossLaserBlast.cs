using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrabBossLaserBlast : MonoBehaviour {

    public float dmg;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        GameObject target = other.gameObject;
        if (target.tag == "Cockpit")
        {
            PlayerPartController partScript = target.GetComponent<PlayerPartController>();
            partScript.takeDamage(dmg);
        }
        if (target.tag == "RightWing")
        {
            PlayerPartController partScript = target.GetComponent<PlayerPartController>();
            partScript.takeDamage(dmg);
        }
        if (target.tag == "LeftWing")
        {
            PlayerPartController partScript = target.GetComponent<PlayerPartController>();
            partScript.takeDamage(dmg);
        }
        if (target.tag == "TorpedoBay")
        {
            PlayerPartController partScript = target.GetComponent<PlayerPartController>();
            partScript.takeDamage(dmg);
        }
        if (target.tag == "Left Thruster")
        {
            PlayerPartController partScript = target.GetComponent<PlayerPartController>();
            partScript.takeDamage(dmg);
        }
        if (target.tag == "Right Thruster")
        {
            PlayerPartController partScript = target.GetComponent<PlayerPartController>();
            partScript.takeDamage(dmg);
        }

    }
}
