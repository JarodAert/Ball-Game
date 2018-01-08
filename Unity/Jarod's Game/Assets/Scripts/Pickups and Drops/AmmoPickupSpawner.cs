using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickupSpawner : MonoBehaviour {

    public GameObject ammoPickup;
    public GameObject grenadePickup;
    public Terrain terr;

    public float grenadeSpawnChance = 50f;
    private float nextTime = 0;
    public float spawnRate = 5;

    public float xConstrant = 45;
    public float zConstrant = 45;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
      
            if (Time.time > nextTime)
            {

            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 0.5f, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
                placement = new Vector3(placement.x, placement.y + offset, placement.z);
                Instantiate(ammoPickup, placement, Quaternion.identity);


                float rand = Random.Range(0, 100);
                if (rand < grenadeSpawnChance)
                {
                placement = placement + new Vector3(Random.Range(0, 10), 0, Random.Range(0, 10));
                offset = terr.SampleHeight(placement);
                placement = new Vector3(placement.x, placement.y + offset, placement.z);
                Instantiate(grenadePickup, placement + new Vector3(Random.Range(0, 10),0, Random.Range(0, 10)), Quaternion.identity);
                }
                nextTime = Time.time + spawnRate;
            }
        }
}
