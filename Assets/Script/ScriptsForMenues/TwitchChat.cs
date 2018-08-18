using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Sockets;
using System.IO;
using System.Linq;
using UnityEngine.UI;

public class TwitchChat : MonoBehaviour {

    public static TcpClient twitchClient;
    private StreamReader reader;
    private StreamWriter writer;

    public Text chatBox;
    public Text timerText;

    private string username;
    private string password;
    private string channelName;
    [HideInInspector]
    public char[] letters = {'A','B','C','D','E','F','G', 'H'};
    [HideInInspector]
    public char[] numbers = { '1','2','3','4','5','6','7','8' };
    [HideInInspector]
    public List<string> movesInputted;
    [HideInInspector]
    public string theMove =" ";
    [HideInInspector]
    public static int selectedX = -1;
    [HideInInspector]
    public static int selectedY = -1;
    [HideInInspector]
    public static int selectedMoveX = -1;
    [HideInInspector]
    public static int selectedMoveY = -1;
    void Start () {
        if (username == null && password == null && channelName == null)
        {
            username = TwitchUsername.TwitchUserName.text;
            password = TwitchUsername.TwitchChatPassword.text;
            channelName = TwitchUsername.TwitchChannelName.text;
            Debug.Log(username);
            Debug.Log(password);
            Debug.Log(channelName);
        }
        if (username != "")
        {
            Connect();
        }
	}


    void Update()
    {
        if (username != "")
        {
            if (!twitchClient.Connected)
            {
                Connect();
            }
            chatBox.text = theMove;
            timerText.text = (Math.Round(timer % 60)).ToString();
            if (modePicker.white && !BoardManager.Instance.isWhiteTurn && !ScriptForInGameMenue.pause)
                ReadChat();
            else if (!modePicker.white && BoardManager.Instance.isWhiteTurn && !ScriptForInGameMenue.pause)
                ReadChat();
        }
    }

    private void Connect()
    {
        twitchClient = new TcpClient("irc.chat.twitch.tv", 6667);
        reader = new StreamReader(twitchClient.GetStream());
        writer = new StreamWriter(twitchClient.GetStream());

        writer.WriteLine("PASS " + password);
        writer.WriteLine("NICK " + username);
        writer.WriteLine("USER " + username + " 8 * :" + username);
        writer.WriteLine("JOIN #" + channelName);
        writer.Flush();
    }
    float timer = 0.0f;
    private void ReadChat()
    {
        timer += Time.deltaTime;
        if (Math.Round(timer % 60) == 10)
        {
            timer = 0.0f;
            Vote();
        }
        if (twitchClient.Available > 0)
        {
            var message = reader.ReadLine();
            if (message.Contains("PRIVMSG"))
            {
                //Get the users message by splitting it from the string
                var splitPoint = message.IndexOf(":", 1);
                message = message.Substring(splitPoint + 1);
                for(int i = 0; i < 8; i++)
                {    
                    if (message[0] == letters[i])
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (message[1] == numbers[j])
                            {
                                if (message[2] == ' ')
                                {
                                    for (int k = 0; k < 8; k++)
                                    {
                                        if (message[3] == letters[k])
                                        {
                                            for (int b = 0; b < 8; b++)
                                            {
                                                if (message[4] == numbers[b])
                                                {
                                                    Debug.Log(message);
                                                    movesInputted.Add(message);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    private void Vote()
    {
        if (movesInputted.Count > 0)
        {
            var movesInputedGroup = movesInputted.GroupBy(x => x);
            var vote = movesInputedGroup.OrderByDescending(x => x.Count()).First();
            theMove = vote.Key;
            for (int i = 0; i < 8; i++)
            {
                if (theMove[0] == letters[i])
                {
                    selectedX = i;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (theMove[1] == numbers[i])
                {
                    selectedY = i;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (theMove[3] == letters[i])
                {
                    selectedMoveX = i;
                }
            }
            for (int i = 0; i < 8; i++)
            {
                if (theMove[4] == numbers[i])
                {
                    selectedMoveY = i;
                }
            }
            print(selectedX);
            print(selectedY);
            print(selectedMoveX);
            print(selectedMoveY);
            movesInputted.Clear();
            vote = null;
        }
    }
}
