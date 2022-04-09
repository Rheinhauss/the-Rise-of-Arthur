using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //退出游戏
    public void quitGame()
    {
        Application.Quit();
    }
    //开始游戏      场景1
    public void startGame()
    {
        StartCoroutine(LoadScene(1));
    }

    public void Menu()
    {
        StartCoroutine(LoadScene(1));
    }
    IEnumerator LoadScene(int index)
    {
        yield return new WaitForSeconds(0.7f);
        AsyncOperation async = SceneManager.LoadSceneAsync(index);
    }
}
