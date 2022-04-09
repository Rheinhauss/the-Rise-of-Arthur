
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using PixelCrushers.DialogueSystem;

namespace PixelCrushers.DialogueSystem.SequencerCommands
{

    public class SequencerCommandScene : SequencerCommand
    {
        public void Start()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }

    }

}
