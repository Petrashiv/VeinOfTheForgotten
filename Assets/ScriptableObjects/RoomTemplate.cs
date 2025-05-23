using UnityEngine;

[CreateAssetMenu(menuName = "RogueLike/Room Template")]
public class RoomTemplate : ScriptableObject
{
    public GameObject roomPrefab;
    public Vector2Int size;
}
