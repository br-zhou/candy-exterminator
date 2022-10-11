using UnityEngine.Audio;
using System;
using UnityEngine;

public class music : MonoBehaviour
{

	public static music instance;

	public AudioMixerGroup mixerGroup;

	public sound[] sounds;

	void Awake()
	{
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			//toggle_music.MUSIC = this.gameObject;
			DontDestroyOnLoad(gameObject);
		}

		foreach (sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;

			s.source.outputAudioMixerGroup = mixerGroup;
		}
	}

	public void Play(string sound)
	{
		sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("Sound: " + name + " not found!");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = 1f;

		s.source.Play();
	}

}
