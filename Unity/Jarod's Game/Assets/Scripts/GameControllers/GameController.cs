using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameController : MonoBehaviour {

    public bool EndGame = false;

    private Text messageText;
    private Text healthText;
    private Text killsText;
    public GameObject player;
    public GameObject playerWeaponHand;
    public GameObject GameFunctions;
    public GameObject OakTree;
    public GameObject FirTree;
    public GameObject PoplarTree;
    public Button resumeButton;
    public Terrain terr;

    public float kills;
    public float activeEnemies=0;
    public int NumberOfTrees = 10;
    public float treeXConstrant = 35;
    public float treeZConstrant = 35;
    public float xPlayerConstrant = 50;
    public float zPlayerConstrant = 50;

	// Runs when it first exists. Find the Text UI elements and then sets them to their starting valuess
	void Start () {

        killsText = GameObject.Find("Canvas/NumberOfKillsText").GetComponent<Text>();
        healthText = GameObject.Find("Canvas/HealthText").GetComponent<Text>();
        messageText = GameObject.Find("Canvas/MessageText").GetComponent<Text>();
        messageText.text = "";
        healthText.text = "Health: " + player.GetComponent<PlayerController>().health;
        killsText.text = "Kills: " + kills;

        GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");

        PlaceTrees();

    }
	
	// Update is called once per frame
    //If the game is over this puts the message up to press r to restart and the handles the inputs
	void Update () {
        if (EndGame==true)
        {
            Cursor.visible = true;
            messageText.text = "Game Over! Press 'R' to restart. Press 'X' to return to the menu.";
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().ToString());
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("StartMenu");
            }
        }
        else
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale==1) {
                Time.timeScale = 0;
                messageText.text = "Game is paused. Press 'P' to resume. Press 'X' to return to the menu.";
            }else if (Time.timeScale==0)
            {
                Time.timeScale = 1;
                messageText.text = "";
            }
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            
            if (Time.timeScale==0)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("StartMenu");
            }
        }
	}

    public void ResumeGame()
    {
        Destroy(GameObject.FindGameObjectWithTag("ResumeButton"));
        Time.timeScale = 1;
    }

    private void PlaceTrees()
    {
        for(int i = 1; i < NumberOfTrees; i++)
        {
            float rand = Random.Range(0,100);
            Vector3 placement = new Vector3(Random.Range(-treeXConstrant,treeXConstrant),0, Random.Range(-treeZConstrant, treeZConstrant));
            if (terr!=null)
            {
                float offset= terr.SampleHeight(placement);
                placement = new Vector3(placement.x, placement.y + offset,placement.z);
            }
            if (rand<30) {
                Instantiate(OakTree, placement, Quaternion.identity);
            }else if (rand>=30 && rand<60)
            {
                Instantiate(FirTree, placement, Quaternion.identity);
            }
            else if (rand >= 60 && rand < 100)
            {
                Instantiate(PoplarTree, placement, Quaternion.identity);
            }

    }
    }
}
