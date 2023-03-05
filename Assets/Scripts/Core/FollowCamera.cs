using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{

    public class FollowCamera : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        private void LateUpdate()
        {
            transform.position = targetTransform.position;
        }
    }
}