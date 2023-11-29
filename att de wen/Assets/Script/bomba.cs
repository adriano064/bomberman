
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bomba : MonoBehaviour
{
    [Header("Bomba")]
    public KeyCode inputKey = KeyCode.Space;
    public GameObject prefabBomba;
    public float tempoFusivelBomba = 2f;
    public int quantidadeBombas = 1;
    private int bombasRestantes;
    
    [Header("Explosion")]
    public Explosion explosionPrefab;
    public float explosionDuration = 1f;
    public LayerMask explosionLayerMask;
    public int explosionRadius = 1;
    
    [Header("Destructible")]
    public Tilemap destrutivelTiles;
    public destrutivel destrutivelPrefab;

    
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


        position = bomba.transform.position;
        position.x = Mathf.Round(position.x);
        position.y = Mathf.Round(position.y);

        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(explosion.inicio);
        explosion.DestroyAfter(explosionDuration);
        Destroy(explosion.gameObject, explosionDuration);
        
        Explode(position, Vector2.up, explosionRadius);
        Explode(position, Vector2.down, explosionRadius);
        Explode(position, Vector2.left, explosionRadius);
        Explode(position, Vector2.right, explosionRadius);
        
        Destroy(bomba);
        bombasRestantes++;  
    }

    private void Explode(Vector2 position, Vector2 direction, int length)
    {
        if (length <= 0)
        {
            return;
        }

        position += direction;
        
        if (Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
        {
            ClearDestrutivel(position);
            return;
        }
        
        Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
        explosion.SetActiveRenderer(length > 1 ? explosion.meio : explosion.fim);
        explosion.SetDirection(direction);
        explosion.DestroyAfter(explosionDuration);
        
        
        Explode(position, direction, length - 1);
        
    }
    
    private void ClearDestrutivel(Vector2 position)
    {
        Vector3Int cell = destrutivelTiles.WorldToCell(position);
        TileBase tile = destrutivelTiles.GetTile(cell);

        if (tile != null)
        {
            Instantiate(destrutivelPrefab, position, Quaternion.identity);
            destrutivelTiles.SetTile(cell, null);
        }
    }

    public void AddBomb()
    {
        quantidadeBombas++;
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
