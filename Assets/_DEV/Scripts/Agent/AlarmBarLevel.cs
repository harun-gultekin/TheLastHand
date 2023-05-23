using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlarmBarLevel : MonoBehaviour
{
    // Start is called before the first frame update
    public AgentOliviaScript theAgentOliviaScript;
    public AgentAlexNavMesh theAgentAlexNavMesh;
    private float alarmBarLevelx;
    public Vector3 alarmBarLevel;

    void Start()
    {    
        alarmBarLevelx = 0;
        /*
            // !=  --> xor şeklinde çalışır
            if       ((theAgentOliviaScript.playerInSightRange != theAgentAlexNavMesh.playerInSightRange) &&  !theAgentOliviaScript.thePlayerInEnemyFOV && !theAgentAlexNavMesh.thePlayerInEnemyFOV )    alarmBarLevelx = 0.20f;
            else if  ( theAgentOliviaScript.playerInSightRange && theAgentAlexNavMesh.playerInSightRange  &&  !theAgentOliviaScript.thePlayerInEnemyFOV && !theAgentAlexNavMesh.thePlayerInEnemyFOV )     alarmBarLevelx = 0.40f;
            else if  ( theAgentOliviaScript.playerInSightRange && theAgentAlexNavMesh.playerInSightRange  && (!theAgentOliviaScript.thePlayerInEnemyFOV != !theAgentAlexNavMesh.thePlayerInEnemyFOV))     alarmBarLevelx = 0.40f;
            else if  ( theAgentOliviaScript.playerInSightRange && theAgentAlexNavMesh.playerInSightRange  &&  !theAgentOliviaScript.thePlayerInEnemyFOV && !theAgentAlexNavMesh.thePlayerInEnemyFOV )     alarmBarLevelx = 0.40f;
        */
    }

    // Update is called once per frame
    void Update()
    {
        if       ( !theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.00f;
        else if  ( !theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.20f;
        else if  ( !theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.20f;
        else if  ( !theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.40f;
        
        else if  ( !theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.40f;
        else if  ( !theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.80f;
        else if  ( !theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.80f;
        else if  ( !theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.90f;

        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.40f;
        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.80f;
        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.80f;
        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&  !theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.90f;
        
        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.60f;
        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&  !theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.85f;
        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&  !theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.85f;
        else if  (  theAgentOliviaScript.thePlayerInEnemyFOV &&   theAgentAlexNavMesh.thePlayerInEnemyFOV  &&   theAgentOliviaScript.playerInSightRange &&   theAgentAlexNavMesh.playerInSightRange )     alarmBarLevelx = 0.95f;
        
        if  (  theAgentOliviaScript.playerInBustedRange ||   theAgentAlexNavMesh.playerInBustedRange)     alarmBarLevelx = 1.00f;

        alarmBarLevel = new Vector3 (alarmBarLevelx, 1, 1);
    }

}
