using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title_SceneChange : MonoBehaviour
{
    public  void    ChangeNewGameScene()
    {
        SceneManager.LoadScene("Village0");
    }

    public void ChangeLoadGameScene()
    {

    }

    public void ChangeExitGameScene()
    {
        Quit();
    }
    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //play모드를 false로.
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); //구글웹으로 전환
#else
        Application.Quit(); //어플리케이션 종료
#endif
    }
}
