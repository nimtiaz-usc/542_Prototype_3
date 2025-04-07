using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] Transform[] attackPos;

    void Update()
    {
        if (Input.GetMouseButton(0)) {
            Debug.Log("hi hi");
            player.isAttacking = true;
            transform.DOMove(attackPos[0].position, 0.5f);
        }
        else {
            player.isAttacking = false;
        }
    }
}
