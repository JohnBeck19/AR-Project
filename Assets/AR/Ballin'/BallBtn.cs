using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BallBtn : MonoBehaviour {
    [SerializeField] private XROrigin xrOrigin;
    [SerializeField] private InputActionManager inputManager;

    [SerializeField] private GameObject ball;

    private Vector2 startPos;
    private bool touch = false;
    private bool shooting = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		xrOrigin ??= GetComponent<XROrigin>();
        inputManager ??= GetComponent<InputActionManager>();
        startPos = transform.position;
	}

    // Update is called once per frame
    void Update() {
        if (Touchscreen.current != null &&
            Touchscreen.current.touches.Count > 0 &&
            Touchscreen.current.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began) {
            touch = true;
        }

        if (Touchscreen.current != null &&
			Touchscreen.current.touches.Count > 0 &&
			Touchscreen.current.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended) {
            touch = false;
            Vector2 endPos = Touchscreen.current.touches[0].position.value;
            Vector3 dir = (endPos - startPos).normalized;
            Ray ray = new Ray(startPos, endPos);
            ball.GetComponent<Rigidbody>().AddRelativeForce(ray.direction * 10, ForceMode.Impulse);
			Instantiate(ball, xrOrigin.Camera.transform.position, xrOrigin.Camera.transform.rotation);
		}

        if (touch) {
            gameObject.transform.position = Touchscreen.current.touches[0].position.value;
		}
    }
}