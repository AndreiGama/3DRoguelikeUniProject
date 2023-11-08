using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowProjectile : MonoBehaviour
{
    Transform parentTransform;
    Rigidbody rb;
    [SerializeField] float forwardForce;
    [SerializeField] float upForce;
    [SerializeField] bool isMovingStraight = false;
    [SerializeField] int projectileSpeed = 5;
    bool isAi;

    private void Start() {
        parentTransform = GetComponentInParent<Transform>();
        rb = GetComponent<Rigidbody>();
        if (!isMovingStraight) {
            StartCoroutine(ProjectileLaunched());
        } 
        
    }
    private void Update() {
        if (isMovingStraight) {
            rb.AddForce(transform.forward * projectileSpeed, ForceMode.Force);
        }
    }

    IEnumerator ProjectileLaunched() {
        Vector3 forceToAdd = parentTransform.forward * forwardForce + parentTransform.up * upForce;
        rb.AddForce(forceToAdd, ForceMode.Impulse);
        yield return new WaitForSeconds(3f);
    }
    private void OnCollisionEnter(Collision collision) {
        if (isAi) {
            if (collision.transform.gameObject.layer == LayerMask.GetMask("Default", "Player")) {
                Destroy(this.gameObject);
            }
        } else {
            if (collision.transform.gameObject.layer == LayerMask.GetMask("Default")) {
                Destroy(this.gameObject);
            }
        }
        
    }
}
