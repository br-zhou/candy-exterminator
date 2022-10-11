using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class toggle_music : MonoBehaviour
{
    public Sprite unmute;
    public Sprite mute;
    static bool musicOn = true;
	music music_script;
	public static GameObject MUSIC;
	Button b;
	AudioSource musicAS;
	float originalVolume;
	private void Start()
	{
		music_script = FindObjectOfType<music>();
		b = GetComponent<Button>();
		if(music_script != null) MUSIC = music_script.gameObject;
		if(MUSIC != null) musicAS = MUSIC.GetComponent<AudioSource>();
		if(musicOn) b.image.sprite = unmute;
		else b.image.sprite = mute;
	}
	public void ToggleMusic()
	{
		if (MUSIC == null) return;
		if (musicOn)
		{
			musicAS.enabled = false;
			b.image.sprite = mute;
		}
		else
		{
			musicAS.enabled = true;
			b.image.sprite = unmute;
		}
		musicOn = musicAS.enabled;
	}
}
