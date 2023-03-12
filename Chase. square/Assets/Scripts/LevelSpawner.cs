using System.Collections.Generic;
using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private Camera mainCamera;
    private List<GameObject> childs = new List<GameObject>();
    private void OnEnable()
    {
        mainCamera = Camera.main;
    }
    public void SpawnLevel(int level)
    {
        if (level >= Storage.instance.actuelLevel.levels.Length)
        {
            print("Hast Planet abgeschlossen");
            return;
        }
        var l = Instantiate(Storage.instance.actuelLevel.levels[level].gameObject, transform);
        childs.Add(l);
        var horizontalCameraSize = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        var xposition = -horizontalCameraSize;
        var yposition = 0;
        l.transform.position = new Vector3(xposition, yposition);
        print(l);

    }

    public void DestroyAll()
    {
        foreach (var t in childs)
        {
            Destroy(t);
        }
    }
}
