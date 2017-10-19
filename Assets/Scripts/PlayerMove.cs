/**
File Created October 18th 2017 - File name = PlayerMove.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {
    private const float PLAYER_MOVE_SPEED = 750.0f;
    private const float DIFFICULTY_INCREASE = 0.015f;
    private float difficulty;
    private Rigidbody rb;
    [SerializeField]
    GameObject respawn;

    /// <summary>
    /// Start function, initiates some variables.
    /// </summary>
	void Start () {
        difficulty = 1.0f;
        rb = GetComponent<Rigidbody>();
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
            transform.position = respawn.transform.position;
            difficulty += DIFFICULTY_INCREASE;
        }
        else if(col.gameObject.layer == LayerMask.NameToLayer("GravityTriggerEnd")) {
            rb.velocity = new Vector3(0, 0, 10);
            rb.useGravity = true;
        }
        else if (col.gameObject.layer == LayerMask.NameToLayer("GravityTriggerStart")) {
            rb.AddForce(Vector3.forward * PLAYER_MOVE_SPEED * difficulty);
            StartCoroutine(ActivateGravity());
        }
    }
}
