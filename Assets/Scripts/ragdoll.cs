using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ragdoll : MonoBehaviour
{
    // Start is called before the first frame update
    private Color originalColor;
    SpriteRenderer spriteRenderer;
    public GameObject[] Children;
    private SpriteRenderer[] SpriteRenderers;
    float decaySpeed = .75f;
    void Start()
    {
        Children = new GameObject[transform.childCount];
        SpriteRenderers = new SpriteRenderer[Children.Length];
        for (int index = 0; index < transform.childCount; index++) //gets all children
        {
            Children[index] = transform.GetChild(index).gameObject;
        }
        for (int index = 0; index < Children.Length; index++) //gets all spriterenderers
        {
            SpriteRenderers[index] = Children[index].GetComponent<SpriteRenderer>();
        }
        originalColor = SpriteRenderers[0].color;
        StartCoroutine(Decay());
    }

    // Update is called once per frame
    IEnumerator Decay()
    {
        float Transparency = 1;
        yield return new WaitForSeconds(1);
        while (Transparency > 0)
		{
            Transparency -= decaySpeed * Time.deltaTime;
            foreach(SpriteRenderer sr in SpriteRenderers)
			{
                sr.color = new Color(originalColor.r, originalColor.g, originalColor.b, Transparency);
			}
            yield return new WaitForSeconds(0.02f);
        }
        Destroy(gameObject);
    }
}
