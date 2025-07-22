using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] private Transform playerPos;

    void Update()
    {
        CameraMovement();
    }
    private void CameraMovement()
    {
        Vector3 playerPosition = new Vector3(playerPos.position.x, playerPos.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, speed * Time.deltaTime);
    }
}
