using UnityEngine.Audio;
using System;
using UnityEngine;

public class audio_manager : MonoBehaviour
{

	
	public sound[] sounds;

	void Awake()
	{


		foreach (sound s in sounds)
		{
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
			if(s.spacial){
				s.source.rolloffMode = AudioRolloffMode.Linear;
				s.source.spatialBlend = 1;
				s.source.dopplerLevel = 0f;
				s.source.maxDistance = 25;
				s.source.minDistance = 1;

			}

		}
	}

	public void Play(string sound)
	{
		sound s = Array.Find(sounds, item => item.name == sound);
		if (s == null)
		{
			Debug.LogWarning("sound: " + name + " not found");
			return;
		}

		s.source.volume = s.volume;
		s.source.pitch = 1f;

		s.source.Play();
	}


}
