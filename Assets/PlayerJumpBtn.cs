using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerJumpBtn : MonoBehaviour
{
    Player Player => Player.Instance;

    [Tooltip("跳转场景点的name")]
    public string Point;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (Player.PlayerResetEntity != null)
            {
                GameObject gameObject = GameObject.Find(Point);
                if (gameObject != null)
                {
                    Player.PlayerResetEntity.ResetTransform(gameObject);
                }
            }
        });
    }

}
