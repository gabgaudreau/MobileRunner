/**
File Created October 18th 2017 - File name = PlayerMove.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    private const float PLAYER_MOVE_SPEED = 500.0f;
    private const float LANE_WIDTH = 0.75f;
    private const float DIFFICULTY_INCREASE = 0.015f;
    private const float Z_VALUE_TURN = 101.01f;
    private const float X_ANGLE_CAMERA = 25.0f;
    private const float X_VALUE_RIGHT_LEVEL = 250.0f;
    private const float X_VALUE_LEFT_LEVEL = -250.0f;
    private const float Y_VALUE_PLAYER = 1.25f;
    private enum State { LEFT, FORWARD, RIGHT};
    private State state;
    private float difficulty;
    private bool isTransitioning;
    private int lane; //0 = left, 1 = middle, 2 = right
    private Rigidbody rb;
    [SerializeField]
    GameObject respawnMiddle, respawnRight, respawnLeft;
    [SerializeField]
    GameObject[] respawns;

    /// <summary>
    /// Start function, initiates some variables.
    /// </summary>
	void Start () {
        difficulty = 1.0f;
        lane = 1;
        rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.forward * PLAYER_MOVE_SPEED * difficulty);
        state = State.FORWARD;
    }

    /// <summary>
    /// Button function to move Left, called by pressing the arrow buttons on the screen.
    /// </summary>
    public void OnClickLeft() {
        if (!isTransitioning && Input.GetKeyDown(KeyCode.Q) && lane != 0) {
            if (state == State.FORWARD)
                transform.position = new Vector3(transform.position.x - LANE_WIDTH, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - LANE_WIDTH);
            lane--;
        }
    }

    /// <summary>
    /// Button function to move Right, called by pressing the arrow buttons on the screen.
    /// </summary>
    public void OnClickRight() {
        if (!isTransitioning && Input.GetKeyDown(KeyCode.E) && lane != 2) {
            if (state == State.FORWARD)
                transform.position = new Vector3(transform.position.x + LANE_WIDTH, transform.position.y, transform.position.z);
            else
                transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + LANE_WIDTH);
            lane++;
        }
    }

    /// <summary>
    /// Keyboard inputs are in here, will be removed once I have no more use for them.
    /// </summary>
    void Update() {
        //REMOVE KEYBOARD INPUT WHEN DONE WITH IT.
        if (!isTransitioning) {
            if (Input.GetKeyDown(KeyCode.Q) && lane != 0) {
                if(state == State.FORWARD)
                    transform.position = new Vector3(transform.position.x - LANE_WIDTH, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - LANE_WIDTH);
                lane--;
            }
            if (Input.GetKeyDown(KeyCode.E) && lane != 2) {
                if(state == State.FORWARD)
                    transform.position = new Vector3(transform.position.x + LANE_WIDTH, transform.position.y, transform.position.z);
                else
                    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + LANE_WIDTH);
                lane++;
            }
        }
    }

    /// <summary>
    /// OnTriggerEnter function, handles entering with multiple triggers, scaling difficulty as well as infinte scrolling logic is implemented in this function.
    /// Handles camera angles when turning a corner, both left and right, handles camera enum and player enum.
    /// </summary>
    /// <param name="col">Object that is the trigger</param>
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.layer == LayerMask.NameToLayer("End")) {
            state = State.FORWARD;
            int respawnIndex = Mathf.RoundToInt(Random.Range(0, 3)) * 3 + lane; // 0 = left, 1 = forward, 2 = right
            difficulty += DIFFICULTY_INCREASE;
            transform.position = respawns[respawnIndex].transform.position;
            Camera.main.transform.rotation = Quaternion.Euler(X_ANGLE_CAMERA, 0, 0);
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.forward * PLAYER_MOVE_SPEED * difficulty);
            CameraFollow.instance.SetState("FORWARD");
        }
        else if(col.gameObject.layer == LayerMask.NameToLayer("TurnLeft")) {
            state = State.LEFT;
            transform.position = new Vector3(X_VALUE_LEFT_LEVEL, Y_VALUE_PLAYER, Z_VALUE_TURN);
            transform.rotation = Quaternion.Euler(0, -90, 0);
            rb.velocity = Vector3.zero;
            CameraFollow.instance.SetState("LEFT");
            Camera.main.transform.rotation = Quaternion.Euler(X_ANGLE_CAMERA, -90, 0);
            rb.AddForce(Vector3.left * PLAYER_MOVE_SPEED * difficulty);
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("TurnRight")) {
            state = State.RIGHT;
            transform.position = new Vector3(X_VALUE_RIGHT_LEVEL, Y_VALUE_PLAYER, Z_VALUE_TURN); 
            transform.rotation = Quaternion.Euler(0, 90, 0);
            rb.velocity = Vector3.zero;
            CameraFollow.instance.SetState("RIGHT");
            Camera.main.transform.rotation = Quaternion.Euler(X_ANGLE_CAMERA, 90, 0);
            rb.AddForce(Vector3.right * PLAYER_MOVE_SPEED * difficulty);
        }
    }
}
