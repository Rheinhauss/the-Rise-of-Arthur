using System;
using UnityEngine;

namespace EGamePlay
{
    [EnableUpdate]
    public class UpdateComponent : Component
    {
        public override bool DefaultEnable { get; set; } = true;


        public override void Update()
        {
            Entity.Update();
        }
    }
}