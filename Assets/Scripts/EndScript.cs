using UnityEngine;
using System.Collections;

public class EndScript : MonoBehaviour {
	
	private GameObject player;
	
	void OnTriggerEnter2D (Collider2D other){
		
		if (other.gameObject.tag == "Player") 
		{
			Debug.Log ("hit");
			//transform.parent.gameObject.AddComponent<GameOverScript> ();
			Application.LoadLevel("Stage2");
			
			
			
		}
		
	}
	
}