using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] float maxHealth = 10f;
    private float health;
    [SerializeField] bool attackable = true;
    [SerializeField] float invulnerableTime = 1f;
    [SerializeField] float maxForce = 1f;
    [SerializeField] float respawnTime = 1f;
    [SerializeField] Transform respawnPos;
    [SerializeField] Material[] defaultMaterials;
    [SerializeField] Material[] attackMaterials;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.GetComponentInParent<PlayerController>();
            if (player != null)
            {
                if (player.hitboxActive && attackable) {

                    player.impactParticles.transform.position = other.gameObject.GetComponent<Collider>().ClosestPointOnBounds(transform.position);
                    player.impactParticles.GetComponent<ParticleSystem>().Play();
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
        GetComponent<Collider>().enabled = false;
        Rigidbody[] partBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody partBody in partBodies) {
            partBody.useGravity = true;
            partBody.isKinematic = false;

            Vector3 forceVector = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
            partBody.AddForce(Vector3.forward * maxForce);
        }
        Invoke("Respawn", respawnTime);
    }

    void Respawn()
    {
        ResetAttackable();

        Transform[] partTransforms = GetComponentsInChildren<Transform>();
        Rigidbody[] partBodies = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody partBody in partBodies)
        {
            partBody.useGravity = false;
            partBody.isKinematic = true;
        }

        foreach (Transform partTransform in partTransforms)
        {
            partTransform.DOLocalMove(Vector3.zero, 0.5f);
            partTransform.DOLocalRotate(Vector3.right * -90f, 0.5f);
        }

        transform.DOLocalMove(respawnPos.localPosition, 0.5f);
        transform.DOLocalRotate(respawnPos.localRotation.eulerAngles, 0.5f);

        GetComponent<Collider>().enabled = true;

        health = maxHealth;
    }
}
