using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    private Transform cam;
    private Vector3 startPos;
    [SerializeField] private float speed = 500f;
    [SerializeField] private float maxSpeed = 20f;

    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private bool isGrounded = true;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main.transform;
        Cursor.lockState = CursorLockMode.Locked;
        startPos = transform.position;
    }

    private void Update() {
        if (isGrounded && Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(Vector3.up * jumpForce);
            isGrounded = false;
        }

        // if(Input.GetKeyDown(KeyCode.K))
        // Time.timeScale = Time.timeScale == 1 ? 3 : 1;

    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(NormalizedXZPlane(cam.forward) * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(NormalizedXZPlane(-cam.right) * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(NormalizedXZPlane(-cam.forward) * speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(NormalizedXZPlane(cam.right) * speed * Time.deltaTime);
        }

        // Debug.Log(rb.velocity.magnitude);

        rb.velocity = new Vector3(ClampVelocity(rb.velocity.x), ClampVelocity(rb.velocity.y), ClampVelocity(rb.velocity.z));
    }



    private float ClampVelocity(float _vel)
    {
        return Mathf.Clamp(_vel, -maxSpeed, maxSpeed);
    }

    private Vector3 NormalizedXZPlane(Vector3 v)
    {
        return new Vector3(v.x, 0, v.z).normalized;
    }

    private void OnCollisionEnter(Collision other)
    {
        isGrounded = true;
        if(other.gameObject.CompareTag("Killer"))
        {
            GameManager.Instance.PlayerDead();
        }
    }
}
