using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptForInGameMenue : MonoBehaviour {

	public GameObject PauseMenue;
	public GameObject CheckMateScreen;
	public GameObject Menue;
	public GameObject pauseButton;

    public static bool pause = false;

	public void BackToGameFromPause()
	{
		PauseMenue.SetActive(false);
		pauseButton.SetActive(true);
        pause = false;
    }
	public void ToPauseMenue()
	{
		PauseMenue.SetActive(true);
		pauseButton.SetActive(false);
        pause = true;
    }
	public void BackToMenue()
	{
		Menue.SetActive(true);
		pauseButton.SetActive(true);
		PauseMenue.SetActive(false);
		//make sure to remember to reset menue to start 

	}
}
