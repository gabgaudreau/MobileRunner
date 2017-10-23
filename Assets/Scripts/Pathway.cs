/**
File Created October 23rd 2017 - File name = Pathway.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using System.Collections.Generic;
using UnityEngine;

public class Pathway : MonoBehaviour {
    [SerializeField]
    GameObject startingPoint, test;
    [SerializeField]
    int depth, orientation; // orientation 0 = left, 1 = forward, 2 = right
    private const float LANE_WIDTH = 0.75f;
    private const float DIST_BETWEEN_OBSTACLES = 8.0f;
    private const float NUM_ROWS_OBSTACLES = 5.0f;

    private List<List<Node>> _nodes = new List<List<Node>>();
    public List<List<Node>> nodes {
        get { return _nodes; }
        set { _nodes = value; }
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
            currNodes.Add(left);
            currNodes.Add(mid);
            currNodes.Add(right);
            _nodes.Add(currNodes);
        }
        //for testing
        foreach(List<Node> l in _nodes) {
            foreach(Node n in l) {
                Instantiate(test, n.worldPos + new Vector3(0, 1, 0), Quaternion.identity);
            }
        }
    }
}
