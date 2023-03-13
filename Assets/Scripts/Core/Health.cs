using RPG.Saving;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float health = 100f;

        public void TakeDamage(float damage)
        {

            health = Mathf.Max(health - damage, 0);
            print(health);
            if (health == 0)
            {
                Die();
            }
        }

        public float GetHealth()
        {
            return health;
        }
        private void Die() 
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        #region ISaveable
        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health = (float)state;
        }
        #endregion
    }
}