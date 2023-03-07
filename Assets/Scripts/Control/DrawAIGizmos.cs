using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Control {
    public class DrawAIGizmos : MonoBehaviour
    {
        private AIController aiController;
        private void Awake()
        {
            aiController= GetComponent<AIController>();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, aiController.GetChaseDistance());
        }
    }
}