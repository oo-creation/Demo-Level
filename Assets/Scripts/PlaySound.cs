using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{

	private static AudioSource _audioSource;
	private void Start()
	{
		_audioSource = GetComponent<AudioSource>();
	}

	public static void Play()
	{
		_audioSource.Play();
	}
}
