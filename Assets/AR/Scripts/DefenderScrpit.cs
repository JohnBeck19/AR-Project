using UnityEngine;

public class DefenderScrpit : MonoBehaviour
{
    [SerializeField] Transform playerPos;
    [SerializeField] Transform basket;
    [SerializeField] float jumpTimmer;
    float currentTime;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentTime = jumpTimmer;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 playerFromBasket = (basket.position - playerPos.position).normalized;

        Vector3 target = playerPos.position + playerFromBasket * 3f;
        Vector3 tagertXZ = new Vector3(target.x, 0, target.z);
     
        Vector3 newPos = Vector3.MoveTowards(rb.position, tagertXZ, 3f * Time.deltaTime);
        rb.MovePosition(newPos);
        currentTime -= Time.deltaTime;
        Debug.Log(target);
        if (currentTime <= 0)
        {
            Vector3 up = new Vector3(0, 10, 0);
            rb.AddRelativeForce(up, ForceMode.Impulse);
            currentTime = jumpTimmer;
        }
    }
}
