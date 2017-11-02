using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NotificationManager : MonoBehaviour {

    private Text notificationText;

    public string[] messages;
    public int[] messageDuration;

    private int currentMessage = 0;

    private bool interrupted = false;
    private float interruptedLast;
    private int interruptedDuration;

    private float lastTime = 0f;

    private string scene = "";

	// Use this for initialization
	void Start () {
        notificationText = GetComponent<Text>();
        notificationText.text = messages[currentMessage];
	}

    internal void SetTextColor(Color color)
    {
        notificationText.color = color;
    }

    // Update is called once per frame
    void Update () {
        if (currentMessage < messages.Length)
        {
            if (Time.time - lastTime >= messageDuration[currentMessage])
            {
                currentMessage++;

                if (currentMessage < messages.Length)
                {
                    notificationText.text = messages[currentMessage];
                    lastTime = Time.time;
                }
            }
        }
        else
        {
            if (interrupted)
            {
                if (Time.time - interruptedLast >= interruptedDuration)
                {
                    if (scene == "")
                    {
                        notificationText.text = "";
                    }
                    else
                    {
                        if (NiceSceneTransition.instance != null)
                        {
                            NiceSceneTransition.instance.LoadScene(scene);
                        }
                        else
                        {
                            SceneManager.LoadScene(scene, LoadSceneMode.Single);
                        }
                    }
                }
            } else
            {
                notificationText.text = "";
            }
        }
	}

    public void DisplayInterrupted(string message, int duration)
    {
        interrupted = true;
        notificationText.text = message;
        interruptedLast = Time.time;
        interruptedDuration = duration;
    }

    public void DisplaySceneSwitch(string message, int duration, string scene)
    {
        interrupted = true;
        notificationText.text = message;
        interruptedDuration = duration;
        interruptedLast = Time.time;
        this.scene = scene;
    }
}
