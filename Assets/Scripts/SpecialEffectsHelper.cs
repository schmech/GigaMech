using UnityEngine;
using System.Collections;


public class SpecialEffectsHelper : MonoBehaviour
{
	
	public static SpecialEffectsHelper Instance;
	
	public ParticleSystem smokeEffect;
	public ParticleSystem fireEffect;
	public GameObject kaboom;
	
	void Awake()
	{
		// Register the singleton
		if (Instance != null)
		{
			Debug.LogError("Multiple instances of SpecialEffectsHelper!");
		}
		
		Instance = this;
	}
	
	
	public void Explosion(Vector3 position)
	{
		/*
		// Smoke on the water
		instantiate(smokeEffect, position);
		
		// Tu tu tu, tu tu tudu
		
		// Fire in the sky
		instantiate(fireEffect, position);
		*/

		
		Instantiate(kaboom, position, Quaternion.Euler(0,0,Random.Range(0,360)));
	}
	
	
	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position)
	{
		ParticleSystem newParticleSystem = Instantiate(
			prefab,
			position,
			Quaternion.identity
			) as ParticleSystem;
		
		// Make sure it will be destroyed
		Destroy(
			newParticleSystem.gameObject,
			newParticleSystem.startLifetime
			);
		
		return newParticleSystem;
	}
}