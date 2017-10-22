/**
File Created October 18th 2017 - File name = CameraFollow.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    public static CameraFollow instance;
    [SerializeField]
    GameObject followTarget;
    private const float Y_OFFSET = 3.2f;
    private const float Z_OFFSET_FORWARD = -3.2f;
    private const float X_OFFSET_RIGHT = -3.2f;
    private const float X_OFFSET_LEFT = 3.2f;
    private enum State { LEFT, FORWARD, RIGHT };
    private State state;

    /// <summary>
    /// Start method, initializes some variables.
    /// </summary>
    void Start() {
        if (instance == null)
            instance = this;
        state = State.FORWARD;
    }

    /// <summary>
    /// SetState method, will use the singleton pattern instantiated above to let playermove script change the state of the camera.
    /// </summary>
    /// <param name="s">a string telling which state to set the camera to.</param>
    public void SetState(string s) {
        switch (s) {
            case "LEFT":
                state = State.LEFT;
                break;
            case "FORWARD":
                state = State.FORWARD;
                break;
            case "RIGHT":
                state = State.RIGHT;
                break;
        }
    }

    /// <summary>
    /// This update will make sure that the camera follows the player at a specific distance, based on the camera state, constantly.
    /// </summary>
	void Update() {
        if (state == State.FORWARD) {
            transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y + Y_OFFSET, followTarget.transform.position.z + Z_OFFSET_FORWARD);
        }
        else if (state == State.RIGHT) {
            transform.position = new Vector3(followTarget.transform.position.x + X_OFFSET_RIGHT, followTarget.transform.position.y + Y_OFFSET, followTarget.transform.position.z);
        }
        else if (state == State.LEFT) {
            transform.position = new Vector3(followTarget.transform.position.x + X_OFFSET_LEFT, followTarget.transform.position.y + Y_OFFSET, followTarget.transform.position.z);
        }
    }
}
