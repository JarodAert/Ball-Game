﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordPickupController : MonoBehaviour {

    private GameObject GameFunctions;

    private Text messageText;

    public float loadedAmmo = 0;
    public float totalAmmo = 0;

    // When existance starts this functions finds the GameFunctions object and the message text UI element
    void Start()
    {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();

    }

    // Update is called once per frame
    // Constantly spins the pickup so it looks fancy
    void Update()
    {
        transform.Rotate(new Vector3(0, 30, 0) * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log("RifleDrop: Loaded:" + loadedAmmo + " / Total:" + totalAmmo);
        }
    }

    //Each frame that the player is in contact with the pickup this function displays a message and checks it the player wants to puckup
    //IF they player presses Q it swaps the pickup and the pllayers current weapon
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            messageText.text = "Press 'Q' to pick up Sword.";
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Debug.Log(other.gameObject.GetComponent<PlayerController>().currentWeapon);
                if (other.gameObject.GetComponent<PlayerController>().currentWeapon != WeaponType.None)
                {
                    GameFunctions.GetComponent<GameFunctions>().DestroyCurrentCreateDrop(other.gameObject.GetComponent<PlayerController>().currentWeapon, loadedAmmo, totalAmmo, transform);
                }
                other.gameObject.GetComponent<PlayerController>().currentWeapon = WeaponType.Sword;
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
