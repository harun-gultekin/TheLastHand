using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneHolder : MonoBehaviour
{
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Box")) 
        { 
            collision.gameObject.transform.SetParent(transform);
            Debug.Log("box");
        }
    }
}