using UnityEngine;

public class ClickInteraction : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100f))
            {
                Renderer renderer = hit.transform.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = Random.ColorHSV();
                }
                Debug.Log("✅ Tu as cliqué sur : " + hit.transform.name);
            }
            else
            {
                Debug.Log("❌ Rien détecté");
            }
        }
    }
}