using UnityEngine;
using System.Collections;
public class Enemy_Combat : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Color _hitColor = Color.white;
    [SerializeField] private float _flashDuration = 0.1f;

    private Color _originalColor;

    private Coroutine _flashCoroutine;

    private void Start()
    {
        _originalColor = _spriteRenderer.color;
    }

    public void TakeDamage(int damage)
    {
        if (_flashCoroutine != null)
            StopCoroutine(_flashCoroutine);
        _flashCoroutine = StartCoroutine(HitFlash());
    }

    private IEnumerator HitFlash()
    {
        _spriteRenderer.color = _hitColor;

        yield return new WaitForSeconds(_flashDuration);

        _spriteRenderer.color = _originalColor;
    }
}
