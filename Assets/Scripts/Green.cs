using UnityEngine;

public class Green : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.SetState(GameState.End);
        }
    }
}
