using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    [Header("Shake Settings")]
    [SerializeField] float shakeIntensity = 0.1f; // How strong the shake is
    [SerializeField] float shakeDuration = 0.5f; // How long the shake lasts
    [SerializeField] float shakeFrequency = 0.05f; // How fast the shake occurs
    
    [Header("Auto Shake")]
    [SerializeField] bool autoShake = false; // Enable to shake automatically
    [SerializeField] float autoShakeInterval = 3f; // Time between auto shakes
    
    private Vector3 originalPosition;
    private bool isShaking = false;
    
    void Start()
    {
        // Store the original camera position
        originalPosition = transform.localPosition;
        
        // Start auto shake if enabled
        if (autoShake)
        {
            StartCoroutine(AutoShake());
        }
    }
    
    // Call this method to trigger a shake
    public void Shake()
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine());
        }
    }
    
    // Call this method to trigger a shake with custom parameters
    public void Shake(float intensity, float duration)
    {
        if (!isShaking)
        {
            StartCoroutine(ShakeCoroutine(intensity, duration));
        }
    }
    
    // Auto shake coroutine
    IEnumerator AutoShake()
    {
        while (autoShake)
        {
            yield return new WaitForSeconds(autoShakeInterval);
            Shake();
        }
    }
    
    // Main shake coroutine
    IEnumerator ShakeCoroutine()
    {
        return ShakeCoroutine(shakeIntensity, shakeDuration);
    }
    
    // Shake coroutine with custom parameters
    IEnumerator ShakeCoroutine(float intensity, float duration)
    {
        isShaking = true;
        float elapsed = 0f;
        
        while (elapsed < duration)
        {
            // Calculate shake offset
            float x = Random.Range(-1f, 1f) * intensity;
            float y = Random.Range(-1f, 1f) * intensity;
            float z = Random.Range(-1f, 1f) * intensity;
            
            // Apply shake to camera position
            transform.localPosition = originalPosition + new Vector3(x, y, z);
            
            elapsed += shakeFrequency;
            yield return new WaitForSeconds(shakeFrequency);
        }
        
        // Reset to original position
        transform.localPosition = originalPosition;
        isShaking = false;
    }
    
    // Method to stop auto shake
    public void StopAutoShake()
    {
        autoShake = false;
    }
    
    // Method to start auto shake
    public void StartAutoShake()
    {
        autoShake = true;
        StartCoroutine(AutoShake());
    }
}