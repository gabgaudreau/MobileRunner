/**
File Created October 18th 2017 - File name = CameraFollow.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    GameObject player;
    private float yOffset = 5;
    private float zOffset = -10;

    /// <summary>
    /// This update will make sure that the camera follows the player at a specific distance, constantly.
    /// </summary>
	void Update () {
        transform.position = new Vector3(player.transform.position.x, player.transform.position.y + yOffset, player.transform.position.z + zOffset);
	}
}
