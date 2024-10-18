using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 10.0f;

    private void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");


        Vector3 direct = new Vector3(inputX, inputY, 0);
        Vector3 targetPosition = transform.position + direct;
        Vector3 direction = targetPosition - transform.position;
        if (direction.magnitude > 0.01f)
        {
      
            // 각도 계산
            float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            Vector3 movement = direction * speed * Time.deltaTime;
            transform.position += new Vector3(movement.x, movement.y, 0);
        }
    }
}

