using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefab;

    [SerializeField] private float[] frequencyCreation;

    private float[] timeLayerCreation;

    private float speadUp;

    [SerializeField] private Player player;

    private void Start()
    {
        timeLayerCreation = new float[frequencyCreation.Length];
    }
    
    private void FixedUpdate()
    {
        speadUp += Time.fixedDeltaTime;
        for (int i = 0; i < timeLayerCreation.Length; i++)
        {
            timeLayerCreation[i] += Time.fixedDeltaTime * (1 * Mathf.Pow(speadUp, 1f / 1.4f) * Mathf.Pow(player.score + 1, 1f / 10f));

            if (Random.Range(0f,1f) <= (timeLayerCreation[i] / frequencyCreation[i]))
            {
                timeLayerCreation[i] = 0;
                GameObject newPrefab = Instantiate<GameObject>(objectPrefab[i]);
                newPrefab.transform.position = new Vector3(Random.Range(-8f, 8f), 10f, 0.5f);
                if (newPrefab.GetComponentInChildren<Meteorite>() != null)
                {
                    newPrefab.GetComponentInChildren<Meteorite>().SetSpead(Random.Range(4f + (0.7f * Mathf.Pow(speadUp, 1f / 3f) * Mathf.Pow(player.score + 1, 1f / 20f)), 4f + (1.1f * Mathf.Pow(speadUp, 1f / 3f) * Mathf.Pow(player.score + 1f, 1f / 20f))));
                }
                else if (newPrefab.GetComponentInChildren<Particle>() != null)
                {
                    newPrefab.GetComponentInChildren<Particle>().SetSpead(Random.Range(4f + (0.7f * Mathf.Pow(speadUp, 1f / 3f) * Mathf.Pow(player.score + 1, 1f / 20f)), 4f + (1.1f * Mathf.Pow(speadUp, 1f / 3f) * Mathf.Pow(player.score + 1f, 1f / 20f))));
                }
            }
        }
    }

    public void SetZero()
    {
        speadUp = 0;
    }
}
