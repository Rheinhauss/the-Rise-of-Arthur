using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    //�˳���Ϸ
    public void quitGame()
    {
        Application.Quit();
    }
    //��ʼ��Ϸ      ����1
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
