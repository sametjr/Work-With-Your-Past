using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeRotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1f;
    private void FixedUpdate() {
        transform.Rotate(Vector3.up * Time.fixedDeltaTime * rotationSpeed, Space.Self);
    }
}
