using UnityEngine;

public partial class DoorMoving : MonoBehaviour
{
    private Animator _animator;
    private bool _isPlayerNearby = false;

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Detect when player enters the trigger zone
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            _isPlayerNearby = true;
        }
    }

    // Detect when player leaves
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
        }
    }

    void Update()
    {
        if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("Open");
        }
    }
}