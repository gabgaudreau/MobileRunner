/**
File Created October 18th 2017 - File name = CameraFollow.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    [SerializeField]
    GameObject followTarget;
    private const float Y_OFFSET = 5;
    private const float Z_OFFSET = -10;

    /// <summary>
    /// This update will make sure that the camera follows the player at a specific distance, constantly.
    /// </summary>
	void Update () {
        transform.position = new Vector3(followTarget.transform.position.x, followTarget.transform.position.y + Y_OFFSET, followTarget.transform.position.z + Z_OFFSET);
	}
}
