using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLightController : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;
    public float playerLightIntensity = 1.5f;

    public Vector3 positionOffset;
    public Vector3 rotationOffset;
    private bool lightOn;



    // Runs First frame this object is active. Finds the GameFunctions object and finds the offsets from the player also sets the lightOn to true Since light is on to start
    void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        lightOn = false;
        
    }
	
	// Update is called once per frame
    //Makes sure the Light is properly positoned arounud the player and checks to see if the user wants to turn the light on or off
	void Update () {

        if (GameController.GetComponent<GameController>().EndGame != true) {
            GameFunctions.GetComponent<GameFunctions>().SetCircularRotation(rotationOffset, positionOffset, this.gameObject, GameFunctions.GetComponent<GameFunctions>().player);
            GameFunctions.GetComponent<GameFunctions>().CheckLightOnOff(this.gameObject, ref lightOn, playerLightIntensity);
        }
      
    }

   
}
