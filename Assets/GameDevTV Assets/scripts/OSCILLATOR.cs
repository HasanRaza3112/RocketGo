using UnityEngine;

public class OSCILLATOR : MonoBehaviour
{
   Vector3 startposition;
   Vector3 endposition;
   [SerializeField] float speed;
   [SerializeField] Vector3 movementVector;
   float movementFactor;
  
    void Start()
    {
        startposition = transform.position;
        endposition = transform.position + movementVector;
    }

  
    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time*speed,1f);
        transform.position = Vector3.Lerp(startposition, endposition, movementFactor);
    }
}
