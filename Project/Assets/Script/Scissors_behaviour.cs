using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scissors_behaviour : MonoBehaviour
{
    public Texture2D texture;
    private SpriteRenderer spriteRenderer;
    private Sprite[] sprites;
    private AudioSource audioData;

    // Start is called before the first frame update

    void Start()
    {
        sprites = Resources.LoadAll<Sprite>(texture.name);
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioData = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Cursor.visible = false;
        var scissors = GameObject.FindWithTag("Scissors");
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        scissors.transform.position = new Vector3(mousePosition.x-2.5f, mousePosition.y, scissors.transform.position.z);

        if (Input.GetMouseButtonDown(0))
        {
            audioData.Play(0);
            spriteRenderer.sprite = sprites[1];
        }

        if (Input.GetMouseButtonUp(0))
        {
            spriteRenderer.sprite = sprites[0];
        }
    }
}