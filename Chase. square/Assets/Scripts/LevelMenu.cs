using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using static UpgradeHolder;

public class LevelMenu : MonoBehaviour
{

    public GameObject[] planets;
    public Skin[] skins;

    [SerializeField] private Text text;
    [SerializeField] private Text error;
    [SerializeField] private Text coins;

    [SerializeField] private float scaleMult;
    [SerializeField] private float planetDis;

    private Vector2 startPosition;
    private Vector2 lastPosition;
    private Vector2 diff;

    private bool stopTouch;
    private bool moved = true;

    [SerializeField] private float minRange;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int planetsInRow = 3;

    public int actuelPlanet;

    public void Update()
    {
        TouchInput();

    }

    public void Start()
    {
        skins = Storage.instance.playerSkins;
        actuelPlanet = 0;
        coins.text = Storage.instance.coins.ToString();
        ChangeDesign();
        for (int i = 0; i < planets.Length; i++)
        {
            if (skins[i].hasBuy)
            {
                planets[i].GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
        foreach (var target in planets)
        {
            target.transform.localScale = new Vector3(1.3f - target.transform.position.x * target.transform.position.x * scaleMult, 1.3f - target.transform.position.x * target.transform.position.x * scaleMult, 1);
        }

    }

    public void Button(bool right)
    {
        if (!moved)
        {
            return;
        }
        if (right)
        {
            StartCoroutine(MovePlanets(true));
        }
        else
        {
            StartCoroutine(MovePlanets(false));
        }
    }


    public void TouchInput()
    {
        if (Input.touchCount > 0)
        {

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;

            }

            if (touch.phase == TouchPhase.Moved)
            {
                lastPosition = touch.position;
                diff = lastPosition - startPosition;
                if (!stopTouch)
                {
                    if (diff.x > minRange)
                    {



                        StartCoroutine(MovePlanets(true));



                        startPosition = touch.position;
                        stopTouch = true;
                    }
                    if (diff.x < -minRange)
                    {


                        StartCoroutine(MovePlanets(false));


                        startPosition = touch.position;

                        stopTouch = true;
                    }
                }




            }

            if (touch.phase == TouchPhase.Ended)
            {
                //stopTouch = false;
            }


        }
    }





    public IEnumerator MovePlanets(bool right)
    {
        moved = false;
        foreach (var planet in planets)
        {
            StartCoroutine(LerpPosition(planet.transform, right ? new Vector3(planet.transform.position.x + planetDis, 0, 0) : new Vector3(planet.transform.position.x - planetDis, 0, 0), 0.5f));
        }
        while (!moved)
        {
            yield return null;
        }
        Move(right);
    }

    public IEnumerator LerpPosition(Transform target, Vector3 endPosition, float duration)
    {




        var time = 0f;


        var startPosition = target.position;
        while (time < duration)
        {
            target.localScale = new Vector3(1.3f - target.transform.position.x * target.transform.position.x * scaleMult, 1.3f - target.transform.position.x * target.transform.position.x * scaleMult, 1);
            target.position = startPosition + (endPosition - startPosition) * (time / duration);
            time += Time.deltaTime;
            yield return false;
        }

        target.transform.position = endPosition;
        target.localScale = new Vector3(1.3f - target.transform.position.x * target.transform.position.x * scaleMult, 1.3f - target.transform.position.x * target.transform.position.x * scaleMult, 1);

        stopTouch = false;
        moved = true;




    }

    public void Move(bool right)
    {

        var dir = right ? 1 : -1;

        var i = actuelPlanet + planetsInRow * dir;
        if (i < 0)
            i = planets.Length + i;
        else if (i > planets.Length - 1)
            i = i - planets.Length;
        print(i + "i");

        planets[i].SetActive(false);
        var r = actuelPlanet - (planetsInRow + 1) * dir;

        if (r < 0)
            r = planets.Length + r;
        else if (r > planets.Length - 1)
            r = r - planets.Length;




        planets[r].SetActive(true);
        planets[r].transform.position = new Vector3((float)planetsInRow * planetDis * (float)-dir, 0, 0);
        planets[r].transform.localScale = new Vector3(1.3f - planets[r].transform.position.x * planets[r].transform.position.x * scaleMult, 1.3f - planets[r].transform.position.x * planets[r].transform.position.x * scaleMult, 1);


        actuelPlanet -= 1 * dir;
        if (actuelPlanet < 0)
        {
            actuelPlanet = planets.Length - 1;
        }
        if (actuelPlanet > planets.Length - 1)
        {
            actuelPlanet = 0;
        }

        ChangeDesign();

    }

    public void ChangeDesign()
    {

        if (!skins[actuelPlanet].hasBuy)
        {
            text.text = skins[actuelPlanet].price.ToString();

        }
        else if (actuelPlanet == Storage.instance.playerSkin)
        {
            text.text = "Equiped";
        }
        else
        {
            text.text = "Equip";
        }
        Storage.instance.playerSkins = skins;
        
        Storage.instance.SaveGame();
    }

    public void OnButtonClick()
    {
        if (!skins[actuelPlanet].hasBuy)
        {
            Buy();
        }
        else if (actuelPlanet == Storage.instance.playerSkin)
        {
            return;
        }
        else
        {
            Equip();
        }
    }

    void Buy()
    {
        //check if fulfill conditions
        if (Storage.instance.coins < skins[actuelPlanet].price)
        {
            error.text =  ("You need " + (skins[actuelPlanet].price - Storage.instance.coins) + " more coins!");
            StartCoroutine(ChangeColorAndBack(0.3f,error,Color.clear,Color.red,1f));;
            StartCoroutine(ChangeColorAndBack(0.1f, coins,Color.white, Color.red, 1f));
            return;
        }
        else if (Storage.instance.highScore < skins[actuelPlanet].minHigh)
        {
            print("You need a higher highscore");
            return;
        }

        //take coins
        StartCoroutine(StartSubtracting(Storage.instance.coins, Storage.instance.coins - skins[actuelPlanet].price, 1f));
        Storage.instance.coins -= skins[actuelPlanet].price;
        

        error.text = "You bought " + skins[actuelPlanet].name;
        StartCoroutine(ChangeColorAndBack(0.3f, error, Color.clear, Color.green, 1f));

        skins[actuelPlanet].hasBuy = true;
        planets[actuelPlanet].GetComponent<SpriteRenderer>().color = Color.white;
        ChangeDesign();
    }

    void Equip()
    {
        Storage.instance.playerSkin = actuelPlanet;
        error.text = "You equiped " + skins[actuelPlanet].name;
        StartCoroutine(ChangeColorAndBack(0.3f, error, Color.clear, Color.white, 1f));
        ChangeDesign();
    }


    IEnumerator ChangeColorAndBack(float duration, Text text, Color startColor, Color endColor, float waitDuration)
    {
        var time = 0f;


        while (time < duration)
        {

            text.color = startColor + (endColor - startColor) * (time / duration);
            time += Time.deltaTime;
            yield return false;
        }
        text.color = endColor;
        yield return new WaitForSeconds(waitDuration);
        time = 0f;
        while (time < duration)
        {

            text.color = endColor + (startColor - endColor) * (time / duration);
            time += Time.deltaTime;
            yield return false;
        }
        text.color = startColor;
    }

    IEnumerator StartSubtracting(float startValue, float endValue, float lerpDuration)
    {
        float timeElapsed = 0;
        float valueToLerp;

        while (timeElapsed < lerpDuration)
        {
            valueToLerp = Mathf.Lerp(startValue, endValue, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            coins.text = valueToLerp.ToString("F0");
            yield return null;
        }
        coins.text = endValue.ToString("F0");


    }
}
