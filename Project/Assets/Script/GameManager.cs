using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;
using static Helper;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] cats;
    private int indexCat;
    private int numCats;
    public GameObject catFace;
    public Texture2D catFaceTextures;
    public GameObject hudbar;
    public GameObject text;
    public GameObject subtext;
    public GameObject titletext;
    private Sprite[] catFaceSprites;
    private bool changeCat;
    private Vector3 startPosition;
    private Vector3 finishPosition;
    private float changeTime;
    private float timer;
    public GameObject panel;
    private bool gameStart;
    // Start is called before the first frame update
    void Start()
    {
        numCats = cats.Length;
        indexCat = 0;
        gameStart = false;
        //Parametros para el cambio de gato.
        catFaceSprites = Resources.LoadAll<Sprite>(catFaceTextures.name);
        changeCat = false;
        startPosition =  new Vector3(-1.6f, -3.179237f, -21.69888f);
        finishPosition =  new Vector3(-13.7f, -52.2f, -21.69888f);
        changeTime = 3.0f;
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                cats[0].SetActive(true);
                catFace.SetActive(true);
                hudbar.SetActive(true);
                text.SetActive(false);
                subtext.SetActive(false);
                titletext.SetActive(false);
                gameStart = true;
            }

        }
        else
        {
            if (changeCat)
            {
                timer += Time.deltaTime;
                if (timer > changeTime / 2.0f && timer < changeTime && cats[indexCat].activeSelf)
                {
                    cats[indexCat].SetActive(false);
                    cats[indexCat + 1].SetActive(true);
                    catFace.GetComponent<Image>().sprite = catFaceSprites[indexCat + 1];
                    StartCoroutine(Helper.MoveToPosition(cats[indexCat + 1], startPosition, changeTime / 2.0f));
                }
                else if (timer > changeTime)
                {
                    changeCat = false;
                    ++indexCat;
                }
            }
        }
    }

    public void OnAllNailsCutted()
    {
        numCats--;
        if (numCats < 1)
        {
            panel.SetActive(true);
            GameObject.FindWithTag("Scissors").SetActive(false);
            Cursor.visible = true;
        }
        else
        {
            Debug.Log("Cambiando gato");
            StartCoroutine(Helper.MoveToPosition(cats[indexCat], finishPosition, changeTime/2.0f));
            timer = 0;
            changeCat = true;
        }
    }

    public int getIndexCat()
    {
        return indexCat;
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Prueba");
    }
}
