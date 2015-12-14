using UnityEngine;
using System.Collections;

public class ANUBossScript : MonoBehaviour {

	public Animator eyeAnimator;
	public ScrollingScript scrolling;
	public SpriteRenderer sprite;

	private bool spawned = false;
	
	void Update() {

		if (spawned) {
			// Spawned
		} else {
			// Not spawned yet
			if (!sprite.IsVisibleFrom(Camera.main)) {
				spawned = true;

				eyeAnimator.gameObject.SetActive(true);
				eyeAnimator.SetTrigger("Open eye");

				transform.position = new Vector3(Camera.main.transform.position.x, transform.position.y, transform.position.z);
				scrolling.direction = Vector2.zero;
				ScrollingScript.bossFight = true;
			}
		}
	}

}
