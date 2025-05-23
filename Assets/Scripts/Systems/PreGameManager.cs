using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class PreGameManager : MonoBehaviour
{

    public void Retry()
    {
        Time.timeScale = 1f;
        LoadingScreenManager.Instance.LoadSceneWithLoading("SampleScene");
        /*Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);*/
    }
}
