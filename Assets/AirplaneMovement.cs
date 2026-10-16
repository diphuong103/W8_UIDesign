using UnityEngine;

public class AirplaneMovement : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Máy bay tiến tới theo trục Z
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}