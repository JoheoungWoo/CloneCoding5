using UnityEngine;

public class Fish : MonoBehaviour
{
    private GameObject playerObj;
    public FishType type = FishType.None;

    [SerializeField]
    private float speed = 10.0f;

    private void Start()
    {
        playerObj = GameObject.FindWithTag("Player");
    }


    private void Update()
    {
        if (playerObj == null)
            return;

        Vector3 targetPosition = Vector3.zero; // 목적지 (원점)
        Vector3 direction = targetPosition - transform.position;

        if (Vector3.Distance(playerObj.transform.position,transform.position) < 30f)
        {
            direction = playerObj.transform.position - transform.position;
            direction.Normalize();

            // 각도 계산
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector3 movement = direction * speed * Time.deltaTime;
            transform.position += new Vector3(movement.x, movement.y, 0);

            return;
        }



        if (direction.magnitude > 1f)
        {
            direction.Normalize();

            // 각도 계산
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector3 movement = direction * speed * Time.deltaTime;
            transform.position += new Vector3(movement.x, movement.y, 0);
        }
    }


}

