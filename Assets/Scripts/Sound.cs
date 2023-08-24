using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sound : MonoBehaviour
{
    [SerializeField] AudioClip[] audioClips;

    [SerializeField] AudioSource audioSource;

    [SerializeField] Slider slider;

    private float timer;

    private void Start()
    {
        audioSource.clip = audioClips[0];
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        timer += Time.fixedDeltaTime;
        if (timer >= 1.2f)
        {
            audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
            audioSource.Play();
            timer = 0;
        }

        audioSource.volume = slider.value;
    }
}
