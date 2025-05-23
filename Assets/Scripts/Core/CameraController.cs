using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float transitionSpeed = 5f;

    private Room currentRoom;
    private Vector3 targetPosition;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        targetPosition = transform.position;
    }

    void Update()
    {
        if (currentRoom != null)
        {
            targetPosition = new Vector3(
                currentRoom.transform.position.x,
                currentRoom.transform.position.y,
                transform.position.z
            );
        }

        // Плавное движение камеры
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * transitionSpeed);
    }

    public void SetCurrentRoom(Room room)
    {
        currentRoom = room;
    }
}
