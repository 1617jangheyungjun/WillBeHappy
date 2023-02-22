using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AllUnits
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] internal float maxHealth = 50f;
        [SerializeField] internal float currentHealth;
        [SerializeField] internal float damageDelay = 2f;
        [SerializeField] internal float damage = 5f;
        internal float initialDamageDelay;
        [SerializeField] protected bool isDamage = false;


        virtual protected void Start()
        {
            currentHealth = maxHealth;
            initialDamageDelay = damageDelay;
        }

        virtual protected void Update()
        {
            DamageDelay();
        }
        protected void DamageDelay()
        {
            if (isDamage && damageDelay > 0)
            {
                damageDelay -= Time.deltaTime;
                if (damageDelay <= 0)
                {
                    isDamage = false;
                    damageDelay = initialDamageDelay;
                }
            }
        }
    }
}
