using System;
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
            !shooting && ((ts.touches[0].position.x.value > 600 && ts.touches[0].position.x.value < 900) 
            && (ts.touches[0].position.y.value <= 300)) ) {
            touch = true;
            shooting = true;
        }

        if (ts != null && ts.touches.Count > 0 &&
			ts.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Ended 
            && shooting) {
            touch = false;
            shooting = false;
            gameObject.transform.position = startPos;
            Vector2 endPos = ts.touches[0].position.value;
            Vector3 dir = (endPos - startPos).normalized;
            Ray ray = new Ray(startPos, endPos);
			GameObject spawnedBall = Instantiate(ball, xrOrigin.Camera.transform.position, xrOrigin.Camera.transform.rotation);
            spawnedBall.GetComponent<BasketBall>().ballBtn = this;
            spawnedBall.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 100, ForceMode.Impulse);
            Destroy(spawnedBall, 3f);
		}

        if (touch) {
			gameObject.transform.position = ts.touches[0].position.value;
		}
    }

    public void updateScore(int newScore) {
        score += newScore;
        scoreTxt.text = score.ToString("00");
    }

    
}