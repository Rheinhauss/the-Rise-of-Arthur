using DG.Tweening;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : UnitControllerComponent
{
    /// <summary>
    /// ״̬���
    /// </summary>
    public Transform StatusSlotsTrm;
    public GameObject StatusIconPrefab;
    /// <summary>
    /// ѣ��Ч��
    /// </summary>
    private GameObject vertigoParticle;
    /// <summary>
    /// ����Ч��
    /// </summary>
    private GameObject weakParticle;

    /// <summary>
    /// Player����UI�Ŀ���
    /// </summary>
    public EnemyUIController EnemyUIController;
    private void Update()
    {
        this.transform.Rotate(-transform.rotation.eulerAngles);
    }
    public void Init()
    {

        // ��ʼ��CombatEntity
        // ���ظ������
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.Position = transform.position;
        combatEntity.AddComponent<MotionComponent>();
        // Action��������
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveDamage, OnReceiveDamage);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveCure, OnReceiveCure);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveStatus, OnReceiveStatus);
        combatEntity.Subscribe<RemoveStatusEvent>(OnRemoveStatus);
    }
    /// <summary>
    /// ��������Action����������UI
    /// </summary>
    /// <param name="combatAction">Actionִ����</param>
    public override void OnReceiveCure(ActionExecution combatAction)
    {
        var cureAction = combatAction as CureAction;
        EnemyUIController._HP.OnReceiveCure(combatEntity.UnitPropertyEntity.HP.Percent(), cureAction.CureValue);
    }
    /// <summary>
    /// �����˺�Action����������UI
    /// </summary>
    /// <param name="combatAction">Actionִ����</param>
    public override void OnReceiveDamage(ActionExecution combatAction)
    {
        var damageAction = combatAction as DamageAction;
        EnemyUIController._HP.OnReceiveDamage(combatEntity.UnitPropertyEntity.HP.Percent(), damageAction.DamageValue);
        //EnemyDeathEntity.CheckDeath();
    }

    /// <summary>
    /// ������״̬
    /// </summary>
    /// <param name="combatAction"></param>
    public override void OnReceiveStatus(ActionExecution combatAction)
    {
        #region ״̬�ں�
        #endregion

//        var action = combatAction as AddStatusAction;
//        var addStatusEffect = action.AddStatusEffect;
//#if EGAMEPLAY_EXCEL
//        var statusConfig = addStatusEffect.AddStatusConfig;
//#else
//        var statusConfig = addStatusEffect.AddStatus;
//#endif

        //// �������ƣ����Ч��չʾ
        //if (name == "Monster")
        //{
        //    var obj = GameObject.Instantiate(StatusIconPrefab);
        //    obj.transform.SetParent(StatusSlotsTrm);
        //    obj.GetComponentInChildren<Text>().text = statusConfig.Name;
        //    obj.name = action.Status.Id.ToString();
        //}
        //// ѣ��
        //if (statusConfig.Name == "Vertigo")
        //{
        //    //unitAnimatorComponent.AnimancerComponent.Play();
        //    if (vertigoParticle == null)
        //    {
        //        vertigoParticle = GameObject.Instantiate(statusConfig.GetParticleEffect());
        //        vertigoParticle.transform.parent = transform;
        //        vertigoParticle.transform.localPosition = new Vector3(0, 2, 0);
        //    }
        //}
        //// ����
        //if (statusConfig.Name == "Weak")
        //{
        //    if (weakParticle == null)
        //    {
        //        weakParticle = GameObject.Instantiate(statusConfig.GetParticleEffect());
        //        weakParticle.transform.parent = transform;
        //        weakParticle.transform.localPosition = new Vector3(0, 0, 0);
        //    }
        //}
    }
    /// <summary>
    /// �Ƴ�״̬
    /// </summary>
    /// <param name="eventData"></param>
    public override void OnRemoveStatus(RemoveStatusEvent eventData)
    {
        base.OnRemoveStatus(eventData);
        if (name == "Monster")
        {
            var trm = StatusSlotsTrm.Find(eventData.StatusId.ToString());
            if (trm != null)
            {
                GameObject.Destroy(trm.gameObject);
            }
        }

        var statusConfig = eventData.Status.StatusConfig;
        if (statusConfig.Name == "Vertigo")
        {
            //unitAnimatorComponent.AnimancerComponent.Play();
            if (vertigoParticle != null)
            {
                GameObject.Destroy(vertigoParticle);
            }
        }
        if (statusConfig.Name == "Weak")
        {
            if (weakParticle != null)
            {
                GameObject.Destroy(weakParticle);
            }
        }
    }
}
