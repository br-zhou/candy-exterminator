using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class grenade_master : MonoBehaviour
{
    // Start is called before the first frame update
    public int startingGrenades = 3;
    int grenadesLeft;
    [SerializeField] GameObject grenade;
    [SerializeField] GameObject grenadeUI;
    public TextMeshProUGUI grenadeText;
    float force = 120;
    static grenade_master me;
    audio_manager am;
	private void Start()
	{
        am = GameObject.FindGameObjectWithTag("Player").GetComponent<audio_manager>();
        me = this;
        grenadesLeft = startingGrenades;
        HideGrenadeUI();
    }
	// Update is called once per frame
	void Update()
    {
        if (grenadesLeft > 0 && Input.GetMouseButtonDown(1))
		{
            if(startingGrenades != 99) grenadesLeft--;
            if (grenadeUI != null)
            {
                grenadeUI.SetActive(true);
                grenadeText.SetText($"x{grenadesLeft}");
            }

            Invoke("HideGrenadeUI", .5f);
            am.Play("grenade throw");
            GameObject g = Instantiate(grenade, transform.position, Quaternion.identity);
            Rigidbody2D grb = g.GetComponent<Rigidbody2D>();
            float angle = transform.rotation.eulerAngles.z;
            grb.AddForce(transform.right*force);
        }
    }

    void HideGrenadeUI()
	{
        if (grenadeUI != null) grenadeUI.SetActive(false);
    }

    public static void ResetGrenades()
	{
        if (me == null) return;
        me.grenadesLeft = me.startingGrenades;

    }
}
