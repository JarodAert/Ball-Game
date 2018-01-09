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
    public float fastEnemyLowerChance = 25;
    public float fastEnemyHigherChance = 40;
    public float bigEnemyLowerChance = 25;
    public float bigEnemyHigherChance = 40;

    private float normalEnemyChance = 0;
    private float fastEnemyChance = 0;
    private float bigEnemyChance = 0;

    // Use this for initialization
    void Start () {
        GameController = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    //Here a new enemy object is randomly placed on the map every 3 seconds
    //After a set number of kills there is a chance it will spawn fast and big enemies 
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Normal: " + normalEnemyChance + " fast: " + fastEnemyChance + " big: " + bigEnemyChance);
        }

        if (Time.time > nextTime && GameController.GetComponent<GameController>().activeEnemies < maxEnemies && GameController.GetComponent<GameController>().EndGame == false)
        {

            SetChances();

            float randNormal = Random.Range(0, 100);
            float randFast = Random.Range(0, 100);
            float randBig = Random.Range(0, 100);

            if (randNormal < normalEnemyChance)
            {
                SpawnEnemy(enemy);
            }
            if (randFast < fastEnemyChance)
            {
                SpawnEnemy(fastEnemy);
            }
            if (randBig < bigEnemyChance)
            {
                SpawnEnemy(bigEnemy);
            }

        }
    }

    private void SpawnEnemy(GameObject enemy)
    {
        Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 1, Random.Range(-zConstrant, zConstrant));
        float offset = terr.SampleHeight(placement);
        placement = new Vector3(placement.x, placement.y + offset, placement.z);
        Instantiate(enemy, placement, Quaternion.identity);
        GameController.GetComponent<GameController>().activeEnemies++;
        nextTime = Time.time + spawnRate;
    }

    private void SetChances()
    {
        float kills = GameController.GetComponent<GameController>().kills;

        if (kills >= fastEnemyStart && kills < fastEnemyDouble)
        {
            fastEnemyChance = fastEnemyLowerChance;
        }
        else if (kills >= fastEnemyStart && kills > fastEnemyDouble)
        {
            fastEnemyChance = fastEnemyHigherChance;
        }

        if (kills >= bigEnemyStart && kills < bigEnemyDouble)
        {
            bigEnemyChance = bigEnemyLowerChance;
        }
        else if (kills >= bigEnemyStart && kills > bigEnemyDouble)
        {
            bigEnemyChance = bigEnemyLowerChance;
        }
        if (kills < fastEnemyStart)
        {
            normalEnemyChance = 100;
            fastEnemyChance = 0;
            bigEnemyChance = 0;
        }
        else
        {
            normalEnemyChance = 100 - bigEnemyChance - fastEnemyChance;
        }
    }
}
