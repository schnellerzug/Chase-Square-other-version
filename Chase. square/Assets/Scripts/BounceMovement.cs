using UnityEngine;

public class BounceMovement : MonoBehaviour
{
    [SerializeField] private float speed;

    private Vector3 direction;
    private float high;
    private float widht;
    //private Vector2 bounds;

    private void Start()
    {
        //bounds = GetComponent<SpriteRenderer>().bounds.size;
        high = Camera.main.orthographicSize;
        widht = high * Camera.main.aspect;
        //high -= bounds.y;
        //widht -= bounds.x;
        direction = (Vector3.zero - transform.position).normalized;
    }


    // Update is called once per frame
    void Update()
    {
        transform.position += speed * GameManager.instance.speed * direction * Time.deltaTime;
        if (transform.position.x > widht)
        {
            direction.x *= -1;
        }
        else if (transform.position.x < -widht)
        {
            direction.x *= -1;
        }
        if (transform.position.y > high)
        {
            direction.y *= -1;
        }
        else if (transform.position.y < -high)
        {
            direction.y *= -1;
        }
    }
}
