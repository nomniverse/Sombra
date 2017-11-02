using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour {

    public NotificationManager notifications;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            notifications.DisplayInterrupted("I can see a light at the end of this tunnel!", 2);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.name == "Player")
        {
            notifications.SetTextColor(Color.black);
            notifications.DisplaySceneSwitch("I feel so warm... and at peace... It's time to go now.", 2, "win");
        }
    }
}
