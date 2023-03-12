

public class SignalObstacle : ForwardMover
{
    protected override void Update()
    {
        base.Update();
        if (transform.position.x > 10f)
        {
            GameManager.instance.NextLevel();
            return;
        }
    }

}
