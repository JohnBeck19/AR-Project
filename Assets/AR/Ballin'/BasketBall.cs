using UnityEngine;

public class BasketBall : MonoBehaviour {
    [SerializeField] float lifeTime = 3;

    void Start() {
        
    }

    void Update() {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0) {
            Destroy(gameObject);
        }
    }
}