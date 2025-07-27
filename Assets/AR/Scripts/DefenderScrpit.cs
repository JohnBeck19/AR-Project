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
        //float distance = Vector3.Distance(this.transform.position, playerPos.position);
        //if (distance >= 1f)
        //{
        //    Vector3 newPos = Vector3.MoveTowards(this.transform.position, playerPos.position, 0.5f * Time.deltaTime);
        //    rb.MovePosition(newPos);
        //}
        rb.MovePosition(target);
        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            Vector3 up = new Vector3(0, 3, 0);
            rb.AddRelativeForce(up, ForceMode.Impulse);
            currentTime = jumpTimmer;
        }
    }
}
