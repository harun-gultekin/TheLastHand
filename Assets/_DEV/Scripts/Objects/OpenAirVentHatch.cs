using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenAirVentHatch : MonoBehaviour
{
    private bool b_moveActive = false;
    private bool b_moveDone = false;
    private bool b_open = false;

    // Start is called before the first frame update
    void Start()
    {
        b_moveActive = false;
        b_moveDone = false;
        b_open = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (b_moveActive && Input.GetKeyDown(KeyCode.E) && !b_moveDone)
        {
            b_open = true;
        }

        if (b_open)
        {
            //this.transform.parent.transform.localPosition = Vector3.MoveTowards(this.transform.parent.transform.localPosition, new Vector3(0,13.85f,11.25f), 3 * Time.deltaTime);
            //this.transform.parent.transform.rotation = Vector3.RotateTowards(this.transform.parent.transform.rotation, rotation, 90 * Time.deltaTime);
            this.transform.parent.transform.RotateAround(transform.position, this.transform.parent.transform.right, 90 * Time.deltaTime);

            Debug.Log(this.transform.parent.transform.rotation.eulerAngles.x);


            if (this.transform.parent.transform.localEulerAngles.x >= 80 && this.transform.parent.transform.localEulerAngles.x < 90)
            {
                b_open = false;
                b_moveDone = true;
            }
        }
        
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            b_moveActive = true;
        }

    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            b_moveActive = false;
        }
    }
}
