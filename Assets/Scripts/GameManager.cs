/**
File Created October 23rd 2017 - File name = GameManager.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager gm; //Singleton.
    [SerializeField]
    Level[] levels; // 0 = left, 1 = forward, 2 = right
    private Level previousLevel;
    private Level currentLevel;
    [SerializeField]
    GameObject obstaclePrefab;

    /// <summary>
    /// Will generate all nodes in all levels.
    /// </summary>
    void Start() {
        if (gm == null)
            gm = this;

        foreach (Level l in levels) {
            foreach (Pathway pw in l.pathways) {
                pw.GenerateNodes();
            }
        }
        currentLevel = levels[1];
        foreach (Pathway p in currentLevel.pathways) {
            p.GenerateObstacles();
        }
    }

    /// <summary>
    /// Changes level, calls transferObstacles and removes obstacles once the transfer is done, then instantiates 
    /// all the obstacles of the current level.
    /// </summary>
    /// <param name="i">int level</param>
    public void SetCurrentLevel(int i) {
        previousLevel = currentLevel;
        currentLevel = levels[i];
        TransferObstacles(previousLevel.pathways[previousLevel.pathways.Count - 1], currentLevel.pathways[0]);
        previousLevel.pathways[previousLevel.pathways.Count - 1].RemoveObstacles();
        int length = currentLevel.pathways.Count;
        for(int j = 1; j < length; j++) {
            currentLevel.pathways[j].GenerateObstacles();
        }
    }

    /// <summary>
    /// This method takes 2 pathways as parameter, the last of the previous level and the first of the next level
    /// In order to keep the infinite scrolling illusion, this method takes the isObstacle bool of all the nodes in the last pathway
    /// of the prev level and copies it into the nodes of the first pathway of the next level.
    /// </summary>
    /// <param name="last">Pathway last</param>
    /// <param name="first">Pathway first</param>
    void TransferObstacles(Pathway last, Pathway first) {
        int length = last.justNodes.Count;
        for (int i = 0; i < length; i++) {
            if (last.justNodes[i].isObstacle)
                first.justNodes[i].isObstacle = true;
        }
        foreach (Node n in first.justNodes) {
            if (n.isObstacle)
                first.obstacles.Add(Instantiate(obstaclePrefab, n.worldPos, Quaternion.identity));
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
//skybox
