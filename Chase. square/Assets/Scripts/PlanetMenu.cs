using UnityEngine;

public class PlanetMenu : MonoBehaviour
{
    public void ChangePlanet(int id)
    {
        Storage.instance.actuelLevel = Storage.instance.planets[id];
        SceneLoader.instance.ChangeScene("Planet", true);
    }
}
