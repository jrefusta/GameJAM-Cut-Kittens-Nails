using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nail : MonoBehaviour
{ 
    public Texture2D texture;
    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    private Component[] colliders;
    private bool isCutted;
    private AudioSource audioData;
    // Start is called before the first frame update
    void Start()
    {
        sprites = Resources.LoadAll<Sprite>(texture.name);
        colliders = GetComponents<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        isCutted = false;
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCutted && Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);
            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                for (int i = 0; i < colliders.Length; ++i)
                {
                    if (colliders[i] == hit.collider && sprites.Length > (i + 1))
                    {
                        spriteRenderer.sprite = sprites[i+1];
                        isCutted = true;
                        if (i == 0 || i == 4)
                        {
                            audioData.Play(0);
                            transform.parent.GetComponent<Paws_behaviour>().OnCutInDangerZone(3);
                        }
                        else if (i == 1 || i == 3)
                        {
                            audioData.Play(0);
                            transform.parent.GetComponent<Paws_behaviour>().OnCutInDangerZone(2);
                        }
                        transform.parent.GetComponent<Paws_behaviour>().OnNailCutted();
                    }
                }
            }
        }
    }
}
