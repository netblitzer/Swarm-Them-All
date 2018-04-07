using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Unit : MonoBehaviour {

    public Vector3 position;

    public Vector3 velocity;

    public Vector3 force;

    public float distFromSelected = 2f;

    NavMeshAgent agent;

    public Vector3 targetPosition;

    public bool atTarget;

    public float health;

    public float maxHealth = 40f;

    public float armor = 1f;

    public float attackDamage = 10f;

    public float attackSpeed = 1f;

    public float maxSpeed = 10f;

    // Use this for initialization
    void Start () {
        this.agent = GetComponent<NavMeshAgent>();

        this.position = this.transform.position;

        this.velocity = Vector3.zero;
        this.force = Vector3.zero;

        this.atTarget = true;
        this.health = this.maxHealth;
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseToWorld;
            if (Physics.Raycast(mouseRay, out mouseToWorld, 1000f)) {
                Debug.Log(mouseToWorld.point);
                this.targetPosition = mouseToWorld.point;
                this.atTarget = false;
            }
        }

        if (!this.atTarget) {

            Vector3 diff = this.targetPosition - this.position;
            diff.y = 0f;

            float dist = Vector3.SqrMagnitude(diff);

            if (dist < this.distFromSelected) {
                this.atTarget = true;
            }
            else {
                this.ApplyForce(diff.normalized * 20f);
            }
        }
        else {
            if (this.velocity.sqrMagnitude > 1) {
                this.ApplyForce(-this.velocity * 5f);
            } else
                this.velocity = Vector3.zero;
        }

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

        this.velocity = Vector3.ClampMagnitude(this.velocity, this.maxSpeed);

        Vector3 targetPos = this.position + this.velocity * Time.deltaTime;

        NavMeshHit posSample;

        if (NavMesh.SamplePosition(targetPos, out posSample, 100f, NavMesh.AllAreas)) {
            this.position.Set(posSample.position.x, this.position.y, posSample.position.z);
        }

        this.transform.position = this.position;
    }
}
