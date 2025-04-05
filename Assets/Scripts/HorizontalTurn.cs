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
    void Update()
    {
        Debug.Log("input horizontal axis: " + Input.GetAxis("Horizontal"));

        if (Input.GetKeyDown(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            transform.DOLocalMove(leftPos.localPosition, turnSpeed).SetEase(turnEase);
            transform.DOLocalRotate(leftPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase);
        }

        if (Input.GetKeyDown(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            transform.DOLocalMove(rightPos.localPosition, turnSpeed).SetEase(turnEase);
            transform.DOLocalRotate(rightPos.localRotation.eulerAngles, turnSpeed).SetEase(turnEase);
        }

        if (!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)) {
            transform.DOLocalMove(Vector3.zero, turnSpeed).SetEase(turnEase);
            transform.DOLocalRotate(Vector3.zero, turnSpeed).SetEase(turnEase);
        }


    }
}
