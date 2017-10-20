/**
File Created October 18th 2017 - File name = PlayerMove.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    private const float PLAYER_MOVE_SPEED = 750.0f;
    private const float LANE_WIDTH = 3.5f;
    private const float DIFFICULTY_INCREASE = 0.015f;
    private float difficulty;
    private bool isTransitioning;
    private int lane; //-1 = left, 0 = middle, 1 = right
    private Rigidbody rb;
    [SerializeField]
    GameObject respawnMiddle, respawnRight, respawnLeft;

    /// <summary>
    /// Start function, initiates some variables.
    /// </summary>
	void Start () {
        difficulty = 1.0f;
        lane = 0;
        rb = GetComponent<Rigidbody>();
	}

    /// <summary>
    /// Button function to move Left, called by pressing the arrow buttons on the screen.
    /// </summary>
    public void OnClickLeft() {
        if (!isTransitioning && lane != -1) {
            transform.position = new Vector3(transform.position.x - LANE_WIDTH, transform.position.y, transform.position.z);
            lane--;
        }
    }

    /// <summary>
    /// Button function to move Right, called by pressing the arrow buttons on the screen.
    /// </summary>
    public void OnClickRight() {
        if(!isTransitioning && lane != 1) {
            transform.position = new Vector3(transform.position.x + LANE_WIDTH, transform.position.y, transform.position.z);
            lane++;
        }
    }

    /// <summary>
    /// Keyboard inputs are in here, will be removed once I have no more use for them.
    /// </summary>
    void Update() {
        //REMOVE KEYBOARD INPUT WHEN DONE WITH IT.
        if (!isTransitioning) {
            if (Input.GetKeyDown(KeyCode.Q) && lane != -1) {
                transform.position = new Vector3(transform.position.x - LANE_WIDTH, transform.position.y, transform.position.z);
                lane--;
            }
            if (Input.GetKeyDown(KeyCode.E) && lane != 1) {
                transform.position = new Vector3(transform.position.x + LANE_WIDTH, transform.position.y, transform.position.z);
                lane++;
            }
        }
    }

    /// <summary>
    /// This will make the game wait for X amount of time. Then activates gravity, gives the ball a chance to land, bounce and land again before locking gravity.
    /// </summary>
    /// <returns>Waits X amount of time</returns>
    IEnumerator ActivateGravity() {
        yield return new WaitForSeconds(0.75f);
        rb.useGravity = false;
    } 

    /// <summary>
    /// OnTriggerEnter function, handles entering with multiple triggers, scaling difficulty as well as infinte scrolling logic is implemented in this function.
    /// </summary>
    /// <param name="col">Object that is the trigger</param>
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.layer == LayerMask.NameToLayer("Pit")) {
            difficulty += DIFFICULTY_INCREASE;
            switch (lane) {
                case -1:
                    transform.position = respawnLeft.transform.position;
                    break;
                case 0:
                    transform.position = respawnMiddle.transform.position;
                    break;
                case 1:
                    transform.position = respawnRight.transform.position;
                    break;
            }
        }
        else if(col.gameObject.layer == LayerMask.NameToLayer("GravityTriggerEnd")) {
            rb.velocity = new Vector3(0, 0, 10);
            rb.useGravity = true;
            isTransitioning = true;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("GravityTriggerStart")) {
            rb.AddForce(Vector3.forward * PLAYER_MOVE_SPEED * difficulty);
            isTransitioning = false;
            StartCoroutine(ActivateGravity());
        }
    }
}
