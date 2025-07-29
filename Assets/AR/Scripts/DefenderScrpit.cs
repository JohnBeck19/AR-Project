using UnityEngine;

public class DefenderScrpit : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] float jumpInterval = 5f; // Time between jumps in seconds
    [SerializeField] float jumpForce = 10f; // Force of the jump
    [SerializeField] float moveSpeed = 3f; // Speed of movement
    
    Transform basket;
    float currentJumpTimer;
    Rigidbody rb;
    bool isInitialized = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentJumpTimer = jumpInterval;
        this.gameObject.SetActive(false);
    }

    public void onNetSpawn()
    {
        Debug.Log("Defender spawned!");
        this.gameObject.SetActive(true);
        basket = GameObject.Find("Net").transform;
        isInitialized = true;
        Debug.Log("Basket position: " + basket.position);
    }

    void FixedUpdate()
    {
        if (!isInitialized || basket == null || playerPos == null) return;

        // Position defender between player and basket
        Vector3 midPoint = Vector3.Lerp(playerPos.position, basket.position, 0.5f);
        Vector3 targetPosition = new Vector3(midPoint.x, transform.position.y, midPoint.z);
        
        // Move towards the target position
        Vector3 newPosition = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        rb.MovePosition(newPosition);

        // Handle jumping timer
        currentJumpTimer -= Time.deltaTime;
        
        if (currentJumpTimer <= 0)
        {
            Jump();
            currentJumpTimer = jumpInterval;
        }
    }

    void Jump()
    {
        Vector3 jumpVector = new Vector3(0, jumpForce, 0);
        rb.AddForce(jumpVector, ForceMode.Impulse);
        Debug.Log("Defender jumped!");
    }
}