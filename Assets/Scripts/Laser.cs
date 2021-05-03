using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private float speed = 8.0f;
    private bool _isparentNotNull;


    // Update is called once per frame
    private void Start()
    {
        _isparentNotNull = transform.parent != null;
    }

    private void Update()
    {
        transform.Translate(Vector3.right * (speed * Time.deltaTime));

        if (transform.position.x >= 9.4f)
        {
            if (_isparentNotNull) Destroy(transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
    
}