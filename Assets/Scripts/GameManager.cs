/**
File Created October 23rd 2017 - File name = GameManager.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;

public class GameManager : MonoBehaviour {
    [SerializeField]
    Level[] levels;
            
    /// <summary>
    /// Will generate all nodes in all levels.
    /// </summary>
    void Start () {
        foreach (Level l in levels) {
            foreach (Pathway pw in l.pathways) {
                pw.GenerateNodes();
            }
        }   
	}
}
//TODO:
//3..2..1..GO then start
//main menu, box style menu, rotating camera between the different panels
//pause button in top left corner
//find a player asset
//power ups allow the player to shoot obstables, shoot idk what kind of projectiles yet
//pick up a key to open the door
//walls and obstacles need to actually hit the player and do damage
//player flashes after getting hit, signaling temporary immunity
//generate obstacles.
//skybox
