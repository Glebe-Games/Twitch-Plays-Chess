using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modePicker : MonoBehaviour
{
    public static int whichMode;
    public GameObject bestCanvas;
    public GameObject inGmaeCanvas;
    public GameObject start;
    public GameObject mode;
    public GameObject twitchLogin;
    public GameObject board;
    public GameObject colorPicker;

    public static bool white;

    void Start()
    {
        board.SetActive(false);
    }
    void Update()
    {
    }
    public void SinglePlayer()
    {
        whichMode = 1;
        Debug.Log(whichMode);
        bestCanvas.SetActive(false);
        board.SetActive(true);
        inGmaeCanvas.SetActive(true);
    }
    public void TwoPlayer()
    {
        whichMode = 2;
        bestCanvas.SetActive(false);
        Debug.Log(whichMode);
        board.SetActive(true);
        inGmaeCanvas.SetActive(true);
    }
    public void TwitchVsAI()
    {
        whichMode = 4;
        Debug.Log(whichMode);
        twitchLogin.SetActive(true);
        gameObject.SetActive(false);
    }
    public void PlayerVsTwitch()
    {
        whichMode = 3;
        twitchLogin.SetActive(true);
        gameObject.SetActive(false);
        colorPicker.SetActive(false);
    }
    public void ColorPicker()
    {
        colorPicker.SetActive(true);
        gameObject.SetActive(false);
    }
    public void backToMenue()
    {
        start.SetActive(true);
        mode.SetActive(false);
        board.SetActive(true);
    }
    public void White()
    {
        white = true;
    }
    public void Balck()
    {
        white = false;
    }
}
