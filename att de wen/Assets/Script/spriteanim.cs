using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spriteanim : MonoBehaviour
{
    // variavel para acessar o componente SpriteRenderer do objeto(player)
    private SpriteRenderer spriteRenderer;
    
    public Sprite idleSprite;
    public Sprite[] animationSprites;
    
    public float TempodeAnimacao = 0.25f;
    private int animationFrame;
        
    public bool loop = true;
    public bool idle = true;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    //Esess métodos vão ativa (OnEnable) e desativa (OnDisable), a renderização dos sprites(animação)
    private void OnEnable()
    {
        spriteRenderer.enabled = true;
    }

    private void OnDisable()
    {
        spriteRenderer.enabled = false;
    }
    
    // Start é chamado antes do primeiro frame de atualização. Ele inicia a repetição da chamada ao método NextFrame
    private void Start()
    {
        InvokeRepeating(nameof(NextFrame), TempodeAnimacao, TempodeAnimacao);
    }

    // avamça para o proximo quadro da animação 
    private void NextFrame()
    {
        animationFrame++;

        if (loop && animationFrame >= animationSprites.Length) {
            animationFrame = 0;
        }

        if (idle) {
            spriteRenderer.sprite = idleSprite;
        } else if (animationFrame >= 0 && animationFrame < animationSprites.Length) {
            spriteRenderer.sprite = animationSprites[animationFrame];
        }
    }
    
}
