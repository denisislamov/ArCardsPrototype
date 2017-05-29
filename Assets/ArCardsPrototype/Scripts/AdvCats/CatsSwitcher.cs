using UnityEngine;
using System.Collections;

public class CatsSwitcher : MonoBehaviour
{
    public GameObject[] ObjectList;
    private int _index = 0;

    public void Next()
    {
        ObjectList[_index].gameObject.SetActive(false);
        _index++;
        if (_index >= ObjectList.Length)
        {
            _index = 0;
        }

        ObjectList[_index].gameObject.SetActive(true);
    }

    public void Prev()
    {
        ObjectList[_index].gameObject.SetActive(false);
        _index--;
        if (_index < 0)
        {
            _index = ObjectList.Length - 1;
        }
        ObjectList[_index].gameObject.SetActive(true);
    }
}
