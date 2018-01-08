using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerController : MonoBehaviour {

    private GameObject GameFunctions;

    // Use this for initialization
    void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
    }
	
	// Update is called once per frame
	void Update () {
        transform.eulerAngles = new Vector3(GameFunctions.GetComponent<GameFunctions>().xRotation, 90-GameFunctions.GetComponent<GameFunctions>().yRotation,0);
    }


    void OnTriggerStay(Collider other)
    {
       /*
        if (!other.CompareTag("RangeCollider"))
        {
            Debug.Log("Collided: "+ other.gameObject.transform.position);
            GetComponent<LineRenderer>().SetPosition(1, other.transform.position);
        }
        */
    }
    

}
