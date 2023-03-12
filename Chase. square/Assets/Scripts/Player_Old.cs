using UnityEngine;

public class Player_Old : MonoBehaviour
{
    private Rigidbody2D rb;

    private Vector3 dif;
    private Vector3 lastPosition;

    private Vector3 lastDir;
    public float minDirChange;

    public float speed;
    public float minDis;

    public float rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        TouchInput();
    }

    public void TouchInput()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                lastPosition = Camera.main.ScreenToWorldPoint(touch.position);

            }
            if (touch.phase == TouchPhase.Moved)
            {



                var actuelPosition = Camera.main.ScreenToWorldPoint(touch.position);
                dif = actuelPosition - lastPosition;
                if (dif.magnitude > minDirChange)
                {
                    Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dif.normalized);
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

                }
                actuelPosition.z = 0;


                lastPosition = actuelPosition;



            }
            if (touch.phase == TouchPhase.Ended)
            {
                dif = Vector3.zero;
            }


        }
    }

    private void FixedUpdate()
    {
        /*if(dif.magnitude > minDis)
        {
            if(lastDir == Vector3.zero || (lastDir - dif.normalized).magnitude * (lastDir - dif.normalized).magnitude > minDirChange)
            {
                print("zsbzg");
                
                Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dif.normalized);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);
                lastDir = dif.normalized;
            }
           
            
            
        }*/


        if ((dif.x > 0 && rb.velocity.x < 0) || (dif.x < 0 && rb.velocity.x > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
            print("hello");
        }
        if ((dif.y > 0 && rb.velocity.y < 0) || (dif.y < 0 && rb.velocity.y > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            print("hello");
        }

        rb.AddForce((Vector2)dif * speed * Time.fixedDeltaTime);

    }
}
