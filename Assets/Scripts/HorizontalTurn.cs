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
        if (player.canMove)
        {
            currInput = Input.GetAxis("Horizontal");

            Debug.Log("prev: " + prevInput);
            Debug.Log("curr: " + currInput);
            Debug.Log("-------------------------------------");

            if (currInput < 0 && currInput <= prevInput)
            {
                transform.DOLocalMove(leftPos.localPosition, turnSpeed).SetEase(turnEase);
                transform.DOLocalRotate(leftPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase);
            }

            if (currInput > 0 && currInput >= prevInput)
            {
                transform.DOLocalMove(rightPos.localPosition, turnSpeed).SetEase(turnEase);
                transform.DOLocalRotate(rightPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase);
            }

            if (currInput == 0 || (currInput < 0 && currInput > prevInput) || (currInput > 0 && currInput < prevInput))
            {
                transform.DOLocalMove(Vector3.zero, turnSpeed).SetEase(turnEase);
                transform.DOLocalRotate(Vector3.zero, turnSpeed).SetEase(turnEase);
            }

            prevInput = currInput;
        }
    }
}
