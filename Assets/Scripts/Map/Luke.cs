using RogueSharpTutorial.Controller;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Luke : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadingScreenManager.Instance.LoadSceneWithLoading("SampleScene");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}
