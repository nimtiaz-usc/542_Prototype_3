using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    //private Camera cam;
    [SerializeField] float walkSpeed = 6f;
    [SerializeField] float sprintFactor = 2f;

    [SerializeField] float lookSpeed = 2f;
    [SerializeField] float lookXLimit = 45f;

    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0f;

    public bool canMove = true;
    public bool isAttacking = false;
    public bool hitboxActive = false;

    public float attackDamage = 2f;

    CharacterController characterController;
    [SerializeField] Transform camPivot;
    [SerializeField] Camera cam;

    [SerializeField] Image speedLines;

    //[SerializeField] AudioSource footstepSFX;

    private float prevInput = 0f;
    private float currInput = 0f;

    private void Start()
    {
        cam = Camera.main;
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        currInput = Input.GetAxis("Vertical");

        Vector3 forward = camPivot.forward; //transform.TransformDirection(Vector3.forward);
        Vector3 right = camPivot.right; //transform.TransformDirection(Vector3.right);

        float curSpeedX = canMove ? walkSpeed * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? walkSpeed * Input.GetAxis("Horizontal") : 0;

        if (isAttacking)
        {
            curSpeedX /= sprintFactor;
            curSpeedY /= sprintFactor;
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetAxis("Vertical") > 0 && currInput >= prevInput && !isAttacking) {
            curSpeedX *= sprintFactor;
            curSpeedY *= sprintFactor;
            DOTween.To(()=> cam.fieldOfView, x=> cam.fieldOfView = x, 80f, 5f).SetEase(Ease.OutElastic);
            speedLines.DOFade(0.35f, 2f).SetEase(Ease.OutElastic);
        } else {
            DOTween.To(()=> cam.fieldOfView, x=> cam.fieldOfView = x, 60f, 5f).SetEase(Ease.OutElastic);
            speedLines.DOFade(0f, 2f).SetEase(Ease.OutElastic);
        }

        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (canMove)
        {
            characterController.Move(moveDirection * Time.deltaTime);
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            camPivot.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);

            //footstepSFX.enabled = true;
            //footstepSFX.volume = Mathf.Max(Mathf.Abs(Input.GetAxis("Vertical")), Mathf.Abs(Input.GetAxis("Horizontal")));
        }

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)
        {
            //footstepSFX.enabled = false;
            //footstepSFX.volume = 0;
        }

        prevInput = currInput;

    }

}
