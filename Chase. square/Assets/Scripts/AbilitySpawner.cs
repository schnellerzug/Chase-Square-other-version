using System.Collections;
using UnityEngine;

public class AbilitySpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private Vector2 minMaxduration;
    [SerializeField] private Vector2 minMaxSpeed;

    [HideInInspector] public Transform[] childs;

    private void Awake()
    {
        StartCoroutine(Spawning());
    }

    public IEnumerator Spawning()
    {
        if (GameManager.instance == null)
            yield return null;

        if (!GameManager.instance.isRunning)
            yield return null;

        while (GameManager.instance.isRunning)
        {

            yield return new WaitForSeconds((minMaxduration.x + (minMaxduration.y - minMaxduration.x) * Random.value) / GameManager.instance.speed);
            SpawnAbility();
        }
    }

    void SpawnAbility()
    {
        var go = GetAvailableAbility();
        var spawnside = Random.Range(0, 4);
        var speed = minMaxSpeed.x + (minMaxSpeed.y - minMaxSpeed.x) * Random.value;
        var xposition = 0f;
        var yposition = 0f;
        var high = mainCamera.orthographicSize;
        var widht = high * mainCamera.aspect;
        if (spawnside == 0)
        {
            yposition += high;
            xposition = -widht + (widht - -widht) * Random.value;
        }
        else if (spawnside == 1)
        {
            xposition += widht;
            yposition = -high + (high - -high) * Random.value;
        }
        else if (spawnside == 2)
        {
            yposition -= high;
            xposition = -widht + (widht - -widht) * Random.value;
        }
        else if (spawnside == 3)
        {
            xposition -= widht;
            yposition = -high + (high - -high) * Random.value;
        }
        go.transform.position = new Vector3(xposition, yposition, 0);
        go.SetActive(true);

    }

    public GameObject GetAvailableAbility()
    {
        childs = transform.GetComponentsInChildren<Transform>(true);
        foreach (Transform g in childs)
        {

            if (!g.gameObject.active)
            {


                return g.gameObject;
            }
        }

        return Instantiate(prefab, transform);
    }
}
