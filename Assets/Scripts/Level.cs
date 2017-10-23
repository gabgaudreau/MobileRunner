/**
File Created October 23rd 2017 - File name = Level.cs
Author: Gabriel Gaudreau
Project: MobileRunner
*/
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour {
    [SerializeField]
    private List<Pathway> _pathways = new List<Pathway>();
    public List<Pathway> pathways {
        get { return _pathways; }
        set { _pathways = value; }
    }

    private int _levelNum; //0 = left, 1 = forward, 2 = right
    public int levelNum {
        get { return _levelNum; }       
        set { _levelNum = value; }
    }
}
