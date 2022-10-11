using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class invincibility : MonoBehaviour
{
    public float damageDelay = 0.3f;
    public float respawnDelay = 3f;
    [HideInInspector] public bool invincible = false;
    private Color originalMaterial;
    private SpriteRenderer spriteRenderer;
    public GameObject[] otherSpiritesObjects;
    private SpriteRenderer[] otherSpriteRenderers;
    Health h;

    void Start()
    {
        h = GetComponent<Health>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalMaterial = spriteRenderer.color;
        if (otherSpiritesObjects != null)
        {
            otherSpriteRenderers = new SpriteRenderer[otherSpiritesObjects.Length];
            for (int i = 0; i < otherSpiritesObjects.Length; i++)
			{
                otherSpriteRenderers[i] = otherSpiritesObjects[i].GetComponent<SpriteRenderer>();
            }
        }
    }

    public void InvincibleDelay(float delay)
    {
        invincible = true;
        StartCoroutine(Animation(delay));
        Invoke("ResetInvincibility", delay + Health.damageEffectLength);
    }

    void ResetInvincibility()
    {
        invincible = false;
    }

    IEnumerator Animation(float delay)
    {
        float t = 0;

        yield return new WaitForSeconds(Health.damageEffectLength);

        while (invincible)
		{
            if(h.flashRoutine == null)
			{
                float alpha = Mathf.Sin(4 * t) * .2f + .6f;
                spriteRenderer.color = new Color(originalMaterial.r, originalMaterial.g, originalMaterial.b, alpha);
                if (otherSpriteRenderers != null)
                {
                    foreach (SpriteRenderer s in otherSpriteRenderers)
                    {
                        s.color = spriteRenderer.color;
                    }
                }
                t += Time.deltaTime;
            }
            yield return 0;
        }
		while (spriteRenderer.color.a < .95)
		{
            float smoothedalphatranstion = Mathf.Lerp(spriteRenderer.color.a, 1f, .5f);
            spriteRenderer.color = new Color(originalMaterial.r, originalMaterial.g, originalMaterial.b, smoothedalphatranstion);
            if(otherSpriteRenderers != null)
			{
                foreach (SpriteRenderer s in otherSpriteRenderers)
                {
                    s.color = spriteRenderer.color;
                }
            }
            yield return new WaitForSeconds(0.02f);

        }
        spriteRenderer.color = originalMaterial;
        if (otherSpriteRenderers != null)
        {
            foreach (SpriteRenderer s in otherSpriteRenderers)
            {
                s.color = spriteRenderer.color;
            }
        }
    }
}
