using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    CharacterController controller;
    Fire playerFire;
    Animator anim;

    [Header("Player Settings")]
    [Space(2)]
    [Tooltip("Speed value between 1 and 6")]
    [Range(1.0f, 6.0f)]
    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;

    [Header("Weapon Settings")]
    public float projectileForce;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;

    Vector3 moveDirection;
    public bool attacking;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();
            playerFire = GetComponent<Fire>();
            anim = GetComponentInChildren<Animator>();
            controller.minMoveDistance = 0.0f;


            if (speed <= 0)
            {
                speed = 6.0f;
            }
            if (jumpSpeed <= 0)
            {
                jumpSpeed = 6.0f;
            }
            if (rotationSpeed <= 0)
            {
                rotationSpeed = 10.0f;
            }
            if (gravity <= 0)
            {
                gravity = 9.81f;
            }

            moveDirection = Vector3.zero;

            if (projectileForce <= 0)
            {
                projectileForce = 10.0f;
            }
            if (!projectilePrefab)
            {
                throw new UnassignedReferenceException("PRojectile prefab is anassigned");
            }
        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        catch (UnassignedReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }
        finally
        {
            Debug.Log("Always Gets Called");
        }
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] curAnim = anim.GetCurrentAnimatorClipInfo(0);
        if (controller.isGrounded)
        {
            if (curAnim.Length > 0)
            {
                if (curAnim[0].clip.name != "Cross Punch")
                {
                    moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                }
                else
                {
                    moveDirection = Vector3.zero;
                }
            }
            anim.SetFloat("Horizontal", moveDirection.x);
            anim.SetFloat("Vertical", moveDirection.z);

            moveDirection *= speed;
            moveDirection = transform.TransformDirection(moveDirection);

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
        if (Input.GetButtonDown("Fire2"))
        {
            anim.SetTrigger("Punch");
        }
        if (Input.GetButtonDown("Fire3"))
        {
            anim.SetTrigger("Kick");
        }
    }

    void Fire()
    {
        Debug.Log("PEW");
        playerFire.FireProjectile();
    }

    [ContextMenu("Reset Stats")]
    void ResetStats()
    {
        speed = 6.0f;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Finish")
        {
            Debug.Log("Game Ended");
        }
        if (hit.gameObject.tag == "Enemy" && !attacking)
        {
            anim.SetTrigger("Death");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Finish")
        {
            Debug.Log("Game Ended");
        }
    }
}
