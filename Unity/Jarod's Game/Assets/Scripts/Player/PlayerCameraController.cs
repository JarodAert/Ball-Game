using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class PlayerCameraController : MonoBehaviour {

    private GameObject GameFunctions;
    private GameObject GameController;

    private Vector3 positionOffset;
    private Vector3 rotationOffset;



    // Runs First frame this object is active. Finds the GameFunctions object and finds the offsets from the player
    void Start () {
        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
        GameController = GameObject.FindGameObjectWithTag("GameController");
        positionOffset = GameFunctions.GetComponent<GameFunctions>().FindPositionOffset(transform);
        rotationOffset = GameFunctions.GetComponent<GameFunctions>().FindRotationOffset(transform);
        Debug.Log(positionOffset+"\n"+rotationOffset);
    }

    // Update is called once per frame
    //Makes sure the Player Camera spot is correctly positioned about the player at all times otherwise things would be funky
    void Update () {
        if (GameController.GetComponent<GameController>().EndGame != true)
        {
            GameFunctions.GetComponent<GameFunctions>().SetCircularRotation(rotationOffset, positionOffset, this.gameObject);
            GameFunctions.GetComponent<GameFunctions>().SetCameraAngle(this.gameObject);
        }
    }

    
}
