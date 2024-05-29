using UnityEngine;

public abstract class Loot : MonoBehaviour
{
    public void Tacked()
    {
        gameObject.SetActive(false);
    }
}