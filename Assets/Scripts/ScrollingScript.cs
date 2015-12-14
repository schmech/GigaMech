using UnityEngine;
using System.Collections;

public class ScrollingScript : MonoBehaviour
{
	
	public Vector2 speed = new Vector2(2, 2);
	public Vector2 direction = new Vector2(-1, 0);
	public bool isLinkedToCamera = false;

	public static bool bossFight = false;
	private static float scale = 1f;
	
	void Update()
	{
		// Slow down if it's the bossfight
		scale = Mathf.MoveTowards(scale, bossFight ? 0f : 1f, Time.deltaTime / 3f);


		// Movement
		Vector3 movement = new Vector3(
			speed.x * direction.x,
			speed.y * direction.y,
			0);
		
		movement *= Time.deltaTime;
		movement *= scale;
		transform.Translate(movement);
		
		// Move the camera
		if (isLinkedToCamera)
		{
			Camera.main.transform.Translate(movement);
		}
	}
}