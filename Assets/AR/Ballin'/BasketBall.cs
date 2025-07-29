using UnityEngine;

public class BasketBall : MonoBehaviour {
    public BallBtn ballBtn;
    void Start() {
        
    }

    void Update() {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Hoop")) {
            ballBtn.updateScore(1);
        }
    }
}