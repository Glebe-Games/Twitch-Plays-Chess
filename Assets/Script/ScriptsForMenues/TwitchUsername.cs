using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using UnityEngine.UI;
using System.Threading;

public class TwitchUsername : MonoBehaviour
{

    public static TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public static UnityEngine.UI.InputField TwitchUserName;
    public static UnityEngine.UI.InputField TwitchChatPassword;
    public static UnityEngine.UI.InputField TwitchChannelName;

    public Button submitButton;

    public string userTwitchUsername;
    public string userTwitchPassword;
    public string userTwitchChannelName;
    private bool error;

    private int cntr = 0;

    public GameObject errorChannelName;
    public GameObject errorChatPassword;
    public GameObject errorUserName;
    public GameObject twitchChat;
    public GameObject theBestCanvas;
    public GameObject chessBoard;
    public GameObject TwitchUserNameObject;
    public GameObject TwitchChatPasswordObject;
    public GameObject TwitchChannelNameObject;
    public GameObject inGameCanvas;


    // Checks if there is anything entered into the input field.
    string LockInput(UnityEngine.UI.InputField input)
    {
        string checker;
        if (input.text.Length > 0)
        {
            checker = input.text;
            Debug.Log(checker);
        }
        return input.text;
    }
    public void Start()
    {
        TwitchUserName = TwitchUserNameObject.GetComponent<UnityEngine.UI.InputField>();
        TwitchChatPassword = TwitchChatPasswordObject.GetComponent<UnityEngine.UI.InputField>();
        TwitchChannelName = TwitchChannelNameObject.GetComponent<UnityEngine.UI.InputField>();
        if (PlayerPrefs.HasKey("ChannelName"))
        {
            TwitchUserName.text = PlayerPrefs.GetString("ChannelName");
        }
        if (PlayerPrefs.HasKey("Password"))
        {
            TwitchChatPassword.text = PlayerPrefs.GetString("Password");
        }
        if (PlayerPrefs.HasKey("UserName"))
        {
            TwitchUserName.text = PlayerPrefs.GetString("UserName");
        }
        /*TwitchUserName.onEndEdit.AddListener(delegate
        {
            userTwitchUsername = LockInput(TwitchUserName);
            Debug.Log(userTwitchUsername);
        });
        TwitchChatPassword.onEndEdit.AddListener(delegate
        {
            userTwitchPassword = LockInput(TwitchChatPassword);
            Debug.Log(userTwitchPassword);
        });
        TwitchChannelName.onEndEdit.AddListener(delegate
        {
            userTwitchChannelName = LockInput(TwitchChatPassword);
            Debug.Log(userTwitchChannelName);
        });
        */
    }
    public void shutDownError()
    {
        errorChatPassword.SetActive(false);
        errorChannelName.SetActive(false);
        errorUserName.SetActive(false);
    }

    public void SubmitAllvalues()
    {
        error = false;
        if (!PlayerPrefs.HasKey("ChannelName"))
        {
            PlayerPrefs.SetString("ChannelName", LockInput(TwitchChannelName));
        }
        userTwitchPassword = LockInput(TwitchChatPassword);
        if (!PlayerPrefs.HasKey("Password"))
        {
            PlayerPrefs.SetString("Password", LockInput(TwitchChatPassword));
        }
        userTwitchUsername = LockInput(TwitchUserName);
        if (!PlayerPrefs.HasKey("UserName"))
        {
            PlayerPrefs.SetString("UserName", LockInput(TwitchUserName));
        }
        if (TwitchChannelName.text.Length <= 0 || TwitchChatPassword.text.Length <= 0 || TwitchUserName.text.Length <= 0)
        {
            error = true;
        }
        else
        {
            error = false;
        }

        Connect();
        connectionCheck();

        if (!error)
        {
            twitchChat.SetActive(true);
            theBestCanvas.SetActive(false);
            chessBoard.SetActive(true);
            inGameCanvas.SetActive(true);
        }
        if (TwitchChannelName.text.Length <= 0)
        {
            errorChannelName.SetActive(true);
        }
        if (TwitchChatPassword.text.Length <= 0)
        {
            errorChatPassword.SetActive(true);
        }
        if (TwitchUserName.text.Length <= 0)
        {
            errorUserName.SetActive(true);
        }
    }
    public void LinkToTwitchTMI()
    {
        Application.OpenURL("https://twitchapps.com/tmi/");
    }
    private void connectionCheck()
    {
        cntr = 0;
        while (!twitchClient.Connected)
        {
            Connect();
        }
        while (true)
        {
            cntr++;
            if (twitchClient.Available>0)
            {
                var message = reader.ReadLine();
                if (message.Contains("Invalid NICK") || message.Contains("Invalid PASS") || message.Contains("Invalid USER"))
                {
                    shutDownError();
                    error = true;
                    break;
                }
                else if (message.Contains("JOIN"))
                {
                    error = false;
                    break;
                }
            }
            if (cntr>5000000)
            {
                shutDownError();
                error = true;
                break;
            }
        }
    }
    private void Connect(){
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + TwitchChatPassword.text);
        writer.WriteLine("NICK " + TwitchUserName.text);
        writer.WriteLine("USER " + TwitchUserName.text + " 8 * :" + TwitchUserName.text);
        writer.WriteLine("JOIN #" + TwitchChannelName.text);
        writer.Flush();
    }
}
