using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCotnroller : MonoBehaviour {

    public float speed = 3.0f;

    private Animator playerAnimation;
    private Transform flashlight;
    private AnimatorStateInfo currentAnimationState;

    // Use this for initialization
    void Start () {
        playerAnimation = GetComponent<Animator>();
        flashlight = transform.Find("Flashlight Pointer");

    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Horizontal") > 0.0)
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.right * Time.deltaTime * speed;
            playerAnimation.SetTrigger("Player_right");
            playerAnimation.SetTrigger("Player_w_right");
        }
        else if (Input.GetAxis("Horizontal") < 0.0)
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.left * Time.deltaTime * speed;
            playerAnimation.SetTrigger("Player_left");
            playerAnimation.SetTrigger("Player_w_left");
        }
        else if (Input.GetAxis("Vertical") > 0.0)
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.up * Time.deltaTime * speed;
            playerAnimation.SetTrigger("Player_up");
            playerAnimation.SetTrigger("Player_w_up");
        }
        else if (Input.GetAxis("Vertical") < 0.0)
        {
            GetComponent<Rigidbody2D>().transform.position += Vector3.down * Time.deltaTime * speed;
            playerAnimation.SetTrigger("Player_down");
            playerAnimation.SetTrigger("Player_w_down");
        }

        currentAnimationState = playerAnimation.GetCurrentAnimatorStateInfo(0);

        if (currentAnimationState.IsName("Player_down") || currentAnimationState.IsName("Player_w_down"))
        {
            flashlight.localEulerAngles = new Vector3(0, 0, 0);
        }
        else if (currentAnimationState.IsName("Player_up") || currentAnimationState.IsName("Player_w_up"))
        {
            flashlight.localEulerAngles = new Vector3(0, 0, 180);
        }
        else if (currentAnimationState.IsName("Player_left") || currentAnimationState.IsName("Player_w_left"))
        {
            flashlight.localEulerAngles = new Vector3(0, 0, -90);
        }
        else if (currentAnimationState.IsName("Player_right") || currentAnimationState.IsName("Player_w_right"))
        {
            flashlight.localEulerAngles = new Vector3(0, 0, 90);
        }
    }
}
