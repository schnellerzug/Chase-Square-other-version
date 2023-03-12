using UnityEngine;

public class Obstacle : MonoBehaviour
{
    [SerializeField] public bool deadly;
    [HideInInspector] public int id;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.player.gameObject)
        {
            print(collision.collider.GetType());
            if(collision.collider.GetType() == typeof(CapsuleCollider2D))
            {
                if (deadly)
                {
                    if(GameManager.instance.isRunning)
                        GameManager.instance.GameOver();
                }
            }
          

        }
    }
}
