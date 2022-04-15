using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCheatToggle : MonoBehaviour
{
    public enum CheatType
    {
        [LabelText("�޵�")]
        Invincible,
        [LabelText("���ӹ���")]
        AttackPower,
        [LabelText("�����ƶ��ٶ�")]
        Speed
    }

    Player Player => Player.Instance;
    public CheatType cheatType;
    /// <summary>
    /// �ܷ�Ӧ��
    /// </summary>
    private bool CanApply = false;
    /// <summary>
    /// �Ƿ�Ӧ��
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
    /// �����޵�
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
    /// ���ù�����
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
    /// �����ƶ��ٶ�
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
