using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RocketLauncherDropController : MonoBehaviour {

    private GameObject GameFunctions;
    public GameObject HandGunDrop;
    public GameObject RifleDrop;
    public GameObject ShotgunDrop;
    public GameObject RocketLauncherDrop;
    private Text messageText;

    public float loadedAmmo = 1;
    public float totalAmmo = 5;

    // Use this for initialization
    void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("Rocket Launcher: Loaded:" + loadedAmmo + " / Total:" + totalAmmo);
        }
    }

    //Each frame that the player is in contact with the pickup this function displays a message and checks it the player wants to puckup
    //IF they player presses Q it swaps the pickup and the pllayers current weapon
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            messageText.text = "Press 'Q' to pick up RocketLauncher.";
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (other.gameObject.GetComponent<PlayerController>().currentWeapon != WeaponType.None)
                {
                    GameFunctions.GetComponent<GameFunctions>().DestroyCurrentCreateDrop(other.gameObject.GetComponent<PlayerController>().currentWeapon, loadedAmmo, totalAmmo, transform);
                }
                other.gameObject.GetComponent<PlayerController>().currentWeapon = WeaponType.RocketLauncher;
                GameFunctions.GetComponent<GameFunctions>().DestroyDropAndCreateNewWeapon(this.gameObject, other.gameObject.GetComponent<PlayerController>().currentWeapon);
                messageText.text = "";
            }
        }
    }

    //Gets rid of the message about pressing Q when the player stoping touoching the pickup
    void OnTriggerExit(Collider other)
    {
        messageText.text = "";
    }
}
