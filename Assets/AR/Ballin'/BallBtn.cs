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
    [SerializeField] TextMeshProUGUI timeTxt;

    [SerializeField] GameObject ball;
    [SerializeField] float time;

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
        time = 30;
        scoreTxt.text = score.ToString("00");
        timeTxt.text = time.ToString("00");
	}

    void Update() {
        if (time > 0) {
            time -= Time.deltaTime;
            timeTxt.text = time.ToString("00");
        }
        if (time < 0) {
            
        }

        if (ts != null && ts.touches.Count > 0 &&
            ts.touches[0].phase.ReadValue() == UnityEngine.InputSystem.TouchPhase.Began &&
            !shooting && ((ts.touches[0].position.x.value > 200 && ts.touches[0].position.x.value < 1300) 
            && (ts.touches[0].position.y.value <= 600)) ) {
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

            float xForce = 0f;
            
            if (endPos.x < (Screen.width / 2) - 100) {
                xForce = -1.5f;
            } else if (endPos.x > (Screen.width / 2) + 100) {
                xForce = 1.5f;
            } else {
                xForce = 0;
			}

            float yForce = (float)((endPos.y / Screen.height) * 3.5);
            float zForce = 2f;
			GameObject spawnedBall = Instantiate(ball, xrOrigin.Camera.transform.position + new Vector3(0, 0, 1), xrOrigin.Camera.transform.rotation);
            spawnedBall.GetComponent<BasketBall>().ballBtn = this;
            spawnedBall.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(xForce, yForce, zForce), ForceMode.Impulse);
            Destroy(spawnedBall, 2f);
            Debug.Log($"X Force { xForce } Y Force { yForce } Z Force { zForce }");
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