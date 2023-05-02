using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningArea : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerExit(Collider other) {

        if(other.CompareTag("Player"))
        {
            Debug.Log("Turn Back or you Will go out of bounds");
        }
    }
}
