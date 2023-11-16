using System.Collections;
using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(UILineRenderer))]
public class WeaponEvolutionLine : MonoBehaviour {

    [SerializeField]
    UILineRenderer lineRenderer;

    public void Draw(Vector2 previousWeaponCoordinates)
    {
        Vector2[] points;

        if(previousWeaponCoordinates.y == 0) {
            points = new Vector2[] { 
                Vector2.zero,
                previousWeaponCoordinates
            };
        }
        else {
            points = new Vector2[] {
                Vector2.zero,
                new Vector2(previousWeaponCoordinates.x + 20, 0),
                new Vector2(previousWeaponCoordinates.x + 20, previousWeaponCoordinates.y),
                previousWeaponCoordinates
            };
        }


        lineRenderer.Points = points;
    }

    public void Reset()
    {
        lineRenderer.Points = null;
    }

}
