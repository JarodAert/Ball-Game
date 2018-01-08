using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionKiller : MonoBehaviour {

	// Makes sure the explosion objects destory themselves after 5 seconds so they dont take up resources
	void Start () {
        Destroy(this.gameObject, 5);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
