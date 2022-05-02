using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Helper
{
    public static GameObject[] GetAllChilds(this GameObject Go)
    {
        Debug.Log(Go.transform.childCount);
        GameObject[] list = new GameObject[Go.transform.childCount];
        for (int i = 0; i< Go.transform.childCount; i++)
        {
            list[i] = Go.transform.GetChild(i).gameObject;
        }
        return list;
    }

    public static IEnumerator MoveToPosition(GameObject gameObject, Vector3 position, float timeToMove)
    {
        var currentPos = gameObject.transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            gameObject.transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }
}
