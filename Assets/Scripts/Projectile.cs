using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Projectile : MonoBehaviour
{
    private float m_LightPower;
    public float lightPower => m_LightPower;

    private Player m_Player;
    public Player player => m_Player;

    [SerializeField] private float spead;

    [SerializeField] private AudioSource audioSourcePrefab;

    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] Slider slider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInChildren<Meteorite>() != null)
        {
            if (other.GetComponentInChildren<Light>().color == GetComponentInChildren<Light>().color)
            {
                AudioSource newAudioSource = Instantiate<AudioSource>(audioSourcePrefab);
                newAudioSource.clip = audioClips[0];
                newAudioSource.volume = slider.value / 4;
                newAudioSource.Play();
                Destroy(newAudioSource.gameObject, 1.2f);
                m_Player.AddScore(m_LightPower * other.GetComponentInChildren<Collected>().score);
                Destroy(other.gameObject);
                Destroy(this.gameObject);
            }
        }
    }

    public void SetSlider(Slider setSlider)
    {
        slider = setSlider;
    }

    public void SetLighPower(float setLightPower)
    {
        m_LightPower = setLightPower;
        GetComponentInChildren<Light>().intensity = m_LightPower;
    }

    public void SetPlayer(Player setPlayer)
    {
        m_Player = setPlayer;
        GetComponentInChildren<Light>().color = player.GetComponentInChildren<Light>().color;
    }

    private void Move(Vector3 moveVector)
    {
        transform.Translate(moveVector.normalized * spead * Time.deltaTime);
    }

    private void Update()
    {
        Move(transform.up);
    }
}
