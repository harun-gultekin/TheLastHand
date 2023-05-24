using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySocketCheck : MonoBehaviour
{
    public bool b_AllBatteriesPlugged = false;
    private GameObject[] allChidren;
    private BatteryPlug[] bpScripts;

    void Start()
    {
        bpScripts = GetComponentsInChildren<BatteryPlug>();
    }

    // Update is called once per frame
    void Update()
    {
        b_AllBatteriesPlugged = CheckIfAllReset();
    }

    private bool CheckIfAllReset()
    {
        foreach(BatteryPlug bp in bpScripts)
        {
            if(!bp.b_MoveBatteryDone)
            {
                return false;
            }
        }

        Debug.Log("Batteries Plugged");
        return true;
        }
}
