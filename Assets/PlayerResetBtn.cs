using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResetBtn : MonoBehaviour
{
    Player Player => Player.Instance;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if(Player.PlayerResetEntity != null)
            {
                Player.PlayerResetEntity.ResetToDeafultPoint();
            }
        });
    }
}
