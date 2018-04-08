using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour, IPointerClickHandler {//, IPointerDownHandler, IPointerUpHandler, IDragHandler {

    public PlayerType type;

    public GameObject bunkerPrefab;
    public Bunker myBunker;

    public GameObject unitPrefab;
    public List<Unit> units;

    // The units currently selected by the player.
    // Mainly only used by the human players.
    public List<Unit> selectedUnits;

    public UnitParams currentUnitParams;

	// Use this for initialization
	void Awake () {
		
	}

    public void Init (PlayerType _type, Vector3 _startingPosition, ArenaManager _manager) {
        // Find the prefabs.
        this.unitPrefab = _manager.unitPrefab;
        this.bunkerPrefab = _manager.bunkerPrefab;

        this.type = _type;

        // Create the bunker.
        this.myBunker = GameObject.Instantiate(this.bunkerPrefab, _startingPosition, Quaternion.identity).GetComponent<Bunker>();
        this.myBunker.Init(this);

        // Create the default unit params/
        // Health, Armor, Attack Speed, Attack Damage, Movement Force, Movement Drag, Movement Deceleration, View Range.
        this.currentUnitParams = new UnitParams(40f, 1f, 1f, 5f, 25f, 5f, 2f, 10f);
    }
	
	// Update is called once per frame
	void Update () {
		

	}

    void UpdateMouse () {
        
    }

    public void OnPointerClick (PointerEventData eventData) {
        // Find out what we clicked on.

        // Check if it was a left click.
        if (eventData.button == PointerEventData.InputButton.Left) {
            // Find what we clicked.
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit mouseToWorld;
            if (Physics.Raycast(mouseRay, out mouseToWorld, 1000f, LayerMask.GetMask("Default"))) {
                // If we clicked on something selectable, see what it is.
                if (mouseToWorld.collider.tag == "Selectable") {
                    Unit selected = mouseToWorld.collider.gameObject.GetComponent<Unit>();

                    if (selected != null && selected.parent == this) {
                        this.selectedUnits.Clear();
                        this.selectedUnits.Add(selected);
                    }
                }
            }
        }
    }
}

public enum PlayerType {
    HUMAN,
    COMPUTER,
}
