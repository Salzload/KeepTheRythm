using UnityEngine;

public class MultiplikatorCounter : MonoBehaviour
{
    int hitCount = 0;
    int multiplikator = 1;

    public void breakMult()
    {
        hitCount=0;
        multiplikator=1;
    }

    public void hitNote()
    {
        hitCount += 1;
        if (multiplikator < 8 && (hitCount%10)==0)
        {
            multiplikator *= 2;
        }
    }

    public void perfectNote()
    {
        hitCount += 2;
        if (multiplikator < 8 && ((hitCount%10)==0 || ((hitCount-1)%10)==0))
        {
            multiplikator *= 2;
        }
    }

    public int getMult()
    {
        return multiplikator;
    }
}
