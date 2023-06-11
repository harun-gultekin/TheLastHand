using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatterySocketCheck : MonoBehaviour
{
    public bool b_AllBatteriesPlugged;
    private GameObject[] allChidren;
    private BatteryPlug[] bpScripts;

    void Start()
    {
        bpScripts = GetComponentsInChildren<BatteryPlug>();
    }

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
        
        if (transform.gameObject.name == "PressMachineBattery")
        {
            PressMachineManager.Instance.triggerActive = true;
        }
        return true;
    }
}
