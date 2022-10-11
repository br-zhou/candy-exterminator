using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class sound
{

	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume = .75f;


	public bool loop = false;

	public bool spacial = true;

	[HideInInspector]
	public AudioSource source;

}
