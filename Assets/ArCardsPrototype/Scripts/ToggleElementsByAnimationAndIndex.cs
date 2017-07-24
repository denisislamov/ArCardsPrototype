using UnityEngine;

public class ToggleElementsByAnimationAndIndex : MonoBehaviour
{
    [System.Serializable]
    public class GameObjects
    {
        public GameObject[] Values;
    }

    [SerializeField] private GameObjects[] _gameObjects;
    
    public void TurnOn(int index)
    {
        foreach (var go in _gameObjects[index].Values)
        {
            go.SetActive(true);
        }
    }

    public void TurnOff(int index)
    {
        foreach (var go in _gameObjects[index].Values)
        {
            go.SetActive(false);
        }
    }

    public void TurnOffAll()
    {
        foreach (var gos in _gameObjects)
        {
            foreach (var go in gos.Values)
            {
                go.SetActive(false);
            }
        }
    }
}
