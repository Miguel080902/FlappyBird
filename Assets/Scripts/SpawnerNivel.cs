using System.Collections;
using UnityEngine;

public class SpawnerNivel : MonoBehaviour
{
    public GameObject nivel;
    public float resetNivel = 2f;
    void Start()
    {
        StartCoroutine(SpawnNivel());
    }

    IEnumerator SpawnNivel()
    {
        float randomY = Random.Range(-2.5f, 2.5f);
        Vector3 posicionSpawn = new Vector3(transform.position.x, randomY, transform.position.z);
        if(GameManager.instance.enJuego)
        Instantiate(nivel, posicionSpawn, Quaternion.identity);
        yield return new WaitForSeconds(resetNivel);
        StartCoroutine(SpawnNivel());
    }
}
