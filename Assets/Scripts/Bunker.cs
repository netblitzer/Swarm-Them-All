using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bunker : MonoBehaviour {

    public Player parent;

    public float health;

    public float maxHealth = 100f;

    public float armor = 2f;

	// Use this for initialization
	public void Init (Player _parent) {
        this.parent = _parent;

        this.health = this.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
