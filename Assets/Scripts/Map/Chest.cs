using TMPro;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] GameObject coinsHintPrefab;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            int coins = Random.Range(3, 7);
            FindFirstObjectByType<CoinManager>().AddCoins(coins);
            GameObject coinsHint = Instantiate(coinsHintPrefab, transform.position, Quaternion.identity);
            coinsHint.GetComponentInChildren<TextMeshPro>().text = $"+ {coins}";
            Destroy(gameObject);
        }
    }
}
