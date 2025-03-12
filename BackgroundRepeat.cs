using UnityEngine;

public class BackgroundRepeat : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _lowerBounds = 0f;

    private Vector3 _startPosition;

    // Start is called before the first frame update
    void Start()
    {
        _startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * -_speed * Time.deltaTime);

        if (transform.position.y < _lowerBounds)
        {
            transform.position = _startPosition;
        }
    }
}
