using System.Collections.Generic;
using UnityEngine;

public class PowerUpBuffMagnet : PowerUpBehaviour
{
    [SerializeField] private float speedScale = 10f;
    [SerializeField] private float AttractSpeed = 10f;
    [SerializeField] private float boxAttractionMultiplier = 4f;
    [SerializeField] [Range(0.0f, 1f)] private float finalScaleMultiplier = 0.5f;

    private List<Pickup> pickupsToAttract = new();
    private Collider[] overlapResults = new Collider[20];

    private Vector3 boxAttractionSize => Vector3.one * boxAttractionMultiplier;

    protected override void EnterPowerUp()
    {
        
    }

    protected override void UpdatePowerUp()
    {
        GatherPickupsInRange();
        ProcessAttractPickups();

    }

    private void GatherPickupsInRange()
    {
        var playerPosition = PowerUpContext.playerController.transform.position;
        int hitCount = Physics.OverlapBoxNonAlloc(playerPosition, boxAttractionSize, overlapResults);

        for (int i = 0; i < hitCount; i++)
        {
            var hit = overlapResults[i];
            if (hit.transform.TryGetComponent<Pickup>(out var pickup) &&
                !pickupsToAttract.Contains(pickup))
            {
                pickupsToAttract.Add(pickup);
            }
        }
    }

    private void ProcessAttractPickups()
    {
        for (int i = 0; i < pickupsToAttract.Count; i++)
        {
            var pickup = pickupsToAttract[i];
            if (pickup != null)
            {
                Vector3 startPos = pickup.transform.position;
                Vector3 endPos = PowerUpContext.playerController.transform.position;
                pickup.transform.position = Vector3.MoveTowards(startPos, endPos, AttractSpeed * Time.deltaTime);
                
                Vector3 startScale = pickup.transform.localScale;
                Vector3 endScale = Vector3.one * finalScaleMultiplier;
                pickup.transform.localScale = Vector3.Lerp(startScale, endScale, speedScale * Time.deltaTime);
            }

        }
    }

    protected override void EndPowerUp()
    {
        pickupsToAttract.Clear();
    }

    void OnDrawGizmos()
    {
        DrawBox();

    }

    private void DrawBox()
    {
        var playerPosition = PowerUpContext.playerController.transform.position;
        Gizmos.color = Color.green;

        Gizmos.DrawWireCube(playerPosition, boxAttractionSize);
    }

}
