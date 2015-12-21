using UnityEngine;
using System.Collections;

public class ShotScript : MonoBehaviour {

	public int damage = 1;
	public bool isEnemyShot = false;
	public SpriteRenderer sprite;
	public TrailRenderer trail;

	void Start () {
		Destroy (gameObject, 20);
	}

	void Update() {
		if (sprite != null && !sprite.IsVisibleFrom(Camera.main)) {
			// Destroy imidietly indead

			if (trail != null)
				trail.transform.parent = transform.parent;

			Destroy(gameObject);

		}
	}

}
