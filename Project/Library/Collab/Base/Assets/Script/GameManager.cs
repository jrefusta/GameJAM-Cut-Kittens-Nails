using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper;

public class GameManager : MonoBehaviour
{
    public GameObject[] cats;
    private int indexCat;
    private int numCats;
    private bool changeCat;
     private Vector3 startPosition;
    private Vector3 finishPosition;
    private float changeTime;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        numCats = cats.Length;
        indexCat = 0;

        //Parametros para el cambio de gato.
        changeCat = false;
        startPosition =  new Vector3(-0.3777065f, 1.088119f, -18.60396f);
        finishPosition =  new Vector3(-13.7f, -52.2f, -21.69888f);
        changeTime = 5.0f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (changeCat)
        {
            timer += Time.deltaTime;
            if (timer > changeTime/2.0f && timer < changeTime && cats[indexCat].activeSelf)
            {
                cats[indexCat].SetActive(false);
                cats[indexCat + 1].SetActive(true);
                StartCoroutine(Helper.MoveToPosition(cats[indexCat + 1], startPosition, changeTime/2.0f));
            }
            else if(timer > changeTime)
            {
                changeCat = false;
                ++indexCat;
            }
        }
    }

    public void OnAllNailsCutted()
    {
        numCats--;
        if (numCats < 1)
        {
        Debug.Log("FELIS I DADES!");
        }
        else
        {
            Debug.Log("Cambiando gato");
            StartCoroutine(Helper.MoveToPosition(cats[indexCat], finishPosition, changeTime/2.0f));
            timer = 0;
            changeCat = true;
        }
    }
}
