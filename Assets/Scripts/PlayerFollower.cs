using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        setPositionRespectToPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        setPositionRespectToPlayer();
    }
    void setPositionRespectToPlayer()
    {
        GameObject player = GameObject.Find("Player");
        Vector3 CameraPosition = player.transform.position;
        CameraPosition.y += 3;
        CameraPosition.z -= 5;
        transform.position = CameraPosition;
    }
}
