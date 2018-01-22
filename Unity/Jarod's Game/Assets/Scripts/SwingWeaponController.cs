using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingWeaponController : MonoBehaviour {

    private GameObject GameFunctions;
    public GameObject explosion;
    public float swingSpeed = 1;
    public float swingStrength = 100;
    private float nextSwing = 0;
    private bool swing = false;

    // Use this for initialization
    void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Mouse0)&&Time.time>nextSwing)
        {
            nextSwing = Time.time + swingSpeed;
            swing = true;
        }
        if (Time.time<nextSwing)
        {
            GameFunctions.GetComponent<GameFunctions>().SwingWeapon(this.gameObject, swingSpeed);
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,transform.eulerAngles.y,0);
            swing = false;
        }
	}

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy") && swing==true)
        {
            GameFunctions.GetComponent<GameFunctions>().DamageObject(other.gameObject, swingStrength);
        }
    }
}
