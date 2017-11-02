using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LOS;

public class ToggleFlashlight : MonoBehaviour {

    LOSRadialLight lightSCript;
    EdgeCollider2D flashlightPointer;

    public float maximumFlashlightCapacity = 100.0f;
    private float flashlightCapacity;
    private int flashlightLife;

    public Text flashlightText;
    public Text matchesText;
    public Text notificationText;
    public Text fearText;
    private NotificationManager notifications;

    public float maximumMatchCapacity = 30f;
    private float matchCapacity;
    public int matchCount = 10;

    private float fear;
    private float timeOff;

    string lightSource = "flashlight";
   
		// Use this for initialization
    void Start () {
        matchCapacity = maximumMatchCapacity;
        flashlightCapacity = maximumFlashlightCapacity;
        
        lightSCript = transform.Find("Radial Light").gameObject.GetComponent<LOSRadialLight>();
        flashlightPointer = transform.Find("Flashlight Pointer").gameObject.GetComponent<EdgeCollider2D>();
        notifications = notificationText.gameObject.GetComponent<NotificationManager>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Toggle Flashlight"))
        {
            lightSCript.ToggleOnOff(!lightSCript.GetComponent<Renderer>().enabled);

            // If the light is enabled, then the light source is a flashlight because the flashlight button turned it on
            if (lightSCript.GetComponent<Renderer>().enabled)
            {
                lightSCript.color = new Color32(0x94, 0x8E, 0x5B, 0xFF);        // Sets flashlight colour to #948E5BFF
                flashlightPointer.enabled = true;
                lightSource = "flashlight";
            }
            // If the light is not enabled, then there is not light source and do not decrease the light brightness
            else
            {
                flashlightPointer.enabled = false;
                lightSource = "none";
            }
        }

        if (Input.GetButtonDown("Light Match"))
        {
            if (!lightSCript.GetComponent<Renderer>().enabled)
            {
                if (matchCount > 0)
                {
                    // Things that should be done to set up the match
                    lightSCript.color = new Color32(0xB8, 0x92, 0x31, 0xFF);        // Sets match colour to #B89231FF
                    matchCapacity = maximumMatchCapacity;
                    lightSCript.radius = matchCapacity / 10;

                    lightSCript.ToggleOnOff(!lightSCript.GetComponent<Renderer>().enabled);

                    matchCount--;
                    matchesText.text = "Matches: " + matchCount;
                    lightSource = "match";

                    if (matchCount == 1)
                    {
                        notificationText.text = "I only have one match left... Better make it count.";
                    }
                }
            }
            else
            {
                lightSCript.ToggleOnOff(!lightSCript.GetComponent<Renderer>().enabled);
                flashlightPointer.enabled = false;
                lightSource = "none";
            }
        }

        if (lightSource == "flashlight")
        {
            flashlightCapacity -= 0.01f;
            flashlightLife = (int)(flashlightCapacity * 100 / maximumFlashlightCapacity);
            lightSCript.radius = flashlightLife / 10;

            flashlightText.text = "Battery: " + flashlightLife;

            switch (flashlightLife)
            {
                case 75:
                    notifications.DisplayInterrupted("I swear this thing was brighter a few seconds ago...", 3);
                    break;
                case 50:
                    notifications.DisplayInterrupted("I think my flashlight is half dead... Or half alive.", 3);
                    break;
                case 25:
                    notifications.DisplayInterrupted("I really need to conserve my battery life!", 3);
                    break;
                case 10:
                    notifications.DisplayInterrupted("I'm almost out of battery in my flashlight...", 3);
                    break;
                case 1:
                    notifications.DisplayInterrupted("This has lasted surprisingly long!", 3);
                    break;
                default:
                    break;
            }

            if (lightSCript.radius < 0f)
            {
                lightSCript.ToggleOnOff(!lightSCript.GetComponent<Renderer>().enabled);
                lightSource = "none";
            }
        }
        else if (lightSource == "match")
        {
            matchCapacity -= 0.03f;
            lightSCript.radius = matchCapacity / 10;

            if (lightSCript.radius < 0f)
            {
                lightSCript.ToggleOnOff(!lightSCript.GetComponent<Renderer>().enabled);
                lightSource = "none";
            }
        }
        else
        {
            if ((int)fear == 100)
            {
                notifications.DisplaySceneSwitch("Everything is fading to black... Goodbye...", 3, "main-menu");
            } else
            {
                fear += 0.05f;
                fearText.text = "Fear: " + (int)fear;
            }

            switch ((int)fear)
            {
                case 90:
                    notifications.DisplayInterrupted("I'm going to die here!", 3);
                    break;
                case 75:
                    notifications.DisplayInterrupted("Oh no, oh no, oh no. It's so dark. What's that?", 3);
                    break;
                case 50:
                    notifications.DisplayInterrupted("I don't want to be here anymore.", 3);
                    break;
                case 25:
                    notifications.DisplayInterrupted("I definitely heard something!", 3);
                    break;
                case 10:
                    notifications.DisplayInterrupted("I think I hear something...", 3);
                    break;
                case 1:
                    notifications.DisplayInterrupted("Who turned out the lights?", 3);
                    break;
                default:
                    break;
            }
        }
    }
        
}
