using System.Collections.Generic;
using UnityEngine;

public class CirclingObstacle : Obstacle
{

    [SerializeField] private List<CirclingObstacles_child> obstacles;
    [SerializeField] private GameObject obstacle;

    [SerializeField] private Vector2[] minMaxChilds;
    [SerializeField] private Vector2[] minMaxDis;
    [SerializeField] private Vector2[] minMaxSpeed;

    [SerializeField] private Vector2[] minMaxSize;

    private int phase;
    // Start is called before the first frame update
    void OnEnable()
    {
        phase = GameManager.instance.phase;
        var ca = minMaxChilds[phase].x + (minMaxChilds[phase].y - minMaxChilds[phase].x) * Random.value;
        obstacles = new List<CirclingObstacles_child>(10);
        obstacles.Clear();
        
        
        var ac = transform.GetComponentsInChildren<Obstacle>(true);
        if(ac.Length-1 > ca)
        {
            for (int i = (int)ca; i < ac.Length-1; i++)
            {
                Destroy(ac[i].gameObject);
                
            }
        }
        else if (ac.Length-1 < ca)
        {
            for (int i = ac.Length-1; i < (int)ca; i++)
            {
                Instantiate(obstacle,transform);
                
            }
        }
        RandomValue();

    }

    void RandomValue()
    {
        var ad = transform.GetComponentsInChildren<Obstacle>(true);
        for (int i = 0; i < (ad.Length - 1); i++)
        {
            var ob = new CirclingObstacles_child();
            
            ob.obstacle = ad[i+1];
            
            ob.distance = minMaxDis[phase].x + (minMaxDis[phase].y - minMaxDis[phase].x) * Random.value;
            var sp = minMaxSpeed[phase].x + (minMaxSpeed[phase].y - minMaxSpeed[phase].x) * Random.value;
            ob.speed = i % 2 == 0 ? sp * -1 : sp;
            obstacles.Add(ob);
            var size = minMaxSize[phase].x + (minMaxSize[phase].y - minMaxSize[phase].x) * Random.value;
            ad[i + 1].transform.localScale = new Vector2(size, size); 
            ad[i].gameObject.SetActive(true);
        }


    }

    // Update is called once per frame
    void Update()
    {
        foreach (var o in obstacles)
        {
            if(o != null)
                o.obstacle.transform.position = new Vector3(Mathf.Sin(Time.time * o.speed + o.offset) * o.distance, Mathf.Cos(Time.time * o.speed + o.offset) * o.distance, 0) + transform.position;
        }
    }
}

