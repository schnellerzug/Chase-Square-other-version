
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private ParticleSystem particle;

    private Vector3 dif;

    private Joystick js;

    [SerializeField] private float speed = 5000;
    [SerializeField] private float rotationSpeed = 1000;


    // Start is called before the first frame update
    void Start()
    {
        js = FindObjectOfType<Joystick>();
        particle = GetComponentInChildren<ParticleSystem>();
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void FixedUpdate()
    {
        if (!GameManager.instance.isRunning)
            return;

        //create direction Vector3 with the joystick
        var x = js.Horizontal;
        var y = js.Vertical;
        dif = new Vector3(x, y, 0);
        if (dif.magnitude == 0)
        {
            particle.Stop();
            return;
        }

        //rotate
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, dif);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, rotationSpeed * Time.fixedDeltaTime);

        //Change direction easier
        if ((dif.x > 0 && rb.velocity.x < 0) || (dif.x < 0 && rb.velocity.x > 0))
        {
            rb.velocity = new Vector2(0, rb.velocity.y);

        }
        if ((dif.y > 0 && rb.velocity.y < 0) || (dif.y < 0 && rb.velocity.y > 0))
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);

        }

        //Add Movement
        particle.Play();

        rb.AddForce((Vector2)dif * speed * Storage.instance.playerSpeedMultiplikator * Time.fixedDeltaTime);

    }
}
