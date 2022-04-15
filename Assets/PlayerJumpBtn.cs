using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpBtn : MonoBehaviour
{
    Player Player => Player.Instance;

    public Transform Point;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Player.PlayerResetEntity != null)
            {
                if(Point != null)
                {
                    Player.PlayerResetEntity.ResetTransform(Point);
                }
            }
        });
    }

}
