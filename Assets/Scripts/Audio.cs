using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    AudioSource ac;
    void Start()
    {
        ac = GetComponent<AudioSource>();
    }

    void OnMouseDown()
    {
        ac.PlayOneShot(ac.clip);
    }
}