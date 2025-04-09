using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] float health = 10f;
    [SerializeField] bool attackable = true;
    [SerializeField] float invulnerableTime = 1f;

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

                    if (health <= 0)
                    {
                        Destroy(gameObject);
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
        attackable = true;
    }
}
