using UnityEngine;
using System.Collections;

public class ParticleEnder : MonoBehaviour {

	public float lifeTime;

	// Use this for initialization
	void Start () {
		Invoke ("killSelf", lifeTime);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void killSelf(){
		Destroy (gameObject);
	}
}
