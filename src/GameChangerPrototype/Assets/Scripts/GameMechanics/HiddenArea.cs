using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiddenArea : MonoBehaviour
{
    private SpriteRenderer spriteRen;
    private Color baseColour;
    public float lowerOpacity;

    public float changeSpeed;
    private float currentOpacity;
    private float targetOpacity;

    void Start() {
        spriteRen = GetComponent<SpriteRenderer>();
        baseColour = spriteRen.color;
        currentOpacity = 255;
        targetOpacity = 255;
    }

    void Update() {
        float diff = targetOpacity - currentOpacity;
        currentOpacity += diff * changeSpeed * Time.deltaTime;
        baseColour.a = currentOpacity / 255;
        spriteRen.color = baseColour;
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            targetOpacity = lowerOpacity;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Player") {
            targetOpacity = 255;
        }
    }
}
