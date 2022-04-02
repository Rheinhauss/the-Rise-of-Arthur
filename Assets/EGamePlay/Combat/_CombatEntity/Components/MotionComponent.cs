using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GameUtils;
using Sirenix.OdinInspector;

namespace EGamePlay.Combat
{
    /// <summary>
    /// 运动组件，在这里管理战斗实体的移动、跳跃、击飞等运动功能
    /// </summary>
    public sealed class MotionComponent : Component
    {
        /// <summary>
        /// 是否启用,默认启用
        /// </summary>
        public override bool DefaultEnable { get; set; } = true;
        /// <summary>
        /// 位置
        /// </summary>
        public Vector3 Position { get => GetEntity<CombatEntity>().Position; set => GetEntity<CombatEntity>().Position = value; }
        /// <summary>
        /// 方位
        /// </summary>
        public float Direction { get => GetEntity<CombatEntity>().Direction; set => GetEntity<CombatEntity>().Direction = value; }
        /// <summary>
        /// 是否可以移动
        /// </summary>
        public bool CanMove { get; set; }
        /// <summary>
        /// 待机计时器
        /// </summary>
        public GameTimer IdleTimer { get; set; }
        /// <summary>
        /// 移动计时器
        /// </summary>
        public GameTimer MoveTimer { get; set; }
        /// <summary>
        /// 移动向量
        /// </summary>
        public Vector3 MoveVector { get; set; }
        /// <summary>
        /// 初始位置
        /// </summary>
        private Vector3 originPos;


        public override void Setup()
        {
            base.Setup();
            IdleTimer = new GameTimer(RandomHelper.RandomNumber(20, 30) / 10f);
            MoveTimer = new GameTimer(RandomHelper.RandomNumber(20, 40) / 10f);
            IdleTimer.Reset();
            originPos = Position;
        }
        /// <summary>
        /// 计时 -> 并执行对应方法
        /// 待机 > 移动
        /// </summary>
        public override void Update()
        {
            if (IdleTimer.IsRunning)
            {
                IdleTimer.UpdateAsFinish(Time.deltaTime, IdleFinish);
            }
            else
            {
                if (MoveTimer.IsRunning)
                {
                    MoveTimer.UpdateAsFinish(Time.deltaTime, MoveFinish);
                    var speed = GetEntity<CombatEntity>().GetComponent<AttributeComponent>().MoveSpeed.Value;
                    Position += MoveVector * speed;
                }
            }
        }
        /// <summary>
        /// 待机计时完成事件
        /// 开始移动
        /// </summary>
        private void IdleFinish()
        {
            var x = RandomHelper.RandomNumber(-20, 20);
            var z = RandomHelper.RandomNumber(-20, 20);
            var vec2 = new Vector2(x, z);
            if (Vector3.Distance(originPos, Position) > 0.1f)
            {
                vec2 = -(Position - originPos);
            }
            vec2.Normalize();
            var right = new Vector2(1, 0);
            Direction = VectorAngle(right, vec2);

            MoveVector = new Vector3(vec2.x, 0, vec2.y) / 100f;
            MoveTimer.Reset();
        }
        /// <summary>
        /// 移动完成，待机
        /// </summary>
        private void MoveFinish()
        {
            IdleTimer.Reset();
        }
        /// <summary>
        /// 计算向量的夹角
        /// </summary>
        /// <param name="from">起始向量</param>
        /// <param name="to">终止向量</param>
        /// <returns>返回夹角</returns>
        private float VectorAngle(Vector2 from, Vector2 to)
        {
            var angle = 0f;
            var cross = Vector3.Cross(from, to);
            angle = Vector2.Angle(from, to);
            return cross.z > 0 ? -angle : angle;
        }
    }
}