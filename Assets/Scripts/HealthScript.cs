using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour
{
	public int hp = 1;
	public bool isEnemy = true;

	[Header("Invincibility effect")]
	
	public float invincibilityTime = 0f;
	public bool damageIndicationOnly = false;

	public SpriteRenderer sprite;
	public Color damagedTint = new Color(1f,0f,0f,.5f);
	public ColorHelper.ColorMode tintMode;
	public AnimationCurve blending = new AnimationCurve(new Keyframe(0f,0f), new Keyframe(1f,1f));

	private Color originalColor;

	private float damageCooldown;
	private bool invincible {
		get { return damageCooldown > 0f && !damageIndicationOnly; }
		set { damageCooldown = value ? invincibilityTime : 0f; }
	}

	private GameObject balloonLife;

	void Start () {
		if (tag == "balloon")
			balloonLife = GameObject.Find ("Player");

		if (sprite != null)
			// Save the original sprite tint
			originalColor = sprite.color;
	}


	void Update() {
		// Damage cooldown, i.e. invincibility
		if (damageCooldown > 0)
			damageCooldown -= Time.deltaTime;

		if (sprite != null) {
			if (damageCooldown > 0) {
				// Damage effect
				float t = Mathf.Clamp(1f-blending.Evaluate(damageCooldown / invincibilityTime), 0f, 1f);
				sprite.color = ColorHelper.Lerp(originalColor, damagedTint, t, tintMode);
			} else {
				// Normal
				sprite.color = originalColor;
			}
		}

		if (!isEnemy)
			HealthGUIScript.instance.UpdateUIElements(hp, 3);
	}
	
	public void Damage(int damageCount)	{
		// Invincible mechanic.
		if (invincible)
			return;

		invincible = true;


		hp -= damageCount;
		
		if (hp <= 0)
		{
			SpecialEffectsHelper.Instance.Explosion(transform.position);
			SoundEffectsHelper.Instance.MakeExplosionSound();
			// Dead!
			Destroy(gameObject);

			// GameOver GUI
			if (!isEnemy)
				transform.parent.gameObject.AddComponent<GameOverScript> ();
		}
	}
	
	void OnTriggerEnter2D(Collider2D otherCollider)
	{
		// Is this a shot?
		ShotScript shot = otherCollider.gameObject.GetComponent<ShotScript>();
		if (shot != null)
		{
			// Avoid friendly fire
			if (shot.isEnemyShot != isEnemy)
			{
				Damage(shot.damage);
				
				// Destroy the shot
				Destroy(shot.gameObject); // Remember to always target the game object, otherwise you will just remove the script

				if(tag == "balloon"){
					
					Destroy(shot.gameObject);
					SoundEffectsHelper.Instance.MakeLifeSound();
					Destroy(gameObject);
					
					if(balloonLife.GetComponent<HealthScript>().hp <= 2){
						balloonLife.GetComponent<HealthScript>().hp +=1;
					}
				}
			}

		}
	}
}