/**
File Created October 23rd 2017 - File name = Pathway.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour {
    [SerializeField]
    GameObject startingPoint, obstaclePrefab;
    [SerializeField]
    int depth, orientation; // orientation 0 = left, 1 = forward, 2 = right
    private const float LANE_WIDTH = 0.75f;
    private const float DIST_BETWEEN_OBSTACLES = 8.0f;
    private const float NUM_ROWS_OBSTACLES = 5.0f;
    private List<GameObject> _obstacles =  new List<GameObject>();

    public List<GameObject> obstacles { 
        get { return _obstacles; }
        set { _obstacles = value; }
    }

    private List<Node> _justNodes = new List<Node>(); //This list of nodes is how I managed to transfer the obstacles from a pathway to another for the infinite scrolling illusion.
    public List<Node> justNodes {
        get { return _justNodes; }
        set { _justNodes = value; }
    }

    private List<List<Node>> _nodes = new List<List<Node>>();
    public List<List<Node>> nodes {
        get { return _nodes; }
        set { _nodes = value; }
    }

    /// <summary>
    /// Destroys all obstacles in a pathway as well as resets all of its nodes' isObstacle bool.
    /// </summary>
    public void RemoveObstacles() {
        foreach(Node n in _justNodes) {
            n.isObstacle = false;
        }
        foreach (GameObject g in _obstacles) {
            Destroy(g);
        }
    }

    /// <summary>
    /// This method will generate all of the possible nodes where an obstacle could spawn.
    /// </summary>
    public void GenerateNodes() {
        Vector3 currPos = startingPoint.transform.position;
        for (int i = 1; i <= NUM_ROWS_OBSTACLES; i++) {
            List<Node> currNodes = new List<Node>();
            Node mid = new Node(currPos, i);
            Node left, right;   
            if (orientation == 0) { // left
                left = new Node(new Vector3(currPos.x, currPos.y, currPos.z - LANE_WIDTH), i);
                right = new Node(new Vector3(currPos.x, currPos.y, currPos.z + LANE_WIDTH), i);
                currPos += new Vector3(-1 * DIST_BETWEEN_OBSTACLES, 0, 0);
            }
            else if(orientation == 1) { // forward
                left = new Node(new Vector3(currPos.x - LANE_WIDTH, currPos.y, currPos.z), i);
                right = new Node(new Vector3(currPos.x + LANE_WIDTH, currPos.y, currPos.z), i);
                currPos += new Vector3(0, 0, DIST_BETWEEN_OBSTACLES);
            }
            else { // right
                left = new Node(new Vector3(currPos.x, currPos.y, currPos.z + LANE_WIDTH), i);
                right = new Node(new Vector3(currPos.x, currPos.y, currPos.z - LANE_WIDTH), i);
                currPos += new Vector3(DIST_BETWEEN_OBSTACLES, 0, 0);
            }
            _justNodes.Add(left);
            _justNodes.Add(mid);
            _justNodes.Add(right);
            currNodes.Add(left);
            currNodes.Add(mid);
            currNodes.Add(right);
            _nodes.Add(currNodes);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public void GenerateObstacles() {
        foreach (List<Node> subList in _nodes) { // we're in the each sublist here
            int safe = Mathf.RoundToInt(Random.Range(0, 3));
            switch (safe) {
                case 0:
                    subList[1].isObstacle = true;
                    subList[2].isObstacle = true;
                    break;
                case 1:
                    subList[0].isObstacle = true;
                    subList[2].isObstacle = true;
                    break;
                case 2:
                    subList[0].isObstacle = true;
                    subList[1].isObstacle = true;
                    break;
            }
            //Instantiate Obstacles
            foreach (Node n in _justNodes) {
                if (n.isObstacle)
                    _obstacles.Add(Instantiate(obstaclePrefab, n.worldPos, Quaternion.identity));
            }
        }
    }

    /// <summary>
    /// OnTriggerEnter function used to check when the player collides with the end of a pathway, hence triggering the removeObstacle method.
    /// </summary>
    /// <param name="col">Collider, in this case, only the player object will trigger this.</param>
    void OnTriggerEnter(Collider col) {
        if(col.gameObject.layer == LayerMask.NameToLayer("Player")){
            RemoveObstacles();
        }
    }
}
