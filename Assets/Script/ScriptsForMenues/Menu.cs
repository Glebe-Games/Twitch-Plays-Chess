using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Menu : MonoBehaviour
{
	public GameObject optionsUI;
	public GameObject menuUI;
	public GameObject modePickerUI;
	public GameObject StartButtons;
	public GameObject TwitchInputs;

	public GameObject AllInGameUI;
	public GameObject checkMateUI;
	public GameObject PauseMenueUI;

	public GameObject twitchLogin; 

	public GameObject colorPicker;

	public GameObject playingGameUI;
	
	public GameObject board;

    void OnEnable()
    {
        board.SetActive(false);
    }

    public void StartGame()
	{
		modePickerUI.SetActive(true);
		StartButtons.SetActive(false);
	}
	public void EndGame()
	{
		Application.Quit();
	}
	public void GoToOptions()
	{
		StartButtons.SetActive(false);
		optionsUI.SetActive(true);
	}
    public void GoToHelpUs()
    {
		StartButtons.SetActive(false);
    }
    public void BackToMenu()
	{
		optionsUI.SetActive(false);
		modePickerUI.SetActive(false);
		StartButtons.SetActive(true);
		TwitchInputs.SetActive(false);

	}
	public void ResetBeforeGameMenue()
	{
		optionsUI.SetActive(false);
		modePickerUI.SetActive(false);
		twitchLogin.SetActive(false);
		StartButtons.SetActive(true);
	}
	public void ResetGameMenue()
	{
		checkMateUI.SetActive(false);
		PauseMenueUI.SetActive(false);
		playingGameUI.SetActive(true); 
	}
	public void GoToColorPicker()
	{
		colorPicker.SetActive(true);
		modePickerUI.SetActive(false); 
	}
}
