using UnityEngine;

public class MainMenuManager : MonoBehaviour
{
    public void NewGameButton()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("PreGame");

    }
    public void ContinueButton()
    {
        LoadingScreenManager.Instance.LoadSceneWithLoading("PreGame");

    }
    public void SettingsButton()
    {
        

    }
    public void ExitButton()
    {
        

    }
}
