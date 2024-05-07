using UnityEngine;
public class Easing : MonoBehaviour
{
    public Vector3 movementDirection = Vector3.left;
    public float speed = 2f;
    public float delayDirection = 2f;

    private int direction = 1;

    void Awake()
    {
        InvokeRepeating("InvertDirection", delayDirection, delayDirection);
    }

    void Update()
    {
        transform.Translate(movementDirection * direction * speed * Time.deltaTime, Space.World);
    }

    void InvertDirection()
    {
        direction *= -1;
    }
}
