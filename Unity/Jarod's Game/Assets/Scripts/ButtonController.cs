using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonController : MonoBehaviour {

    public GameObject mainCanvas;
    public GameObject ChoosePlayCanvas;
    public GameObject infoCanvas;

    public void StartArena()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("ArenaLevel");
    }

    public void StartTutorial()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Tutorial");
    }

    public void ChooseLevel()
    {
        mainCanvas.SetActive(false);
        ChoosePlayCanvas.SetActive(true);
    }

    public void StartCity()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("CityLevel");
    }

    public void StartOutDoor()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("OutDoorLevel");
    }

    public void BackToMain()
    {
        mainCanvas.SetActive(true);
        ChoosePlayCanvas.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ShowInfo()
    {
        mainCanvas.SetActive(false);
        infoCanvas.SetActive(true);
    }

    public void CloseInfoScreen()
    {
        mainCanvas.SetActive(true);
        infoCanvas.SetActive(false);
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   

}
