using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class CorridorBuilder : MonoBehaviour
{
    [Header("Спрайты")]
    public Sprite floorSprite;
    public Sprite wallSprite;

    [Header("Параметры размеров")]
    public float floorHeight = 1f;
    public float floorThickness = 1f;
    public float wallThickness = 0.2f;
    public float wallOffset = 0.5f;

    [ContextMenu("Собрать Коридор")]
    public void BuildCorridor()
    {
        // Удаляем старые дети
        foreach (Transform child in transform)
        {
#if UNITY_EDITOR
            DestroyImmediate(child.gameObject);
#else
            Destroy(child.gameObject);
#endif
        }

        CreatePart("Floor", floorSprite, Vector3.zero, new Vector3(1f, floorHeight, 1f));

        CreatePart("WallLeft", wallSprite, new Vector3(0, wallOffset, 0), new Vector3(1f, wallThickness, 1f));
        CreatePart("WallRight", wallSprite, new Vector3(0, -wallOffset, 0), new Vector3(1f, wallThickness, 1f));
    }

    private void CreatePart(string name, Sprite sprite, Vector3 position, Vector3 scale)
    {
        GameObject part = new GameObject(name);
        part.transform.parent = transform;
        part.transform.localPosition = position;
        part.transform.localScale = scale;

        var sr = part.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 0;
    }
}
