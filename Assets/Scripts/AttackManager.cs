using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class AttackManager : MonoBehaviour
{
    [SerializeField] PlayerController player;
    [SerializeField] float attackSpeed = 0.25f;
    [SerializeField] float attackWindup = 1f;
    [SerializeField] Transform[] attackPos;
    [SerializeField] float lightAttackDamage = 2f;
    [SerializeField] float heavyAttackDamage = 10f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.isAttacking) {
            StartCoroutine(AttackSequence());
        }
    }

    IEnumerator AttackSequence() {
        DOTween.KillAll();
        player.isAttacking = true;
        player.attackDamage = lightAttackDamage;

        transform.DOLocalMove(attackPos[0].localPosition, attackWindup);
        transform.DOLocalRotate(attackPos[0].localRotation.eulerAngles, attackWindup);

        yield return new WaitForSeconds(attackWindup);

        player.hitboxActive = true;
        transform.DOLocalMove(attackPos[1].localPosition, attackSpeed);
        transform.DOLocalRotate(attackPos[1].localRotation.eulerAngles, attackSpeed);

        yield return new WaitForSeconds(attackSpeed);
        player.hitboxActive = false;

        if (!Input.GetMouseButton(0))
        {
            player.isAttacking = false;
        }
        else
        {
            transform.DOLocalMove(attackPos[2].localPosition, attackWindup);
            transform.DOLocalRotate(attackPos[2].localRotation.eulerAngles, attackWindup);

            yield return new WaitForSeconds(attackWindup);

            player.hitboxActive = true;
            transform.DOLocalMove(attackPos[3].localPosition, attackSpeed);
            transform.DOLocalRotate(attackPos[3].localRotation.eulerAngles, attackSpeed);

            yield return new WaitForSeconds(attackSpeed);
            player.hitboxActive = false;

            if (!Input.GetMouseButton(0))
            {
                player.isAttacking = false;
            }
            else
            {
                StartCoroutine(AttackSequence());
            }
        }
        yield return null;
    }
}
