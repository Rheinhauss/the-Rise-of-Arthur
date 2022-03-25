using DG.Tweening;
using EGamePlay.Combat;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : UnitControllerComponent
{
    /// <summary>
    /// 状态组件
    /// </summary>
    public Transform StatusSlotsTrm;
    public GameObject StatusIconPrefab;
    /// <summary>
    /// 眩晕效果
    /// </summary>
    private GameObject vertigoParticle;
    /// <summary>
    /// 虚弱效果
    /// </summary>
    private GameObject weakParticle;

    /// <summary>
    /// Player所有UI的控制
    /// </summary>
    public EnemyUIController EnemyUIController;
    private void Update()
    {
        this.transform.Rotate(-transform.rotation.eulerAngles);
    }
    public void Init()
    {

        // 初始化CombatEntity
        // 挂载各种组件
        combatEntity = CombatContext.Instance.AddChild<CombatEntity>();
        CombatContext.Instance.Object2Entities.Add(gameObject, combatEntity);
        combatEntity.Position = transform.position;
        combatEntity.AddComponent<MotionComponent>();
        // Action监听函数
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveDamage, OnReceiveDamage);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveCure, OnReceiveCure);
        combatEntity.ListenActionPoint(ActionPointType.PostReceiveStatus, OnReceiveStatus);
        combatEntity.Subscribe<RemoveStatusEvent>(OnRemoveStatus);
    }
    /// <summary>
    /// 接受治愈Action，进行跳字UI
    /// </summary>
    /// <param name="combatAction">Action执行体</param>
    public override void OnReceiveCure(ActionExecution combatAction)
    {
        var cureAction = combatAction as CureAction;
        EnemyUIController._HP.OnReceiveCure(combatEntity.UnitPropertyEntity.HP.Percent(), cureAction.CureValue);
    }
    /// <summary>
    /// 接受伤害Action，进行跳字UI
    /// </summary>
    /// <param name="combatAction">Action执行体</param>
    public override void OnReceiveDamage(ActionExecution combatAction)
    {
        var damageAction = combatAction as DamageAction;
        EnemyUIController._HP.OnReceiveDamage(combatEntity.UnitPropertyEntity.HP.Percent(), damageAction.DamageValue);
        //EnemyDeathEntity.CheckDeath();
    }

    /// <summary>
    /// 被附加状态
    /// </summary>
    /// <param name="combatAction"></param>
    public override void OnReceiveStatus(ActionExecution combatAction)
    {
        #region 状态融合
        #endregion

//        var action = combatAction as AddStatusAction;
//        var addStatusEffect = action.AddStatusEffect;
//#if EGAMEPLAY_EXCEL
//        var statusConfig = addStatusEffect.AddStatusConfig;
//#else
//        var statusConfig = addStatusEffect.AddStatus;
//#endif

        //// 根据名称，添加效果展示
        //if (name == "Monster")
        //{
        //    var obj = GameObject.Instantiate(StatusIconPrefab);
        //    obj.transform.SetParent(StatusSlotsTrm);
        //    obj.GetComponentInChildren<Text>().text = statusConfig.Name;
        //    obj.name = action.Status.Id.ToString();
        //}
        //// 眩晕
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
        //// 虚弱
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
    /// 移除状态
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
