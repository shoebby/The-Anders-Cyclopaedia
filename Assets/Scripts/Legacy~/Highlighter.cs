using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Highlighter : MonoBehaviour
{
    private PlayerMovement _playerMovement;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _playerMovement = GetComponentInParent<PlayerMovement>();
    }

    void Start()
    {
        _spriteRenderer.enabled = false;
    }

    void Update()
    {
        if (_playerMovement.canInteract)
        {
            _spriteRenderer.enabled = true;
        } else if (!_playerMovement.canInteract) {
            _spriteRenderer.enabled = false;
            transform.localPosition = Vector3.zero;
        }
    }
}
