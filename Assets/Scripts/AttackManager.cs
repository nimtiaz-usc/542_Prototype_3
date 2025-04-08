using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float attackSpeed = 0.25f;
    [SerializeField] float attackWindup = 2f;
    [SerializeField] Transform[] attackPos;

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            Debug.Log("attack manager click");
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence() {
        player.isAttacking = true;

        transform.DOLocalMove(attackPos[0].localPosition, attackSpeed * attackWindup);
        transform.DOLocalRotate(attackPos[0].localRotation.eulerAngles, attackSpeed * attackWindup);

        yield return new WaitForSeconds(attackSpeed * attackWindup);

        transform.DOLocalMove(attackPos[1].localPosition, attackSpeed);
        transform.DOLocalRotate(attackPos[1].localRotation.eulerAngles, attackSpeed);

        yield return new WaitForSeconds(attackSpeed);

        if (!Input.GetMouseButton(0)) {
            player.isAttacking = false;
        } else {
            StartCoroutine(AttackSequence());
        }
        yield return null;
    }
}
