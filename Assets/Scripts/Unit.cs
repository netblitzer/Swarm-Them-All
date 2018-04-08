using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public Player parent;

    public Vector3 position;

    public Vector3 velocity;

    public Vector3 force;

    public float distanceToTargetToSlow = 2f;

    public Vector3 targetMovePosition;

    public bool atTarget;

    public float health;

    public UnitParams myParams;

    public ViewRangeCollider viewRange;

    // Use this for initialization
    void Awake () {
        this.viewRange = this.GetComponentInChildren<ViewRangeCollider>();
	}

    public void Init (Player _parent, Vector3 _position, UnitParams _params) {
        this.Init(_parent, _position, _position, _params);
    }

    public void Init (Player _parent, Vector3 _position, Vector3 _targetMovePosition, UnitParams _params) {

        if (_parent == null) {
            Debug.LogError("ERROR: Unit created without a parent!");
            GameObject.Destroy(this);
        }

        this.parent = _parent;

        this.position = _position;
        this.transform.position = this.position;
        this.targetMovePosition = _targetMovePosition;

        // Initialize the units view range class.
        this.viewRange.Init(this, _params.viewRange);

        if (Vector3.Distance(this.targetMovePosition, this.position) > this.distanceToTargetToSlow)
            this.atTarget = false;
        else
            this.atTarget = true;

        this.myParams = _params;
        this.health = this.myParams.maxHealth;

    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseToWorld;
            if (Physics.Raycast(mouseRay, out mouseToWorld, 1000f)) {
                Debug.Log(mouseToWorld.point);
                this.targetMovePosition = mouseToWorld.point;
                this.atTarget = false;
            }
        }

        if (!this.atTarget) {

            Vector3 diff = this.targetMovePosition - this.position;
            diff.y = 0f;

            float dist = Vector3.SqrMagnitude(diff);

            if (dist < this.distanceToTargetToSlow) {
                this.atTarget = true;
            }
            else {
                this.ApplyForce(diff.normalized * this.myParams.movementForce);
            }
        }
        else {
            if (this.velocity.sqrMagnitude > 1) {
                this.ApplyForce(-this.velocity * this.myParams.movementDeceleration);
            }
        }

        if (this.velocity.sqrMagnitude > 1f) {
            this.ApplyForce(Vector3.Scale(this.velocity, this.velocity) * -this.myParams.movementDrag);
        } else
            this.velocity = Vector3.zero;

        this.UpdatePhysics();
	}

    public void SetPosition (Vector3 _position) {
        this.position = _position;
        this.transform.position = this.position;
    }

    public void ApplyForce (Vector3 force) {
        this.force += force;
    }

    void UpdatePhysics () {

        this.velocity += this.force * Time.deltaTime;

        this.force = Vector3.zero;

        Vector3 targetPos = this.position + this.velocity * Time.deltaTime;

        NavMeshHit posSample;

        if (NavMesh.SamplePosition(targetPos, out posSample, 100f, NavMesh.AllAreas)) {
            this.position.Set(posSample.position.x, this.position.y, posSample.position.z);
        }

        this.transform.position = this.position;
    }
}

public struct UnitParams {
    public float maxHealth;
    public float armor;
    public float attackSpeed;
    public float attackDamage;
    public float movementForce;
    public float movementDrag;
    public float movementDeceleration;
    public float viewRange;

    public UnitParams(float _max, float _a, float _aS, float _aD, float _mF, float _mDr, float _mDe, float _vr) {
        this.maxHealth = _max;
        this.armor = _a;
        this.attackSpeed = _aS;
        this.attackDamage = _aD;
        this.movementForce = _mF;
        this.movementDrag = _mDr;
        this.movementDeceleration = _mDe;
        this.viewRange = _vr;
    }
}

public enum UnitAttackDamageTypes {
    NORMAL,
    ACID,
    ANTIFLESH,
}
