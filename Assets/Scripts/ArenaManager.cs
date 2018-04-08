using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaManager : MonoBehaviour {

    // Prefabs for the players to use.
    public GameObject unitPrefab;
    public GameObject bunkerPrefab;

    public Player[] players;

	// Use this for initialization
	void Start () {

        players = new Player[4];

        for(int i = 0; i < players.Length; i++) {
            // Find where to start the new player.
            Vector3 startPosition = new Vector3(400f * Mathf.Sin(Mathf.PI * i / 2), 0, 400f * Mathf.Cos(Mathf.PI * i / 2));
            // Create them.
            players[i] = new Player();

            // If they are the first player, they should be human.
            if (i == 0) {
                players[i].Init(PlayerType.HUMAN, startPosition, this);
            }
            else {
                // Make them a computer otherwise.
                players[i].Init(PlayerType.COMPUTER, startPosition, this);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
