using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour {

    List<Unit> Player1Units;

	// Use this for initialization
	void Start () {
        this.Player1Units = new List<Unit>();
        this.Player1Units.AddRange(FindObjectsOfType<Unit>());

        foreach(Unit u in this.Player1Units) {
            u.SetPosition(new Vector3(Random.Range(-10f, 10f), 0.5f, Random.Range(-10f, 10f)));
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
