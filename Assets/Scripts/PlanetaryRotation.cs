using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlanetaryRotation : MonoBehaviour
{
    public float rotationSpeed = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.up, rotationSpeed * Time.deltaTime, Space.Self);
    }
}
