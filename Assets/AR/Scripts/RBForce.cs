using UnityEngine;

public class RBForce : MonoBehaviour {
    [SerializeField] public float force;

    void Start() {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddRelativeForce(Vector3.forward * force, ForceMode.VelocityChange);
    }
}
