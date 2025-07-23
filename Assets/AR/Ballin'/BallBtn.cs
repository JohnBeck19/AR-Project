using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class BallBtn : MonoBehaviour {
    [SerializeField] private XROrigin xrOrigin;
    [SerializeField] private InputActionManager inputManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start() {
		xrOrigin ??= GetComponent<XROrigin>();
        inputManager ??= GetComponent<InputActionManager>();
	}

    // Update is called once per frame
    void Update() {
        
    }
}
