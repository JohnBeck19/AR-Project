using UnityEngine;

public class BasketBall : MonoBehaviour {
    public BallBtn ballBtn;
    [SerializeField] AudioSource audio;
    [SerializeField] AudioClip make;

    void Start() 
    {
        audio = GetComponent<AudioSource>();
    }

    void Update() {
        
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Hoop")) {
            ballBtn.updateScore(1);
            audio.PlayOneShot(make);
        }
    }
}