using UnityEngine;
using System.IO;
using System.Collections.Generic;

public class DataLogger : MonoBehaviour
{
    public Transform player;

    private int nombreClics = 0;
    private float tempsDebut;

    private string cheminPositions;
    private string cheminObjets;

    void Start()
    {
        tempsDebut = Time.time;

        // Chemins des fichiers
        cheminPositions = Application.persistentDataPath + "/positions.csv";
        cheminObjets = Application.persistentDataPath + "/objets_cliques.csv";

        // Création des fichiers avec entêtes
        File.WriteAllText(cheminPositions, "Temps,X,Y,Z,Nombre_Clics\n");
        File.WriteAllText(cheminObjets, "Temps,Objet\n");

        Debug.Log("📁 Fichiers créés ici : " + Application.persistentDataPath);

        // Enregistrer position toutes les 2 secondes
        InvokeRepeating(nameof(EnregistrerPosition), 2f, 2f);
    }

    void Update()
    {
        // Détection clic souris
        if (Input.GetMouseButtonDown(0))
        {
            nombreClics++;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                string nomObjet = hit.transform.name;
                float temps = Time.time - tempsDebut;

                // Enregistrer objet cliqué
                string ligne = $"{temps:F1},{nomObjet}\n";
                File.AppendAllText(cheminObjets, ligne);

                // Changer couleur objet
                Renderer rend = hit.transform.GetComponent<Renderer>();
                if (rend != null)
                    rend.material.color = Random.ColorHSV();

                Debug.Log("🖱️ Clic sur : " + nomObjet);
            }
        }
    }

    void EnregistrerPosition()
    {
        float temps = Time.time - tempsDebut;
        Vector3 pos = player.position;

        string ligne = $"{temps:F1},{pos.x:F2},{pos.y:F2},{pos.z:F2},{nombreClics}\n";

        File.AppendAllText(cheminPositions, ligne);

        Debug.Log("📍 Position enregistrée : " + pos);
    }

    void OnApplicationQuit()
    {
        float tempsTotal = Time.time - tempsDebut;

        Debug.Log("⏱️ Temps total : " + tempsTotal + " sec");
        Debug.Log("🖱️ Nombre total de clics : " + nombreClics);
        Debug.Log("📁 Données sauvegardées !");
    }
}