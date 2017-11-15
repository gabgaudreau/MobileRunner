/**
File Created November 15th 2017 - File name = MenuManager.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MenuManager : MonoBehaviour {
    private bool isTurningRight, isTurningLeft, isTurningMiddle;
    private const float ROTATION_SPEED = 3.5f;

    /// <summary>
    /// On click function to start the game.
    /// </summary>
    public void OnClickStart() {
        SceneManager.LoadScene(1);
    }

    /// <summary>
    /// On click function to rotate the camera to the right, to the how to play panel.
    /// </summary>
    public void OnClickHowTo() { // Right
        if (!isTurningMiddle && !isTurningLeft) {
            StopAllCoroutines();
            isTurningRight = true;
        }
    }

    /// <summary>
    /// On click function to rotate the camera left, to the top scores panel.
    /// </summary>
    public void OnClickTopScores() { // Left
        if (!isTurningMiddle && !isTurningRight) {
            StopAllCoroutines();
            isTurningLeft = true;
        }
    }

    /// <summary>
    /// On click function to rotate the camera back to the main panel.
    /// </summary>
    public void OnClickBack() { // Middle
        if (!isTurningRight && !isTurningLeft) {
            StopAllCoroutines();
            isTurningMiddle = true;
        }
    }

    /// <summary>
    /// On click function to exit game.
    /// </summary>
    public void OnClickExit() {
        Application.Quit();
    }

    /// <summary>
    /// Update method, checks for boolean and starts coroutine when appropriate.
    /// </summary>
    void Update() {
        if (isTurningRight) 
            StartCoroutine(RotateRight());
        if (isTurningLeft) 
            StartCoroutine(RotateLeft());
        if (isTurningMiddle) 
            StartCoroutine(RotateMiddle());
    }

    /// <summary>
    /// Function that handles the rotation of the camera, Right turn.
    /// </summary>
    /// <returns>Wait for seconds</returns>
    IEnumerator RotateRight() {
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(0, 90, 0), ROTATION_SPEED * Time.deltaTime);
        yield return new WaitForSeconds(2.0f);
        Camera.main.transform.rotation = Quaternion.Euler(0, 90, 0);
        isTurningRight = false;
    }

    /// <summary>
    /// Function that handles the rotation of the camera, Left turn.
    /// </summary>
    /// <returns>Wait for seconds</returns>
    IEnumerator RotateLeft() {
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(0, -90, 0), ROTATION_SPEED * Time.deltaTime);
        yield return new WaitForSeconds(2.0f);
        Camera.main.transform.rotation = Quaternion.Euler(0, -90, 0);
        isTurningLeft = false;
    }

    /// <summary>
    /// Function that handles the rotation of the camera back to the main panel.
    /// </summary>
    /// <returns>Wait for seconds</returns>
    IEnumerator RotateMiddle() {
        Camera.main.transform.rotation = Quaternion.Slerp(Camera.main.transform.rotation, Quaternion.Euler(0, 0, 0), ROTATION_SPEED * Time.deltaTime);
        yield return new WaitForSeconds(2.0f);
        Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        isTurningMiddle = false;
    }
}
