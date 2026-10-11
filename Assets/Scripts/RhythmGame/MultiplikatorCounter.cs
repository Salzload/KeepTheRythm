using UnityEngine;

public class MultiplikatorCounter : MonoBehaviour
{
    [SerializeField] private int minComboForBonus = 5;
    [SerializeField] private float lovePerComboPoint = 0.02f;

    private int currentCombo = 0;
    private int multiplikator = 1;

    public void breakMult()
    {
        if (currentCombo >= minComboForBonus)
        {
            float bonusLove = currentCombo * lovePerComboPoint;
            GameEvents.OnLoveAdded?.Invoke(bonusLove);
            Debug.Log("Combo Break! Bonus erhalten: " + bonusLove);
        }

        currentCombo = 0;
        multiplikator = 1;

        GameEvents.OnComboChanged?.Invoke(currentCombo, multiplikator);
    }

    public void hitNote()
    {
        currentCombo++;
        CheckMultiplikator();
        GameEvents.OnComboChanged?.Invoke(currentCombo, multiplikator);
    }

    public void perfectNote()
    {
        currentCombo++;
        CheckMultiplikator();
        GameEvents.OnComboChanged?.Invoke(currentCombo, multiplikator);
    }

    private void CheckMultiplikator()
    {
        if (currentCombo >= 30) multiplikator = 8;
        else if (currentCombo >= 20) multiplikator = 4;
        else if (currentCombo >= 10) multiplikator = 2;
        else multiplikator = 1;
    }

    public int getMult()
    {
        return multiplikator;
    }

    public int getCombo()
    {
        return currentCombo;
    }
}