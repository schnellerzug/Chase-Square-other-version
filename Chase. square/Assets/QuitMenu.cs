
using UnityEngine;

public class QuitMenu : MonoBehaviour
{
    public void Back()
    {
        gameObject.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
