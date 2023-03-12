using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class ClutterSpawnwer : MonoBehaviour
{

    [SerializeField] private Prefab[] prefabs;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private float[] duration;
    [SerializeField] private int startAmount;

    [SerializeField] private Vector2[] minMaxSpeed;
    [SerializeField] private Vector2[] minMaxSize;
    [SerializeField] private Vector2 minMaxHeight;

    [HideInInspector] public Obstacle[] childs;

    private int phase;
    private float maxSpriteHeight;


    private void OnEnable()
    {


        maxSpriteHeight = sprites.Max(x => x.bounds.size.y);
        for (int i = 0; i < prefabs.Length; i++)
        {
            prefabs[i].id = i;
        }

        StartCoroutine(Checking());
    }
    public IEnumerator Checking()
    {
        if (GameManager.instance == null)
            yield return null;

        

        Starting();
    }
    public void Starting()
    {
      

        StartCoroutine(Spawning());
        for (int i = 0; i < startAmount; i++)
        {
            SpawnClutter(true);
        }
    }

    private IEnumerator Spawning()
    {

        if (!GameManager.instance.isRunning)
            yield return null;

        while (GameManager.instance.isRunning)
        {   
            SpawnClutter(true);
            phase = GameManager.instance.phase;
            yield return new WaitForSeconds(duration[phase] / GameManager.instance.speed);
            
            
            

        }
    }

    void SpawnClutter(bool outsidecamera)
    {

        var clutter = GetAvailableClutter();
        var cluttersprite = sprites[(int)(Random.value * sprites.Length)];
        var xposition = -3f;
        var horizontalCameraSize = mainCamera.orthographicSize * 2 * mainCamera.aspect;
        xposition += outsidecamera ?
            -horizontalCameraSize / 2
            : horizontalCameraSize * Random.value - horizontalCameraSize / 2;
        var yposition = minMaxHeight.x + (minMaxHeight.y - minMaxHeight.x) * Random.value;
        var size = minMaxSize[phase].x + (minMaxSize[phase].y - minMaxSize[phase].x) * Random.value;
        size *= maxSpriteHeight / cluttersprite.bounds.size.y;
        var localposition = new Vector3(xposition, yposition, maxSpriteHeight / cluttersprite.bounds.size.y);
        var speed = minMaxSpeed[phase].x + (minMaxSpeed[phase].y - minMaxSpeed[phase].x) * Random.value;
        clutter.transform.localScale = new Vector3(size, size, 0);
        clutter.transform.position = localposition + (Vector3)(Vector2)mainCamera.transform.position;
        clutter.GetComponent<ForwardMover>().speed = speed;
        clutter.GetComponent<SpriteRenderer>().sprite = cluttersprite;
        var c = clutter.GetComponent<BoxCollider2D>();
        /*if(c != null)
        {
            c.size *= cluttersprite.bounds.size.y / maxSpriteHeight;
        }*/



    }

    public GameObject GetAvailableClutter()
    {
        var pf = RandomPrefab();
       
        foreach (Obstacle g in childs)
        {
            if (g == null)
                break;
            if (!g.gameObject.activeInHierarchy)
            {
                if(g.tag == "Planet")
                {
                    if (g.id == pf.id)
                    {
                        g.gameObject.SetActive(true);
                        return g.gameObject;
                    }
                }
            



            }
        }

        var ng = Instantiate(pf.prefab, transform);
        ng.GetComponent<Obstacle>().id = pf.id;
        childs = transform.GetComponentsInChildren<Obstacle>(true);
        return ng;

    }

    [Serializable]
    public class Prefab
    {
        public GameObject prefab;
        public int id;
        public float[] probability;
    }

    public Prefab RandomPrefab()
    {
        var a = 0f;
        var e = Random.value;
        
            
        
        for (int i = 0; i < prefabs.Length; i++)
        {

            if (e <= (prefabs[i].probability[phase] + a))
            {
                
                return prefabs[i];
            }
            else
            {
                a += prefabs[i].probability[phase];
            }

        }
        return null;
    }
}
