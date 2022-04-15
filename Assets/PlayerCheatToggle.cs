using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCheatToggle : MonoBehaviour
{
    public enum CheatType
    {
        [LabelText("无敌")]
        Invincible,
        [LabelText("增加攻击")]
        AttackPower,
        [LabelText("增加移动速度")]
        Speed
    }

    Player Player => Player.Instance;
    public CheatType cheatType;
    /// <summary>
    /// 能否应用
    /// </summary>
    private bool CanApply = false;
    /// <summary>
    /// 是否应用
    /// </summary>
    private bool IsApplyed = false;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Toggle>().onValueChanged.AddListener((b) =>
        {
            CanApply = b;
            switch (cheatType)
            {
                case CheatType.Invincible:
                    SetInvincible(b);
                    break;
                case CheatType.AttackPower:
                    SetAttackPower(b);
                    break;
                case CheatType.Speed:
                    SetSpeed(b);
                    break;
            }
        });
    }

    private void Update()
    {
        if(CanApply && !IsApplyed)
        {
            switch (cheatType)
            {
                case CheatType.Invincible:
                    SetInvincible(CanApply);
                    break;
                case CheatType.AttackPower:
                    SetAttackPower(CanApply);
                    break;
                case CheatType.Speed:
                    SetSpeed(CanApply);
                    break;
            }
        }
    }

    /// <summary>
    /// 设置无敌
    /// </summary>
    /// <param name="b"></param>
    public void SetInvincible(bool b)
    {
        if (Player != null)
        {
            Player.combatEntity.IsInvincibel = b;
            IsApplyed = b;
        }
    }

    /// <summary>
    /// 设置攻击力
    /// </summary>
    /// <param name="b"></param>
    public void SetAttackPower(bool b)
    {
        if (Player != null)
        {
            if (b)
            {
                Player.combatEntity.UnitPropertyEntity.AttackPower.Add(1000);
            }
            else
            {
                Player.combatEntity.UnitPropertyEntity.AttackPower.Minus(1000);

            }
            IsApplyed = b;
        }

    }

    /// <summary>
    /// 设置移动速度
    /// </summary>
    /// <param name="b"></param>
    public void SetSpeed(bool b)
    {
        if (Player != null)
        {
            if (b)
            {
                Player.combatEntity.UnitPropertyEntity.MoveSpeed.Add(10);
                IsApplyed = true;
            }
            else
            {
                Player.combatEntity.UnitPropertyEntity.MoveSpeed.Minus(10);
            }
            IsApplyed = b;
        }
    }

}
