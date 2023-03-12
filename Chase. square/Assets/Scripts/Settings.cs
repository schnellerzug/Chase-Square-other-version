using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] private Animator soundSlider;
    [SerializeField] private Animator vibrationSlider;

    private void OnEnable()
    {
        soundSlider.SetBool("On", Storage.instance.sounds);
        vibrationSlider.SetBool("On", Storage.instance.vibration);

    }



    public void Sound()
    {
        Storage.instance.sounds = !Storage.instance.sounds;
        soundSlider.SetBool("On", Storage.instance.sounds);
        Storage.instance.SaveGame();
    }

    public void Vibration()
    {
        Storage.instance.vibration = !Storage.instance.vibration;
        vibrationSlider.SetBool("On", Storage.instance.vibration);
        Storage.instance.SaveGame();
    }
}
