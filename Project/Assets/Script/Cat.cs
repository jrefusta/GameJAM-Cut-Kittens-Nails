using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper;
using UnityEngine.SceneManagement;

public class Cat : MonoBehaviour
{
    private GameObject[] paws;
    private int numPaws;
    private int stress;
    private GameObject hudStress;
    public GameObject panel; // Podemos tener hasta un maximo de 10 en stress.

    // Start is called before the first frame update
    void Start()
    {
        paws = Helper.GetAllChilds(this.gameObject);
        numPaws = paws.Length;
        stress = 0;
        hudStress = GameObject.Find("Bar");
        hudStress.GetComponent<RectTransform>().sizeDelta = new Vector2(0, hudStress.GetComponent<RectTransform>().sizeDelta.y);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnAllNailsInPawCutted()
    {
        --numPaws;
        Debug.Log("Patas restantes: " + numPaws);
        if (numPaws == 0 && stress < 10)
        {
            GameObject.Find("GameManager").GetComponent<GameManager>().OnAllNailsCutted();
        }
    }

    public void OnCutInDangerZone(int loose)
    {
        stress += loose;
        float aux = (4826.0f*stress)/10.0f;
        hudStress.GetComponent<RectTransform>().sizeDelta = new Vector2((aux > 4826) ? 4826 : aux, hudStress.GetComponent<RectTransform>().sizeDelta.y);
        if (stress >= 10)
        {
            panel.SetActive(true);
            GameObject.FindWithTag("Scissors").SetActive(false);
            GameObject.Find("FinalText").GetComponent<UnityEngine.UI.Text>().text = "Maximum stress achieved.\n YOU LOOSE";
            Cursor.visible = true;
        }
    }

    public int GetStress()
    {
        return stress;
    }
}
