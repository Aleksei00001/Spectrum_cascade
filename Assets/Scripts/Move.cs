using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] GameObject[] moveObject;

    [SerializeField] float spead;

    private void FixedUpdate()
    {
        for (int i = 0; i < moveObject.Length; i++)
        {
            moveObject[i].transform.Translate(Vector3.forward * spead * Time.deltaTime);
        }
    }
}
