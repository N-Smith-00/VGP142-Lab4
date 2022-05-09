using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    Transform model;
    [SerializeField]
    GameObject player;

    CharacterController controller;

    [Header("Player Settings")]
    [Space(2)]
    [Tooltip("Speed value between 1 and 6")]
    [Range(1.0f, 6.0f)]
    public float speed;
    public float rotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();

        if (speed <= 0)
        {
            speed = 3.0f;
        }
        if (rotationSpeed <= 0)
        {
            rotationSpeed = 0.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        model.gameObject.SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Projectile")
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Attack")
        {
            Destroy(gameObject);
        }
    }
}
