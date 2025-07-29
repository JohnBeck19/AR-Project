using UnityEngine;

public class BasketBall : MonoBehaviour {
    [SerializeField] float lifeTime = 3;
    public BallBtn ballBtn;
    void Start() {
        
    }

    void Update() {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Hoop")) {
            ballBtn.updateScore(1);
        }
    }
}