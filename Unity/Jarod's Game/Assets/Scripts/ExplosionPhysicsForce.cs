using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityStandardAssets.Effects
{
    public class ExplosionPhysicsForce : MonoBehaviour
    {
        public float explosionForce = 1;
        public float explosionDamage = 20;
        private GameObject GameFunctions;
        private GameObject GameController;

        private IEnumerator Start()
        {
            GameFunctions = GameObject.FindGameObjectWithTag("GameFunctions");
            GameController = GameObject.FindGameObjectWithTag("GameController");
            // wait one frame because some explosions instantiate debris which should then
            // be pushed by physics force
            yield return null;

            float multiplier = GetComponent<ParticleSystemMultiplier>().multiplier;

            float r = multiplier;
            Debug.Log("ExplosionRadius: "+r);
            var cols = Physics.OverlapSphere(transform.position, r);
            var rigidbodies = new List<Rigidbody>();
            foreach (var col in cols)
            {
                if (col.attachedRigidbody != null && !rigidbodies.Contains(col.attachedRigidbody) && !col.CompareTag("RangeCollider")) 
                {
                    rigidbodies.Add(col.attachedRigidbody);
                }
                if (col.gameObject.CompareTag("Enemy")||col.gameObject.CompareTag("Player"))
                {
                    GameFunctions.GetComponent<GameFunctions>().DamageObject(col.gameObject, explosionDamage*multiplier);
                }
            }
            foreach (var rb in rigidbodies)
            {
                rb.AddExplosionForce(explosionForce*multiplier, transform.position, r, multiplier, ForceMode.Impulse);
            }
        }
    }
}
