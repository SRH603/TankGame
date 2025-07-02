using UnityEngine;

public class TankCamera : MonoBehaviour
{
    [SerializeField] GameObject tank;
    [SerializeField] float height;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = tank.transform.position + new Vector3(0, height, 0);
    }
}
