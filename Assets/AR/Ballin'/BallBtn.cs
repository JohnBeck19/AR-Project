using TMPro;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BallBtn : MonoBehaviour {
    [SerializeField] XROrigin xrOrigin;
    [SerializeField] InputActionManager inputManager;
    [SerializeField] TextMeshProUGUI scoreTxt;

    [SerializeField] GameObject ball;

    private Touchscreen ts;
    private Vector2 startPos;
    private bool touch = false;
    private bool shooting = false;
    private int score = 0;
    
    void Start() {
		xrOrigin ??= GetComponent<XROrigin>();
        inputManager ??= GetComponent<InputActionManager>();
        startPos = transform.position;
        ts = Touchscreen.current;
        score = 0;
        scoreTxt.text = score.ToString("00");
	}

    void Update() {
        if (ts != null && ts.touches.Count > 0 &&
            ts.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began &&
            !shooting /*&&*/) {
            touch = true;
            shooting = true;
        }

        if (ts != null && ts.touches.Count > 0 &&
			ts.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended &&
            shooting) {
            touch = false;
            shooting = false;
            Vector2 endPos = ts.touches[0].position.value;
            Vector3 dir = (endPos - startPos).normalized;
            Ray ray = new Ray(startPos, endPos);
            ball.GetComponent<Rigidbody>().AddRelativeForce(ray.direction * 100, ForceMode.Impulse);
			GameObject ballInstance = Instantiate(ball, xrOrigin.Camera.transform.position, xrOrigin.Camera.transform.rotation);
            ballInstance.GetComponent<BasketBall>().ballBtn = this;
            gameObject.transform.position = startPos;
		}

        if (touch) {
            gameObject.transform.position = Touchscreen.current.touches[0].position.value;
		}
    }

    public void updateScore(int newScore) {
        score += newScore;
        scoreTxt.text = score.ToString("00");
    }

    
}