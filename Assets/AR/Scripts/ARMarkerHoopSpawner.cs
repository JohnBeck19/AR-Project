using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARMarkerHoopSpawner : MonoBehaviour
{
    [SerializeField] private ARTrackedImageManager trackedImageManager;
    [SerializeField] private GameObject hoopPrefab; // Assign your Cube prefab here

    private Dictionary<string, GameObject> spawnedHoops = new Dictionary<string, GameObject>();

    private void Start()
    {
        // Debug logging to check if script is running
        Debug.Log("ARMarkerHoopSpawner: Script started");
        
        if (trackedImageManager == null)
        {
            Debug.LogError("ARMarkerHoopSpawner: trackedImageManager is null!");
        }
        else
        {
            Debug.Log("ARMarkerHoopSpawner: trackedImageManager found");
        }
        
        if (hoopPrefab == null)
        {
            Debug.LogError("ARMarkerHoopSpawner: hoopPrefab is null!");
        }
        else
        {
            Debug.Log("ARMarkerHoopSpawner: hoopPrefab found - " + hoopPrefab.name);
        }
    }

    private void OnEnable()
    {
        Debug.Log("ARMarkerHoopSpawner: OnEnable called");
        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
            Debug.Log("ARMarkerHoopSpawner: Subscribed to trackedImagesChanged event");
        }
    }

    private void OnDisable()
    {
        if (trackedImageManager != null)
        {
            trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
            Debug.Log("ARMarkerHoopSpawner: Unsubscribed from trackedImagesChanged event");
        }
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        Debug.Log($"ARMarkerHoopSpawner: OnTrackedImagesChanged - Added: {eventArgs.added.Count}, Updated: {eventArgs.updated.Count}, Removed: {eventArgs.removed.Count}");
        
        // For newly detected images
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log($"ARMarkerHoopSpawner: New image detected - {trackedImage.referenceImage.name}");
            SpawnHoop(trackedImage);
        }

        // For updated images (e.g., position/rotation changes)
        foreach (var trackedImage in eventArgs.updated)
        {
            Debug.Log($"ARMarkerHoopSpawner: Image updated - {trackedImage.referenceImage.name}, Tracking State: {trackedImage.trackingState}");
            UpdateHoop(trackedImage);
        }

        // For removed images
        foreach (var trackedImage in eventArgs.removed)
        {
            Debug.Log($"ARMarkerHoopSpawner: Image removed - {trackedImage.referenceImage.name}");
            RemoveHoop(trackedImage);
        }
    }

    private void SpawnHoop(ARTrackedImage trackedImage)
    {
        Debug.Log($"ARMarkerHoopSpawner: Attempting to spawn hoop for {trackedImage.referenceImage.name}");
        
        if (!spawnedHoops.ContainsKey(trackedImage.referenceImage.name))
        {
            if (hoopPrefab != null)
            {
                GameObject hoop = Instantiate(hoopPrefab, trackedImage.transform.position, trackedImage.transform.rotation);
                hoop.transform.parent = trackedImage.transform; // Parent to marker for automatic movement
                spawnedHoops[trackedImage.referenceImage.name] = hoop;
                Debug.Log($"ARMarkerHoopSpawner: Successfully spawned hoop at position {trackedImage.transform.position}");
            }
            else
            {
                Debug.LogError("ARMarkerHoopSpawner: Cannot spawn hoop - hoopPrefab is null!");
            }
        }
        else
        {
            Debug.Log($"ARMarkerHoopSpawner: Hoop already exists for {trackedImage.referenceImage.name}");
        }
    }

    private void UpdateHoop(ARTrackedImage trackedImage)
    {
        if (spawnedHoops.TryGetValue(trackedImage.referenceImage.name, out GameObject hoop))
        {
            hoop.SetActive(trackedImage.trackingState == TrackingState.Tracking);
            hoop.transform.position = trackedImage.transform.position;
            hoop.transform.rotation = trackedImage.transform.rotation;
            Debug.Log($"ARMarkerHoopSpawner: Updated hoop for {trackedImage.referenceImage.name}, Active: {hoop.activeSelf}");
        }
    }

    private void RemoveHoop(ARTrackedImage trackedImage)
    {
        if (spawnedHoops.TryGetValue(trackedImage.referenceImage.name, out GameObject hoop))
        {
            Destroy(hoop);
            spawnedHoops.Remove(trackedImage.referenceImage.name);
            Debug.Log($"ARMarkerHoopSpawner: Removed hoop for {trackedImage.referenceImage.name}");
        }
    }
}
