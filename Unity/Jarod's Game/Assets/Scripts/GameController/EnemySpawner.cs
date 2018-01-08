using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour {

    private GameObject GameController;
    public GameObject enemy;
    public GameObject bigEnemy;
    public GameObject fastEnemy;
    public Terrain terr;

    private float nextTime=0;
    public float spawnRate=3;
    public float maxEnemies=50;
    public float fastEnemyStart=20;
    public float fastEnemyDouble=50;
    public float bigEnemyStart=40;
    public float bigEnemyDouble=75;
    public float xConstrant = 45;
    public float zConstrant = 45;

    // Use this for initialization
    void Start () {
        GameController = GameObject.FindGameObjectWithTag("GameController");
    }
	
	// Update is called once per frame
    //Here a new enemy object is randomly placed on the map every 3 seconds
    //After a set number of kills there is a chance it will spawn fast and big enemies 
	void Update () {
        if (Time.time>nextTime && GameController.GetComponent<GameController>().activeEnemies<maxEnemies&&GameController.GetComponent<GameController>().EndGame==false)
        {

            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 1, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
            placement = new Vector3(placement.x, placement.y + offset, placement.z);
            Instantiate(enemy, placement, Quaternion.identity);
            GameController.GetComponent<GameController>().activeEnemies++;
            nextTime = Time.time + spawnRate;

            //Check to see if there are enough kills to start spawning fast enemies
            if (GameController.GetComponent<GameController>().kills>fastEnemyStart)
            {
                float rand = Random.Range(0,100);
                //Check and see if the chances are high enough to spawn
                if (rand<25 && GameController.GetComponent<GameController>().kills<fastEnemyDouble)
                {
                    placement = new Vector3(Random.Range(-xConstrant, xConstrant), 1, Random.Range(-zConstrant, zConstrant));
                    offset = terr.SampleHeight(placement);
                    placement = new Vector3(placement.x, placement.y + offset, placement.z);
                    Instantiate(fastEnemy, placement, Quaternion.identity);
                    GameController.GetComponent<GameController>().activeEnemies++;
                } else if (rand < 40 && GameController.GetComponent<GameController>().kills > fastEnemyDouble)
                {
                    placement = new Vector3(Random.Range(-xConstrant, xConstrant), 1, Random.Range(-zConstrant, zConstrant));
                    offset = terr.SampleHeight(placement);
                    placement = new Vector3(placement.x, placement.y + offset, placement.z);
                    Instantiate(fastEnemy, placement, Quaternion.identity);
                    GameController.GetComponent<GameController>().activeEnemies++;
                }
            }
            //Check to see if there are enough kills to start spawning big enemies
            if (GameController.GetComponent<GameController>().kills > bigEnemyStart)
            {
                float rand = Random.Range(0, 100);
                //Check and see if the chances are high enough to spawn
                if (rand < 25 && GameController.GetComponent<GameController>().kills < bigEnemyDouble)
                {
                    placement = new Vector3(Random.Range(-xConstrant, xConstrant), 1, Random.Range(-zConstrant, zConstrant));
                    offset = terr.SampleHeight(placement);
                    placement = new Vector3(placement.x, placement.y + offset, placement.z);
                    Instantiate(bigEnemy, placement, Quaternion.identity);
                    GameController.GetComponent<GameController>().activeEnemies++;
                }
                else if (rand < 40 && GameController.GetComponent<GameController>().kills > bigEnemyDouble)
                {
                    placement = new Vector3(Random.Range(-xConstrant, xConstrant), 1, Random.Range(-zConstrant, zConstrant));
                    offset = terr.SampleHeight(placement);
                    placement = new Vector3(placement.x, placement.y + offset, placement.z);
                    Instantiate(bigEnemy, placement, Quaternion.identity);
                    GameController.GetComponent<GameController>().activeEnemies++;
                }
            }
        }
	}
}
