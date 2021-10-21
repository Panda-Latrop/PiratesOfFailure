using UnityEngine;

public class Item : MonoBehaviour 
{
    public bool active = true;
    public string[] detectThisTags;
    void OnTriggerEnter2D(Collider2D coll)
    {
        if (active)
            if (TagEquals(coll.tag, detectThisTags))
                Action();

    }
    public virtual void Action()
    {
        return;
    }
    bool TagEquals(string gojT, string[] arrT)
    {
        for (int i = 0; i < arrT.Length; i++)
        {
            if (gojT == arrT[i])
                return true;
        }
        return false;
    }
}
