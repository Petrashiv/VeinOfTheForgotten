using UnityEngine;

public class Room : MonoBehaviour
{
    public bool doorTop;
    public bool doorBottom;
    public bool doorLeft;
    public bool doorRight;

    public bool HasDoor(Vector2Int direction)
    {
        if (direction == Vector2Int.up) return doorTop;
        if (direction == Vector2Int.down) return doorBottom;
        if (direction == Vector2Int.left) return doorLeft;
        if (direction == Vector2Int.right) return doorRight;
        return false;
    }
}
