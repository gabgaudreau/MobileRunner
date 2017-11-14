/**
File Created October 23rd 2017 - File name = Node.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using UnityEngine;

public class Node : MonoBehaviour {
    private bool _isObstacle;
            
    public bool isObstacle {
        get { return _isObstacle; }
        set { _isObstacle = value; }
    }

    private Vector3 _worldPos;
    public Vector3 worldPos {
        get { return _worldPos; }
        set { _worldPos = value; }
    }

    private int _depth;
    public int depth{
        get { return _depth; }
        set { _depth = value; }
    }

    /// <summary>
    /// Constructor for Node 
    /// </summary>
    /// <param name="pos">vec3 position</param>
    /// <param name="d">int depth</param>
    public Node(Vector3 pos, int d) {
        _worldPos = pos;
        _depth = d;
    }
}
