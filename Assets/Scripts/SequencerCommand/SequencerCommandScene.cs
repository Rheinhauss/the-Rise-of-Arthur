
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;
using System;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandScene : SequencerCommand
    {
        public void Start()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            if (Player.Instance != null)
            {
                Player.Instance.SwitchScene();
            }
            //SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            //StartCoroutine(waitForLevelToLoad(SceneManager.GetActiveScene().buildIndex + 1));
        }

        private IEnumerator waitForLevelToLoad(int index)
        {
            while (SceneManager.GetActiveScene().buildIndex != index)
            {
                //Debug.Log("loading scene:" + SceneManager.GetActiveScene().name);
                yield return null;
            }
        }

    }

}
