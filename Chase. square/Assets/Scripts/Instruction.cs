using UnityEngine.UI;
using UnityEngine;

public class Instruction : MonoBehaviour
{
    public InstructionSlide[] slides;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject rightButton;
    [SerializeField] private GameObject leftButton;
    [SerializeField] private GameObject exit;
    [SerializeField] private Image image;
    [SerializeField] private Text headline;
    [SerializeField] private Text description;
    [SerializeField] private Text slideAmount;
    
    

    private int actuelSlide;
    public void Start()
    {
        if (!Storage.instance.alreadyPlayed)
        {
            actuelSlide = 0;
            panel.SetActive(true);
            ChangeSlide();
        }
        else
        {
            panel.SetActive(false);
        }
     
    }

    private void ChangeSlide()
    {
        leftButton.SetActive(true);
        rightButton.SetActive(true);
        exit.SetActive(false);
        if (actuelSlide == 0)
        {
            leftButton.SetActive(false);
        }
        else if(actuelSlide == slides.Length - 1)
        {
            rightButton.SetActive(false);
            exit.SetActive(true);


        }

            
        var s = slides[actuelSlide];
        headline.text = s.headline;
        description.text = s.description;
        image.sprite = s.image;
        var r = image.GetComponent<RectTransform>();
        r.sizeDelta = new Vector2(s.imageWidth, s.imageHeight);
        slideAmount.text = actuelSlide + 1 + "/" + slides.Length;
    }

    public void NextSlide(bool right)
    {
        actuelSlide += right ? 1 : -1;
        if(actuelSlide >= slides.Length)
        {
            panel.SetActive(false);
            return;
        }

        ChangeSlide();
    }
}
