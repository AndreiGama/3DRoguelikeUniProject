using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    Transform parentTransform;
    float gravity = -9.87f;
    Rigidbody rb;
    [SerializeField] float forwardForce;
    [SerializeField] float upForce;

    private void Start() {
        parentTransform = GetComponentInParent<Transform>();
        rb = GetComponent<Rigidbody>();
        StartCoroutine(grenadeLaunched());
    }
    private void Update() {
        
    }

    IEnumerator grenadeLaunched() {
        Vector3 forceToAdd = parentTransform.forward * forwardForce + parentTransform.up * upForce;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }
}
