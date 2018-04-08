using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewRangeCollider : MonoBehaviour {

    Unit parent;
    float viewRange;

    public void Init (Unit _parent, float _viewRange) {
        this.parent = _parent;

        this.ChangeViewRange(_viewRange);
    }

    public void ChangeViewRange (float _viewRange) {
        this.viewRange = _viewRange;

        this.GetComponent<SphereCollider>().radius = this.viewRange;
    }
	
    void OnTriggerEnter (Collider col) {

    }

    void OnTriggerStay (Collider col) {

    }

    void OnTriggerExit (Collider col) {

    }
}
