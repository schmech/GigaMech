using UnityEngine;
using System.Collections;

public class FireAtWillScript : MonoBehaviour {

	public SpriteRenderer sprite;
	public WeaponScript weapon;
	public Vector2 motion;
	public float degreesPerSecond = 100f;
	public float angleError = 5f;

	private PlayerScript player;
	private States state = States.idle;

	void Start() {
		player = FindObjectOfType<PlayerScript>();
	}

#if UNITY_EDITOR
	void OnDrawGizmos() {
		var trans = UnityEditor.Selection.transforms;
		bool shouldDraw = false;

		foreach (var tran in trans) {
			if (tran.IsChildOf(transform)) {
				shouldDraw = true;
				break;
			}
		}

		if (shouldDraw) {

			float length = 5f;
			float angle = weapon.transform.eulerAngles.z;
			Gizmos.color = Color.red;
			Gizmos.DrawRay(weapon.transform.position, weapon.transform.right * length);
			Gizmos.color = Color.cyan;
			Gizmos.DrawRay(weapon.transform.position, Quaternion.AngleAxis(angle - angleError, Vector3.forward) * Vector3.right * length);
			Gizmos.DrawRay(weapon.transform.position, Quaternion.AngleAxis(angle + angleError, Vector3.forward) * Vector3.right * length);
		}
	}
#endif

	void Update() {
		switch(state) {
			case States.idle:
				// Wait until visable
				if (sprite.IsVisibleFrom(Camera.main))
					state = States.shooting;
				break;

			case States.shooting:
				// Move
				transform.position += new Vector3(motion.x, motion.y) * Time.deltaTime;

				// Destroy once no longer visable
				if (!sprite.isVisible)
					Destroy(gameObject);

				// Turn the weapon towards the player
				if (player) {
					// Get the angle
					Vector2 delta = player.transform.position - weapon.transform.position;
					float targetAngle = Mathf.Atan2(delta.y, delta.x) * Mathf.Rad2Deg;
					float angle = weapon.transform.eulerAngles.z;

					// Modify the angle
					angle = Mathf.MoveTowardsAngle(angle, targetAngle, degreesPerSecond * Time.deltaTime);
					weapon.transform.rotation = Quaternion.Euler(0, 0, angle);

					// Check if within firing angle
					float diff = Mathf.DeltaAngle(angle, targetAngle);
                    if (diff < angleError && diff > -angleError) {
						// FIRE ZE WEAPON
						weapon.Attack(true);
					}
				}
				break;
		}
	}

	enum States {
		idle, shooting
	}
}
