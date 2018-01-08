using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialController : MonoBehaviour {

    public GameObject handGunDrop;
    public GameObject enemy;
    public Text messageText;
    public Text healthText;
    public Text killsText;
    public float nextInstruction=1000;
    private bool handGun = false;
    public bool enemyYes = false;

    private string[] messages = new string[10];
    private int messageCount = 0;

    public float checkpointCount = 0;

	// Use this for initialization
	void Start () {
        healthText.text = "Health: 100";
        killsText.text = "Kills: 0";
        messageText.text = "";

        messages[0] = "An Enemy has just spawned! Aim and shoot with the mouse to kill it.";
        messages[1] = "Notice the Ammo Counter in the lower left. The left number is the loaded Ammo, the right is your total Ammo.";
        messages[2] = "When you run out of loaded ammo you must press 'R' to reload. It will then take a second to reload.";
        messages[3] = "If your total ammo is running low you can collect boxes like the one in the center to get more.";
        messages[4] = "You can also press 'P' at any time during the game to pause.";
        messages[5] = "When you are finished messing around press 'X' to return to the menu and play.";


    }

    // Update is called once per frame
    void Update() {
        if (checkpointCount == 0)
        {
            messageText.text = "Welcome to the tutorial. Lets start by rolling around using the 'ASWD' keys and collecting the purple checkpoints.";
        }
        if (checkpointCount == 1)
        {
            messageText.text = "If you hold down 'Shift' while rolling you can burst your speed";
        }
        if (checkpointCount == 2)
        {
            messageText.text = "You can Jump by pressing space. After jumping there is a short recovery time where you cannot jump.";
        }
        if (checkpointCount == 3)
        {
            messageText.text = "You can press and hold 'E' to stop movement instantly and hold your position";
        }
        if (checkpointCount == 4)
        {
            messageText.text = "Pressing 'F' will turn off and on your flashlight if you need it.";
        }
        if (checkpointCount == 5 && handGun == false)
        {
            Instantiate(handGunDrop, new Vector3(0, 0, 0), Quaternion.identity);
            messageText.text = "Good Job! Now roll over to the weapon in the center and pick it up.";
            handGun = true;
            Debug.Log("handgun");
        }
        if (checkpointCount == 6 && enemyYes==false)
        {
            messageText.text = "An Enemy has just spawned! Aim and shoot with the mouse to kill it.";
            Instantiate(enemy, new Vector3(20, 1, 20), Quaternion.identity);
            nextInstruction = Time.time + 5;
            enemyYes = true;
        }
        if (Time.time > nextInstruction)
        {
            messageText.text = messages[messageCount];
            messageCount++;
            nextInstruction = Time.time + 3;
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            SceneManager.LoadScene("StartScene");
        }
        if (GameObject.FindGameObjectWithTag("HandGunDrop") == null && GameObject.FindGameObjectWithTag("Checkpoint") == null)
        {
            checkpointCount = 6;
        }
	}


}
