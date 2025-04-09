using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HorizontalTurn : MonoBehaviour
{
    [SerializeField] Transform leftPos;
    [SerializeField] Transform rightPos;
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] Ease turnEase;

    [SerializeField] PlayerController player;

    private float prevInput = 0f;
    private float currInput = 0f;

    void Update()
    {
        if (!player.isAttacking)
        {
            currInput = Input.GetAxis("Horizontal");

            if (currInput < 0 && currInput <= prevInput)
            {
                transform.DOLocalMove(leftPos.localPosition, turnSpeed).SetEase(turnEase).SetId(transform);
                transform.DOLocalRotate(leftPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase).SetId(transform);
            }

            if (currInput > 0 && currInput >= prevInput)
            {
                transform.DOLocalMove(rightPos.localPosition, turnSpeed).SetEase(turnEase).SetId(transform);
                transform.DOLocalRotate(rightPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase).SetId(transform);
            }

            if (currInput == 0 || (currInput < 0 && currInput > prevInput) || (currInput > 0 && currInput < prevInput))
            {
                transform.DOLocalMove(Vector3.zero, turnSpeed).SetEase(turnEase).SetId(transform);
                transform.DOLocalRotate(Vector3.zero, turnSpeed).SetEase(turnEase).SetId(transform);
            }

            prevInput = currInput;
        }
    }
}
