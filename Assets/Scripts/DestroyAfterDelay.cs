using UnityEngine;
using System.Collections;

public class DestroyAfterDelay : MonoBehaviour {

	public float delay;

	void Start () {
		Destroy(gameObject, delay);
	}
}
