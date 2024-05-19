using UnityEngine;

public class EnemyColliderManager : MonoBehaviour
{
    [System.Serializable]
    public class DirectionalCollider
    {
        public string spriteName;
        public GameObject colliderObject;
    }

    public DirectionalCollider[] directionalColliders;

    private string currentSpriteName = "";

    void Start()
    {
        DeactivateAllColliders();
    }

    public void SetDirection(string spriteName)
    {
        if (currentSpriteName == spriteName) return;

        currentSpriteName = spriteName;
        DeactivateAllColliders();
        ActivateCollider(spriteName);
    }

    private void DeactivateAllColliders()
    {
        foreach (var collider in directionalColliders)
        {
            collider.colliderObject.SetActive(false);
        }
    }

    private void ActivateCollider(string spriteName)
    {
        foreach (var collider in directionalColliders)
        {
            if (collider.spriteName == spriteName)
            {
                collider.colliderObject.SetActive(true);
                break;
            }
        }
    }
}