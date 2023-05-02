using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Satelite : MonoBehaviour
{
    public GameObject moon;
    public GameObject pivotPlanet;
    public float speed = 2f;
    //private float earthMass;
    public static Rigidbody playerRigidBody;
    private float G = 100f;
    private float dist;
    public Vector3 axis = new Vector3(0f,1f,0f);
 
    // Start is called before the first frame update
    void Start()
    {
        dist = Vector3.Distance(pivotPlanet.transform.position, moon.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
            //Assumes Pivot Planets Mass to be 1, keeps the ratio of G/distance.
            moon.transform.RotateAround(pivotPlanet.transform.position,axis, speed * Mathf.Sqrt(G/dist) * Time.deltaTime);
    }
}
