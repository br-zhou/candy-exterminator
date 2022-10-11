using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Health : MonoBehaviour
{
	public float health = 100;
	public bool damageEffects = true;
	[SerializeField] public UnityEvent deathTrigger;
	[SerializeField] public UnityEvent damageTrigger;
	public float MaxHealth = 0;
	public static Color Red = new Color(255, 0, 0);
	public static Color Green = new Color(0, 255, 255);
	//public SpriteRenderer spriteRenderer;
	public static float damageEffectLength = .07f;
	private Color originalMaterial;
	public Coroutine flashRoutine;
	invincibility invin;
	public GameObject[] SGO;
	private SpriteRenderer[] SRGO;
	bool extraSprites = false;
	void Start()
	{
		if (SGO.Length > 0) extraSprites = true;
		if (extraSprites)
		{
			SRGO = new SpriteRenderer[SGO.Length];
			for (int index = 0; index < SGO.Length; index++) //gets all children
			{
				SRGO[index] = SGO[index].GetComponent<SpriteRenderer>();
			}
		}
		else
		{
			SRGO = new SpriteRenderer[1];
			SRGO[0] = GetComponent<SpriteRenderer>();
		}
		if (MaxHealth == 0) MaxHealth = health;
		//if(spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
		invin = GetComponent<invincibility>();
		//flashMaterial = Resources.Load("flash_material", typeof(Material)) as Material;
		originalMaterial = SRGO[0].color;

	}

	public void TakeDamage(float damage)
	{
		if(invin != null)
		{
			if (invin.invincible) return;
			invin.InvincibleDelay(invin.damageDelay);
		}
		health -= damage;
		//print($"{gameObject.name} has taken dmg");
		if (health <= 0)
		{
			health = 0;
			deathTrigger.Invoke();
		}
		else
		{
			damageTrigger.Invoke();
			Flash(Red);
		}
	}

	public void Kill()
	{
		health = 0;
		deathTrigger.Invoke();
	}

	public void Heal(float amount)
	{

		health += amount;
		
		if (health > MaxHealth)
		{
			health = MaxHealth;
		}
		Flash(Green);
	}
	public void Flash(Color color)
	{
		// If the flashRoutine is not null, then it is currently running.
		if (flashRoutine != null)
		{
			// In this case, we should stop it first.
			// Multiple FlashRoutines the same time would cause bugs.
			StopCoroutine(flashRoutine);
		}

		// Start the Coroutine, and store the reference for it.
		flashRoutine = StartCoroutine(FlashRoutine(color));
	}
	private IEnumerator FlashRoutine(Color color)
	{
		// Swap to the flashMaterial.
		foreach(SpriteRenderer sr in SRGO)
		{
			sr.color = color;
		}
		

		// Pause the execution of this function for "duration" seconds.
		yield return new WaitForSeconds(damageEffectLength);

		// After the pause, swap back to the original material.
		foreach (SpriteRenderer sr in SRGO)
		{
			sr.color = originalMaterial;
		}

		// Set the routine to null, signaling that it's finished.
		flashRoutine = null;
	}
	
	/*
	void FixedUpdate()
	{
		if (transform.position.y < ms.voidDepth)
		{
			deathTrigger.Invoke();
		}
	}*/

}
