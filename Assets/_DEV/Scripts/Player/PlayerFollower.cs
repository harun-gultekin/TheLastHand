using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] float y_distance = 5f;
    [SerializeField] float z_distance = 3f;
    [SerializeField] GameObject player;

    void Start()
    {
        setPositionRespectToPlayer();
    }

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