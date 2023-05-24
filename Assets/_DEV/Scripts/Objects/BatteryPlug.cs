using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryPlug : MonoBehaviour
{
    private bool b_MoveBattery = false;
    public bool b_MoveBatteryDone = false;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (transform.localPosition == new Vector3(0,0,0))
        {
            rb.useGravity = false;
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }

    }

    void Update()
    {
        if (b_MoveBattery && !b_MoveBatteryDone)
        {
            float step = 2 * Time.deltaTime;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, new Vector3(0,0,0), step);

            if (transform.localPosition == new Vector3(0,0,0))
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
                b_MoveBatteryDone = true;
            }
        if (b_MoveBatteryDone)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            transform.localPosition = new Vector3(0,0,0);
        }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject == transform.parent.transform.parent.gameObject)
        {
            b_MoveBattery = true;
            rb.useGravity = false;
        }
    }
}
