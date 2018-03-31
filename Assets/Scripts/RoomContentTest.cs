using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomContentTest : MonoBehaviour
{

    void Start ()
    {
        StartCoroutine(LogContent());
    }

    private IEnumerator LogContent()
    {
        while (true)
        {
            if (LevelManager.Instance.CurrentRoom != null)
            {
                var str = "";
                foreach (var content in LevelManager.Instance.RoomContentManager.GetObjects(LevelManager.Instance.CurrentRoom))
                {
                    str += (content as MonoBehaviour).name + " ";
                }
                if (!str.Equals(""))
                {
                    Debug.Log(str);
                }
            }

            yield return new WaitForSeconds(2);
        }
    }
}
