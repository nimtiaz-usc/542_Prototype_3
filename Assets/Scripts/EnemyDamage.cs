using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] float health = 10f;
    [SerializeField] bool attackable = true;
    [SerializeField] float invulnerableTime = 1f;
    [SerializeField] float maxForce = 1f;
    [SerializeField] Material[] defaultMaterials;
    [SerializeField] Material[] attackMaterials;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                if (player.hitboxActive && attackable) {
                    attackable = false;
                    health -= player.attackDamage;
                    Debug.Log("attacked: now at " + health + " HP!");

                    MeshRenderer[] partRenderers = GetComponentsInChildren<MeshRenderer>();
                    foreach(MeshRenderer partRenderer in partRenderers) {
                        partRenderer.materials = attackMaterials;
                    }

                    if (health <= 0)
                    {
                        KillEnemy();
                    } else
                    {
                        Invoke("ResetAttackable", invulnerableTime);
                    }

                }
            }
        }
    }

    void ResetAttackable()
    {
        MeshRenderer[] parts = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer part in parts) {
            part.materials = defaultMaterials;
        }
        attackable = true;
    }

    void KillEnemy() {
        Rigidbody[] partBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody partBody in partBodies) {
            partBody.useGravity = true;
            partBody.isKinematic = false;

            //Vector3 forceVector = newVector3(Random.Range())


            //partBody.AddForce();
        }
    }
}
