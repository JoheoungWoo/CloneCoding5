using UnityEngine;

public class DeadLine : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObj;

    private void Update()
    {
        if (!GameManager.Instance.IsGameStart)
            return;

        if (playerObj == null)
            GameManager.Instance.GameOver();

    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            playerObj = other.gameObject;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerObj = null;
    }
}

