using UnityEngine;

public class EnterGame : MonoBehaviour
{
    public GameManager gameManager;

    private void OnTriggerEnter(Collider other)
    {
        // Khi máy bay đâm vào vùng PanelTrigger
        if (other.CompareTag("Player") || other.transform.root.CompareTag("Player"))
        {
            if (gameManager == null)
            {
                Debug.LogError("GameManager is not assigned on the trigger.", this);
                return;
            }

            gameManager.ShowUIContent(true);
        }
    }
}