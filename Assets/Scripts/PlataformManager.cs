using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlataformManager : MonoBehaviour
{
    List<GameObject> plataformas; 
    float distanciaEntrePlataformas = 10f;
    float ultimaPlataformaZ; 

    // Start is called before the first frame update
    void Start()
    {
        plataformas = new List<GameObject>();
        // busca todas las plataformas y encuentra la Z más alta
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Translator"))
        {
        plataformas.Add(obj);
        if (obj.transform.position.z > ultimaPlataformaZ)
            ultimaPlataformaZ = obj.transform.position.z;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReciclarPlataforma(GameObject plataforma)
    {
        StartCoroutine(MoverDespues(plataforma));
    }

    IEnumerator MoverDespues(GameObject plataforma)
    {
        yield return new WaitForSeconds(3f);

        Debug.Log("Reciclando plataforma: " + plataforma.name);

        float nuevaZ = ultimaPlataformaZ + distanciaEntrePlataformas;
        plataforma.transform.position = new Vector3(plataforma.transform.position.x,
        plataforma.transform.position.y, nuevaZ);
        ultimaPlataformaZ += distanciaEntrePlataformas; 
    }
}
