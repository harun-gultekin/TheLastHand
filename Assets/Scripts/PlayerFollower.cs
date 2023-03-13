using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] float y_distance = 5f;
    [SerializeField] float z_distance = 3f;
    [SerializeField] GameObject player;
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
        Vector3 CameraPosition = player.transform.position;
        CameraPosition.y += y_distance;
        CameraPosition.z -= z_distance;
        transform.position = CameraPosition;
    }
}
