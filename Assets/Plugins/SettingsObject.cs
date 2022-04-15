using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsObject : MonoBehaviour
{
    public static SettingsObject Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            Instance = this;
            this.gameObject.SetActive(false);
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
