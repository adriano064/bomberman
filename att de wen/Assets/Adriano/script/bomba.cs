
using System.Collections;
using UnityEngine;

public class bomba : MonoBehaviour
{
    [Header("Bomba")]
    public KeyCode inputKey = KeyCode.Space;
    public GameObject prefabBomba;
    public float tempoFusivelBomba = 2f;
    public int quantidadeBombas = 1;
    private int bombasRestantes;
    
    private void OnEnable()
    {
        bombasRestantes = quantidadeBombas;
    }
    private void Update()
    {
        if (bombasRestantes > 0 && Input.GetKeyDown(inputKey))
        {
            StartCoroutine(ColocarBomba());
        }
    }
    
    private IEnumerator ColocarBomba()
    {
        Vector2 position = transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        GameObject bomba = Instantiate(prefabBomba, position, Quaternion.identity);
        bombasRestantes--;

        yield return new WaitForSeconds(tempoFusivelBomba);
        
        Destroy(bomba);
        bombasRestantes++;  
    }

    ///chutar a bola
   private void OnTriggerExit2D(Collider2D other)
    { 
        if (other.gameObject.layer == LayerMask.NameToLayer("Bomba"))
        {
            other.isTrigger = false;
        }
    }
}
