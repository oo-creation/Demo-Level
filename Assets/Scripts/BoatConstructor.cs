using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BoatConstructor : MonoBehaviour
{
    public GameObject Boat;
    public GameObject Paddle1;
    public GameObject Paddle2;

    [HideInInspector] public bool BoatComplete;

    private AudioSource _audioSource;
    
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        BoatComplete = true; //TODO set to false and disable boat
    }

    public void ConstructStep()
    {
        if (!Boat.activeSelf)
        {
            Boat.SetActive(true);
        }
        else if (!Paddle1.activeSelf)
        {
            Paddle1.SetActive(true);
        }
        else if (!Paddle2.activeSelf)
        {
            Paddle2.SetActive(true);
        }
        else
        {
            _audioSource.Play();
            BoatComplete = true;
        }
    }
}