using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class playerController : MonoBehaviour
{
    public Vector3 movedir;
    private float movespeed = 7f;
    private float rotatespeed = 10f;
    public bool isWalking;
    private bool isLifting = false;
    private Vector3 lastInterecdir;
    [SerializeField] private Transform holdObject;
    private Transform holdingObject;
    private bool holding = false;
    private int score;
    private CharacterController controller;

    Animator animator;
    public bool canMove = true;
    private Transform itemInFront;

    // ใช้สำหรับกัน Trigger ซ้ำ
    private bool wasWalking = false;

    public void EnableControl(bool enable)
    {
        canMove = enable;
    }

    void Awake()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!canMove)
        {
            animator.SetTrigger("idle");
            return;
        }

        Vector2 inputVector = new Vector2(0, 0);
        if (Input.GetKey(KeyCode.W)) inputVector.y = +1;
        if (Input.GetKey(KeyCode.S)) inputVector.y = -1;
        if (Input.GetKey(KeyCode.A)) inputVector.x = -1;
        if (Input.GetKey(KeyCode.D)) inputVector.x = +1;

        inputVector = inputVector.normalized;
        Vector3 movedir = new Vector3(inputVector.x, 0f, inputVector.y);

        controller.Move(movedir * movespeed * Time.deltaTime);

        if (movedir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, movedir, Time.deltaTime * rotatespeed);
            isWalking = true;
            lastInterecdir = movedir;
        }
        else
        {
            isWalking = false;
        }

        // เรียก Trigger walk เฉพาะเมื่อเริ่มเดิน
        if (isWalking && !wasWalking)
        {
            animator.SetTrigger("walk");
        }
        // เรียก idle เมื่อหยุดเดิน
        if (!isWalking && wasWalking && !isLifting)
        {
            animator.SetTrigger("idle");
        }

        wasWalking = isWalking;

        // isLifting เดินอยู่ด้วยก็เล่น lift-walk
        if (isLifting && isWalking)
        {
            animator.SetTrigger("lift-walk");
        }

        CheckForItem();

        if (Input.GetKeyDown(KeyCode.E) && itemInFront != null && !holding)
        {
            PickupItem(itemInFront);
            itemInFront = null;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            DropItem();
        }

        Debug.DrawRay(transform.position + Vector3.up * 0.5f, lastInterecdir * 1.5f, Color.red);
    }

    private void PickupItem(Transform item)
    {
        item.SetParent(holdObject);
        item.localPosition = Vector3.zero;
        item.localRotation = Quaternion.identity;
        item.localScale = Vector3.one;

        Rigidbody rb = item.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider col = item.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = false;
        }

        holding = true;
        holdingObject = item;

        isLifting = true;
        animator.SetTrigger("lift-idle");
    }

    public bool Iswalking()
    {
        return isWalking;
    }

    private void DropItem()
    {
        if (!holding || holdingObject == null) return;

        holdingObject.SetParent(null);

        Rigidbody rb = holdingObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }

        Collider col = holdingObject.GetComponent<Collider>();
        if (col != null)
        {
            col.enabled = true;
        }

        holdingObject.position = transform.position + transform.forward + Vector3.up * 0.5f;

        holding = false;
        isLifting = false;
        holdingObject = null;

        animator.SetTrigger("idle");
    }

    void CheckForItem()
    {
        itemInFront = null;
        Vector3 origin = transform.position + Vector3.up * 0.5f;

        if (Physics.Raycast(origin, lastInterecdir, out RaycastHit hit, 1.5f))
        {
            if (!holding && hit.transform.CompareTag("potion"))
            {
                itemInFront = hit.transform;
            }
        }
    }
}
