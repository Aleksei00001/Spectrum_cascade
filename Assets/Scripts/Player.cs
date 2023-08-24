using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textScore;

    [SerializeField] private float spead;

    private Vector3 moveVector;

    private float m_Score;
    public float score => m_Score;

    private float lihtPower = 3;

    [SerializeField] private Projectile projectilePrefab;

    [SerializeField] private Leaderboard leaderboard;

    private bool active = false;

    [SerializeField] private GameStateManager gameStateManager;

    [SerializeField] private AudioSource audioSourcePrefab;

    [SerializeField] private AudioClip[] audioClips;

    [SerializeField] Slider slider;

    public void SetActive(bool setActive)
    {
        if (setActive == true)
        {
            active = true;
            transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            active = false;
            transform.position = new Vector3(999, 999, 999);
        }
    }

    public void SetZero()
    {
        m_Score = 0;
        textScore.text = ((int)m_Score).ToString();
        lihtPower = 3;
        GetComponentInChildren<SpriteRenderer>().color = Color.white;
        transform.GetComponentInChildren<Light>().color = Color.white;
    }

    public void AddScore(float addScore)
    {
        m_Score += addScore;
    }

    private void Move(Vector3 moveVector)
    {
        transform.
        transform.Translate(moveVector.normalized * spead * Time.deltaTime);
    }

    private void Update()
    {
        moveVector = Vector3.zero;

        if (active == true)
        {
            if (Input.GetKey(KeyCode.W))
            {
                if (transform.position.y < 4.4)
                {
                    moveVector += transform.up;
                }
            }
            if (Input.GetKey(KeyCode.A))
            {
                if (transform.position.x > -8.4)
                {
                    moveVector += -transform.right;
                }
            }
            if (Input.GetKey(KeyCode.S))
            {
                if (transform.position.y > -4.4)
                {
                    moveVector += -transform.up;
                }
            }
            if (Input.GetKey(KeyCode.D))
            {
                if (transform.position.x < 8.4)
                {
                    moveVector += transform.right;
                }
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Projectile newProjectile = Instantiate<Projectile>(projectilePrefab);
                newProjectile.SetLighPower(lihtPower * 0.1f);
                newProjectile.SetPlayer(this);
                newProjectile.transform.position = transform.position;
                newProjectile.SetSlider(slider);

                AudioSource newAudioSource = Instantiate<AudioSource>(audioSourcePrefab);
                newAudioSource.clip = audioClips[3];
                newAudioSource.volume = slider.value / 4;
                newAudioSource.Play();
                Destroy(newAudioSource.gameObject, 1.2f);

                lihtPower = lihtPower * 0.9f;
            }

            Move(moveVector);

            textScore.text = ((int)m_Score).ToString();
        }
    }

    private void FixedUpdate()
    {
        lihtPower = lihtPower * 0.9995f;
        GetComponentInChildren<Light>().intensity = lihtPower;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Collected>() != null)
        {
            if (other.GetComponent<Particle>() != null)
            {
                //m_Score += other.GetComponent<Collected>().score;
                if (other.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    if (other.GetComponentInChildren<SpriteRenderer>().color == new Color32(255,0,0,255))
                    {
                        AudioSource newAudioSource = Instantiate<AudioSource>(audioSourcePrefab);
                        newAudioSource.clip = audioClips[0];
                        newAudioSource.volume = slider.value / 2;
                        newAudioSource.Play();
                        Destroy(newAudioSource.gameObject, 1.2f);
                    }
                    else if (other.GetComponentInChildren<SpriteRenderer>().color == new Color32(0, 255, 0, 255))
                    {
                        AudioSource newAudioSource = Instantiate<AudioSource>(audioSourcePrefab);
                        newAudioSource.clip = audioClips[1];
                        newAudioSource.volume = slider.value / 2;
                        newAudioSource.Play();
                        Destroy(newAudioSource.gameObject, 1.2f);
                    }
                    else if (other.GetComponentInChildren<SpriteRenderer>().color == new Color32(0, 0, 255, 255))
                    {
                        AudioSource newAudioSource = Instantiate<AudioSource>(audioSourcePrefab);
                        newAudioSource.clip = audioClips[2];
                        newAudioSource.volume = slider.value / 2;
                        newAudioSource.Play();
                        Destroy(newAudioSource.gameObject, 1.2f);
                    }

                    if (GetComponentInChildren<SpriteRenderer>().color != other.GetComponentInChildren<SpriteRenderer>().color)
                    {
                        lihtPower += 1;
                    }
                    GetComponentInChildren<SpriteRenderer>().color = other.GetComponentInChildren<SpriteRenderer>().color;
                    transform.GetComponentInChildren<Light>().color = other.GetComponentInChildren<SpriteRenderer>().color;
                }
                Destroy(other.GetComponent<Collected>().gameObject);
            }
            if (other.GetComponent<Meteorite>() != null)
            {
                StartCoroutine(DieRoutine());
                gameStateManager.SetStopGame();
            }
        }
    }

    IEnumerator DieRoutine()
    {
        Debug.Log(score);
        yield return leaderboard.SubmitScoreRoutine((int)m_Score);
    }
}
