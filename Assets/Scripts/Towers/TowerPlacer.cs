using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class TowerPlacer : MonoBehaviour
{
    public GameObject G1;
    public GameObject G2;
    public GameObject G3;

    private void OnMouseDown()
    {
        Instantiate(PlayingManager.inst.TowerPanel, PlayingManager.inst.CanvasParent.transform);
        PlayingManager.inst.placer = this.gameObject;
    }
}
