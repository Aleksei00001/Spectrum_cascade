using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    [SerializeField] private float spead;

    [SerializeField] private Color32 color;

    private void Move(Vector3 moveVector)
    {
        transform.Translate(moveVector.normalized * spead * Time.deltaTime);
    }

    private void Update()
    {
        Move(-transform.up);
    }

    private void Start()
    {
        int numberColor = Random.Range(0, 3);
        if (numberColor == 0)
        {
            color = new Color32(255, 0, 0, 255);
        }
        else if (numberColor == 1)
        {
            color = new Color32(0, 255, 0, 255);
        }
        else if (numberColor == 2)
        {
            color = new Color32(0, 0, 255, 255);
        }

        transform.GetComponentInChildren<SpriteRenderer>().color = color;
        transform.GetComponentInChildren<Light>().color = color;
    }

    public void SetSpead(float setSpead)
    {
        spead = setSpead;
    }
}
