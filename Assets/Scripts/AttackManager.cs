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
    [SerializeField] float heavyAttackDamage = 4f;
    [SerializeField] float chargeScale = 0.5f;
    [SerializeField] ParticleSystem[] particles;
    [SerializeField] Ease attackEase;
    [SerializeField] GameObject trail;

    private bool charged = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.isAttacking) {
            StartCoroutine(AttackSequence());
        }

        if (Input.GetMouseButtonDown(1) && !player.isAttacking)
        {
            StartCoroutine(AttackCharge());
        }

        if (player.hitboxActive) { trail.SetActive(true); } else { trail.SetActive(false); }
    }

    IEnumerator AttackSequence() {
        DOTween.Kill(transform);
        player.isAttacking = true;
        player.attackDamage = lightAttackDamage;

        transform.DOLocalMove(attackPos[0].localPosition, attackWindup).SetEase(attackEase);
        transform.DOLocalRotate(attackPos[0].localRotation.eulerAngles, attackWindup).SetEase(attackEase);

        yield return new WaitForSeconds(attackWindup);

        player.hitboxActive = true;
        transform.DOLocalMove(attackPos[1].localPosition, attackSpeed).SetEase(attackEase);
        transform.DOLocalRotate(attackPos[1].localRotation.eulerAngles, attackSpeed).SetEase(attackEase);

        yield return new WaitForSeconds(attackSpeed);
        player.hitboxActive = false;

        if (!Input.GetMouseButton(0))
        {
            player.isAttacking = false;
        }
        else
        {
            transform.DOLocalMove(attackPos[2].localPosition, attackWindup).SetEase(attackEase);
            transform.DOLocalRotate(attackPos[2].localRotation.eulerAngles, attackWindup).SetEase(attackEase);

            yield return new WaitForSeconds(attackWindup);

            player.hitboxActive = true;
            transform.DOLocalMove(attackPos[3].localPosition, attackSpeed).SetEase(attackEase);
            transform.DOLocalRotate(attackPos[3].localRotation.eulerAngles, attackSpeed).SetEase(attackEase);

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

    IEnumerator AttackCharge()
    {
        DOTween.Kill(transform);
        player.isAttacking = true;
        player.attackDamage = heavyAttackDamage;

        transform.DOLocalMove(attackPos[4].localPosition, attackWindup * 2).SetEase(attackEase);
        transform.DOLocalRotate(attackPos[4].localRotation.eulerAngles, attackWindup * 2).SetEase(attackEase);

        particles[0].Play();

        yield return new WaitForSeconds(attackWindup * 2);

        transform.DOShakeRotation(0.1f, 2f).SetLoops(-1).SetId(transform).SetEase(attackEase);

        while (Input.GetMouseButton(1))
        {
            if (player.attackDamage < 10f)
            {
                player.attackDamage += (chargeScale * Time.deltaTime);
            } else if (!charged)
            {
                charged = true;
                particles[0].Stop();
                particles[1].Play();
                player.attackDamage = 10f;
            }
            Debug.Log("damage: " + player.attackDamage);
            yield return null;
        }

        charged = false;
        particles[0].Stop();
        player.hitboxActive = true;
        DOTween.Kill(transform);

        transform.DOLocalMove(attackPos[5].localPosition, attackSpeed * 2).SetEase(attackEase);
        transform.DOLocalRotate(attackPos[5].localRotation.eulerAngles, attackSpeed * 2).SetEase(attackEase);

        yield return new WaitForSeconds(attackSpeed * 2);

        player.hitboxActive = false;
        player.isAttacking = false;

        yield return null;
    }
}
