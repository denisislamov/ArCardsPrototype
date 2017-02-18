using UnityEngine;
using System.Collections;

public class ToggleElementsByAnimation : MonoBehaviour
{
    [SerializeField] protected GameObject[] GameObjects;

    public void TurnOn()
    {
        foreach (var go in GameObjects)
        {
            go.SetActive(true);
        }
    }

    public void TurnOff()
    {
        foreach (var go in GameObjects)
        {
            go.SetActive(false);
        }
    }
}
