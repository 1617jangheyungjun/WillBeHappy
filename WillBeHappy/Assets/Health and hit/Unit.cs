using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AllUnits
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] protected float speed = 3f;
        [SerializeField] internal float maxHealth = 50f;
        [SerializeField] internal float currentHealth;
        [SerializeField] internal float damageDelay = 2f;
        [SerializeField] internal float damage = 5f;
        internal float initialDamageDelay;
        [SerializeField] protected bool isDamage = false;

        [SerializeField]internal bool applyCameraShake;
        internal CameraShake cameraShake;

        virtual protected void Start()
        {
            cameraShake = Camera.main.GetComponent<CameraShake>();
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
