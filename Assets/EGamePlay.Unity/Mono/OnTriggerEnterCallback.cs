using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using EGamePlay;
using System;

namespace EGamePlay.Combat
{
    public class OnTriggerEnterCallback : MonoBehaviour
    {
        public Action<Collider> OnTriggerEnterCallbackAction;
        public bool EmitOnce = true;
        private List<Collider> colliders = new List<Collider>();

        private void OnTriggerEnter(Collider other)
        {
            if (EmitOnce && colliders.Contains(other))
                return;
            //Debug.Log($"OnTriggerEnterCallback OnTriggerEnter {other.name}");
            OnTriggerEnterCallbackAction?.Invoke(other);
            colliders.Add(other);
        }
    }
}