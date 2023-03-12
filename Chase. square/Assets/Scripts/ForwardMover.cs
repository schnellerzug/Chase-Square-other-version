using UnityEngine;

public class ForwardMover : MonoBehaviour
{
    public float speed;
    public float rotationSpeed = 1;
    protected virtual void Update()
    {
        //Move Object
        transform.position += Vector3.right * speed * GameManager.instance.speed * Time.deltaTime;
        //Rotate Object
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationSpeed * speed);


        //deactivated Object if too far away
        if (transform.position.x > 15f)
        {

            gameObject.SetActive(false);
        }
    }
    public void OnDrawGizmosSelected()
    {
        var r = GetComponent<Renderer>();
        if (r == null)
            return;
        var bounds = r.bounds;
        Gizmos.matrix = Matrix4x4.identity;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireCube(bounds.center, bounds.extents * 2);
    }
}
