using Unity.XR.CoreUtils;
using UnityEngine;

public class BallBtn : MonoBehaviour {
    [SerializeField] private XROrigin xrOrigin;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		xrOrigin ??= GetComponent<XROrigin>();
	}

    // Update is called once per frame
    void Update() {
        
    }
}
