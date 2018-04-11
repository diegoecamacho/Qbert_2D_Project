using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeScript : MonoBehaviour {
   public NodeScript[] Adjacent;

    ///Component Reference
    SpriteRenderer spriteRenderer;

    /// <summary>
    /// Ordered from non selected down.
    /// </summary>
    [SerializeField] Sprite[] CubeSprites;
    int currentSprite = 0;
    private bool selected = false;
    public bool complete;

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
        }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SelectCube()
    {
        if (!complete)
        {
            currentSprite++;
            spriteRenderer.sprite = CubeSprites[currentSprite];
            complete = (currentSprite == CubeSprites.Length - 1);
            GameManager.Score += 25;
        }
    }
}
