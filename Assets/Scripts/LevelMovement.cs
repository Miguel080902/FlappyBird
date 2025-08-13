using UnityEngine;

public class LevelMovement : MonoBehaviour
{
    public float speed = 5f; // Velocidad de movimiento del nivel
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }
}
