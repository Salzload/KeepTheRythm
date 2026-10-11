using UnityEngine;
using UnityEngine.UI;
public class SliderController : MonoBehaviour
{
    public Slider slider;
    public int maxProgress = 100;
    public float progress = 0;

    void Start()
    {
        if (slider == null) slider = GetComponent<Slider>();

        slider.minValue = 0;
        slider.maxValue = maxProgress;
        slider.value = 0;
    }
    public void addProgess()
    {
        if (progress <= maxProgress)
        {
            progress++;
            slider.value = progress;
            Debug.Log($"Progress: {progress} / {maxProgress}");
        }
        else
        {
            Debug.Log("GEWONNEN!");
        }
    }
    public void removeProgess()
    {
        if (progress > 0)
        {
            progress = progress - 0.5f;
            slider.value = progress;
            Debug.Log($"Progress: {progress} / {maxProgress}");
        }
        else
        {
            Debug.Log("VERLOREN!");
        }
    }
}
