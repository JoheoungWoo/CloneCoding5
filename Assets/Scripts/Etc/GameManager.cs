using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private BoxCollider deadLineCollider;

    private string deadLineName = "DeadLine";


    private void Start()
    {
        deadLineCollider = transform.parent.FindChildByName(deadLineName).GetComponent<BoxCollider>();
        Debug.Assert(deadLineCollider != null);

        Vector3 screenSize = new Vector3(Screen.width, Screen.height, 0);
        deadLineCollider.size = screenSize;
    }

    private void OnDrawGizmosSelected()
    {
        if (!Application.isPlaying)
            return;

        Gizmos.color = Color.green;
        Gizmos.DrawCube(deadLineCollider.transform.position, deadLineCollider.size);
    }
}

