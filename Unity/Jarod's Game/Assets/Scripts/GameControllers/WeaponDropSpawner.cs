using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDropController : MonoBehaviour {

    public GameObject handGunDrop;
    public GameObject shotgunDrop;
    public GameObject rifleDrop;
    public GameObject rocketLauncherDrop;
    public GameObject SwordDrop;
    public GameObject AxeDrop;
    public Terrain terr;
    public bool handGun=true;
    private bool rifle=false;
    private bool shotgun=false;
    private bool rocketLauncher=false;
    public bool sword = false;
    public bool axe = false;
    public float xConstrant = 45;
    public float zConstrant = 45;

    

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<GameController>().kills >= 0 && handGun == false)
        {
            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 0.5f, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
            placement = new Vector3(placement.x, placement.y + offset, placement.z);
            Instantiate(handGunDrop, placement, Quaternion.identity);
            handGun = true;
        }

        if (GetComponent<GameController>().kills>=15 && shotgun==false)
        {
            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 0.5f, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
            placement = new Vector3(placement.x,placement.y+offset,placement.z);
            Instantiate(shotgunDrop, placement, Quaternion.identity);
            shotgun = true;
        }
        if (GetComponent<GameController>().kills >= 40 && rifle == false)
        {
            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 0.5f, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
            placement = new Vector3(placement.x, placement.y + offset, placement.z);
            Instantiate(rifleDrop, placement, Quaternion.identity);
            rifle = true;
        }
        if (GetComponent<GameController>().kills >= 60 && rocketLauncher == false)
        {
            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 0.5f, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
            placement = new Vector3(placement.x, placement.y + offset, placement.z);
            Instantiate(rocketLauncherDrop, placement, Quaternion.identity);
            rocketLauncher = true;
        }
        if (GetComponent<GameController>().kills>=30 && sword==false)
        {
            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 0.5f, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
            placement = new Vector3(placement.x, placement.y + offset, placement.z);
            Instantiate(SwordDrop, placement, Quaternion.identity);
            sword = true;
        }
        if (GetComponent<GameController>().kills >= 50 && axe == false)
        {
            Vector3 placement = new Vector3(Random.Range(-xConstrant, xConstrant), 0.5f, Random.Range(-zConstrant, zConstrant));
            float offset = terr.SampleHeight(placement);
            placement = new Vector3(placement.x, placement.y + offset, placement.z);
            Instantiate(AxeDrop, placement, Quaternion.identity);
            axe = true;
        }
    }
}
