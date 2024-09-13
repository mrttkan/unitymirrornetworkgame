using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < -10.5f)
        {
            Destroy(gameObject);
        }
    }
}
