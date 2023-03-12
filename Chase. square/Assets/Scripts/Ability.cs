using System;
using System.Linq;
using UnityEngine;

public class Ability : MonoBehaviour
{
    public enum AbilityType
    {
        BiggerShip,
        SmallerShip,
        TimeStop
    }

    public AbilityType type;
    public float duration;
    private float lifeTime;
    [SerializeField] private SpriteAssignment[] spriteAssignment;

    private void OnEnable()
    {   // dice the type, set the appropriate sprite
        RandomizeType();
        GetComponent<SpriteRenderer>().sprite = spriteAssignment.FirstOrDefault(x => x.type == type)?.sprite;
        lifeTime = duration;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            gameObject.SetActive(false);
        if (!GameManager.instance.isRunning)
            gameObject.SetActive(false);
    }
    private void RandomizeType()
    {
        var number = UnityEngine.Random.value;

        if (number < 0.33f)
        {
            type = AbilityType.BiggerShip;
            return;
        }

        if (number < 0.66f)
        {
            type = AbilityType.SmallerShip;
            return;
        }

        type = AbilityType.TimeStop;
    }

    [Serializable]
    private class SpriteAssignment
    {
        public AbilityType type;
        public float duration;
        public float power;
        public Sprite sprite;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == GameManager.instance.player.gameObject)
        {
            GameManager.instance.Activate(type, spriteAssignment.FirstOrDefault(x => x.type == type).duration, spriteAssignment.FirstOrDefault(x => x.type == type).power);
            gameObject.SetActive(false);
        }
    }


}

