using UnityEngine;
using System.Collections;

public class DodgeMeScript : MonoBehaviour {

	[Header("Initial idle state")]
	public Renderer mainRenderer;
	public float initWait = 1f;
	public Vector2 initMotion;

	[Header("Aiming state")]
	public float minAimWait = 1f;
	public Vector2 aimMotion;
	[Tooltip("Degrees per second")]
	public float angularSpeed = 100f;

	[Header("Moving state")]
	public float topSpeed = 3f;
	public int damage = 1;
	public GameObject trail;

	private States state;

	private float timeSpent = 0f;
	private float angle = 180;
	private Transform target;

	private bool damagedPlayer;

	void Awake() {
		var player = GameObject.FindGameObjectWithTag("Player");
		if (player == null) {
			Debug.LogError("Unable to find the player!");
		} else {
			target = player.transform;
		}

		trail.SetActive(false);
	}

	void Update() {
		switch(state) {
		case States.idle:
			// Wait until inside screen
			if (mainRenderer.IsVisibleFrom(Camera.main)) {
				// Move faster left
				transform.position += new Vector3(initMotion.x, initMotion.y) * Time.deltaTime;
				
				// Wait /initWait/ seconds
				timeSpent += Time.deltaTime;
				
				if (timeSpent >= initWait) {
					// Next face
					timeSpent = 0;
					state = States.aimin;
				}
			}
			break;
				
		case States.aimin:
			// Wait /aimWait/ seconds
			timeSpent += Time.deltaTime;

			if (target == null) {
				// PLAYER IS DEAD
				state = States.flyin;
				trail.SetActive(true);
				break;
			}

			// Adjust angle
			var delta = target.position - transform.position;
			var targetAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
			angle = Mathf.MoveTowardsAngle(angle, targetAngle, angularSpeed * Time.deltaTime);

			// Move
			transform.localRotation = Quaternion.Euler(0,0,angle);
			transform.position += new Vector3(aimMotion.x, aimMotion.y) * Time.deltaTime;

			// Wait for correct angle
			if (/*Mathf.Floor(targetAngle) == Mathf.Floor(angle) && */timeSpent >= minAimWait) {
				state = States.flyin;
				trail.SetActive(true);
			}
			break;
			
		case States.flyin:
			// Move forwards, 'til the end of thyme
			transform.Translate(Vector3.right * topSpeed * Time.deltaTime);

			// ... or end of screen
			if (!mainRenderer.isVisible) {
				Destroy(gameObject);
				trail.transform.parent = transform.parent;
			}
			break;
		}
	}

	void OnTriggerStay2D(Collider2D other) {
		if (state == States.flyin && other.tag == "Player") {

			var health = other.GetComponent<HealthScript>();
			health.Damage (damage);

		}
	}

	public enum States {
		idle, aimin, flyin
	}

}
