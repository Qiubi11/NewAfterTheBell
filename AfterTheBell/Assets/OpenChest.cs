using UnityEngine;

public class OpenChest : MonoBehaviour
{
    private Animator _animator;
    private bool _isPlayerNearby = false;
    private bool _isOpen = false; // Prevents re-triggering once opened

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerNearby = false;
        }
    }

    void Update()
    {
        // Only trigger if nearby, pressing E, and the chest isn't already open
        if (_isPlayerNearby && Input.GetKeyDown(KeyCode.E) && !_isOpen)
        {
            _animator.SetTrigger("OpenChest");
            _isOpen = true; 
            Debug.Log("Chest opened!");
        }
    }
}