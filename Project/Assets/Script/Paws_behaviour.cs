using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Helper;

public class Paws_behaviour : MonoBehaviour
{
    private GameObject[] nails;
    public int numNails;
    private int level; //MÁS ADELANTE SERÁ EL NIVEL DEL GM
    // Start is called before the first frame update
    public float posX, posY, angle = 0f;
    public float rotationRadius = 2f;
    private bool collision = false;
    private int speed = 6;
    private GameObject[] paws;
    private float initialPos = 5f;
    public float freq = 20f;
    public float magnitude = 0.5f;
    private bool goToleft = true;
    private Animator anim;
    void Start()
    {
        nails = Helper.GetAllChilds(this.gameObject);
        Debug.Log("Tenemos " + nails.Length + " uñas");
        numNails = nails.Length;
        level = 0;
    }

    // Update is called once per frame
    void Update()
    {
        int level = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().getIndexCat();
        GameObject cat = transform.parent.gameObject;
        paws = new GameObject[cat.transform.childCount];
        paws[0] = (cat.transform.GetChild(0).gameObject);
        paws[1] = (cat.transform.GetChild(1).gameObject);


        var totalNailsCutted = 8;
        totalNailsCutted -= paws[0].GetComponent<Paws_behaviour>().numNails;
        totalNailsCutted -= paws[1].GetComponent<Paws_behaviour>().numNails;
        for (int i = 0; i < paws.Length; ++i)
        {
            var paw = paws[i];

            Animator anim = paw.GetComponent<Animator>();
            if (totalNailsCutted < 2)
            {
                anim.SetInteger("stage", 0);
            }
            else if (totalNailsCutted < 4)
            {
                anim.SetInteger("stage", 1);
            }

            else if (totalNailsCutted < 6)
            {
                anim.SetInteger("stage", 2);
            }

            else if (totalNailsCutted < 8)
            {
                anim.SetInteger("stage", 3);
            }
        }
    }

    
    public void OnNailCutted()
    {
        --numNails;
        Debug.Log("Uñas restantes de la pata "+ transform.name + " : " + numNails);
        if (numNails == 0)
        {
             transform.parent.GetComponent<Cat>().OnAllNailsInPawCutted();
        }
    }

     public void OnCutInDangerZone(int v)
    {
        transform.parent.GetComponent<Cat>().OnCutInDangerZone(v);
    }
    public IEnumerator MoveToPosition(Transform transform, Vector3 position, float timeToMove)
    {
        var currentPos = transform.position;
        var t = 0f;
        while (t < 1)
        {
            t += Time.deltaTime / timeToMove;
            gameObject.transform.position = Vector3.Lerp(currentPos, position, t);
            yield return null;
        }
    }

}
