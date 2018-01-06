using UnityEngine;
using UnityEngine.Tilemaps;

namespace MrNibblesML
{
    //public class PlayerInPipeController : MonoBehaviour
    //{
    //    private static Vector3Int North = new Vector3Int(0,-1,0);
    //    private static Vector3Int South = new Vector3Int(0,1,0);
    //    private static Vector3Int East = new Vector3Int(1,0,0);
    //    private static Vector3Int West = new Vector3Int(-1,0,0);
    //    private static Vector3Int[] Directions = {North, South, East, West};

    //    public float speed = 10f;
    //    public float cellScale = 3f;
            
    //    private Vector3 _targetPos;
    //    private Tilemap _tilemap;
    //    private Vector3Int _cell;
    //    private Vector3Int _lastCell;
    //    private bool _isExiting;

    //    public void Travel(Vector3Int fromCell, Tilemap tilemap)
    //    {
    //        _tilemap = tilemap;
    //        _cell = _lastCell = fromCell;
    //        _isExiting = false;
    //        _targetPos = GetCellCentre(fromCell);
    //        GetComponent<Animator>().SetBool("inPipe", true);
    //        GetComponent<PlayerPlatformerController>().enabled = false;
    //        enabled = true;
    //    }

    //    void Update()
    //    {
    //        var delta = transform.position - _targetPos;
    //        var norm = delta.normalized;
    //        var travel = norm * -speed * Time.deltaTime;

    //        if (delta.sqrMagnitude <= travel.sqrMagnitude)
    //        {
    //            transform.position = _targetPos;

    //            if (_isExiting)
    //                Finish();
    //            else
    //                FindNextTarget();
    //        }
    //        else
    //        {
    //            transform.position += travel;
    //        }
    //    }

    //    private void Finish()
    //    {
    //        GetComponent<Animator>().SetBool("inPipe", false);
    //        GetComponent<PlayerPlatformerController>().enabled = true;
    //        transform.localRotation = transform.rotation = Quaternion.identity;
    //        enabled = false;
    //    }

    //    private void FindNextTarget()
    //    {
    //        // Look for a cell to move to
    //        foreach (var direction in Directions)
    //        {
    //            if (HasCellAndIsntLastCell(direction))
    //            {
    //                TargetNextCell(direction);
    //                return;
    //            }
    //        }

    //        // If we get here then we have reached the end of the pipe
    //        TransitionOutOfPipe();
    //    }

    //    private void TransitionOutOfPipe()
    //    {
    //        var delta = _cell - _lastCell;
    //        var norm = new Vector3(delta.x, delta.y, 0);
    //        var cellEdge = norm * ((_tilemap.cellSize.x / 2) * cellScale * 1.1f);
    //        _targetPos = transform.position + cellEdge;
    //        _isExiting = true;
    //    }

    //    private void TargetNextCell(Vector3Int offsetFromCurrent)
    //    {
    //        _lastCell = _cell;
    //        _cell = _cell + offsetFromCurrent;
    //        _targetPos = GetCellCentre(_cell);
    //    }

    //    private bool HasCellAndIsntLastCell(Vector3Int offsetFromCurrent)
    //    {
    //        var newCell = _cell + offsetFromCurrent;
    //        if (newCell == _lastCell)
    //            return false;

    //        var has = _tilemap.HasTile(newCell);
    //        return has;
    //    }

    //    private Vector3 GetCellCentre(Vector3Int cell)
    //    {
    //        return _tilemap.CellToWorld(cell) +
    //               new Vector3((_tilemap.cellSize.x / 2) * cellScale,
    //                   (_tilemap.cellSize.y / 2) * cellScale, 0);
    //    }
    //}
}