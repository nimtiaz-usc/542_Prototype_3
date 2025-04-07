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
        if (Input.GetMouseButtonDown(0)) {
            player.canMove = false;
            transform.DOMove(attackPos[0].position, 0.5f);
        }
        if (!Input.GetMouseButton(0)) {
            player.canMove = true;
        }
    }
}
