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

    private Sequence leftTurn;
    private Sequence rightTurn;
    private Sequence resetTurn;

    void Start()
    {
        leftTurn = DOTween.Sequence();
        leftTurn.Append(transform.DOLocalMove(leftPos.localPosition, turnSpeed).SetEase(turnEase));
        leftTurn.Join(transform.DOLocalRotate(leftPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase));

        rightTurn = DOTween.Sequence();
        rightTurn.Append(transform.DOLocalMove(rightPos.localPosition, turnSpeed).SetEase(turnEase));
        rightTurn.Join(transform.DOLocalRotate(rightPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase));

        resetTurn = DOTween.Sequence();
        resetTurn.Append(transform.DOLocalMove(Vector3.zero, turnSpeed).SetEase(turnEase));
        resetTurn.Join(transform.DOLocalRotate(Vector3.zero, turnSpeed).SetEase(turnEase));
    }

    void Update()
    {
        if (!player.isAttacking)
        {
            currInput = Input.GetAxis("Horizontal");

            if (currInput < 0 && currInput <= prevInput)
            {
                leftTurn.Play();
            }

            if (currInput > 0 && currInput >= prevInput)
            {
                rightTurn.Play();
            }

            if (currInput == 0 || (currInput < 0 && currInput > prevInput) || (currInput > 0 && currInput < prevInput))
            {
                resetTurn.Play();
            }

            Debug.Log("-----------------------------------");
            Debug.Log("left turn: " + leftTurn.IsPlaying());
            Debug.Log("right turn: " + rightTurn.IsPlaying());
            Debug.Log("reset turn: " + resetTurn.IsPlaying());
            Debug.Log("-----------------------------------");

            prevInput = currInput;
        } else {
            //resetTurn.Kill();
        }
    }
}
