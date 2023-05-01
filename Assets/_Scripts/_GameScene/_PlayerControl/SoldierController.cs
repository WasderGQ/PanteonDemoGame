using System.Collections;
using System.Collections.Generic;
using _Scripts._GameScene._GameArea;
using Third_Party_Packages.Helpers.WasderGQ.PathFinding;
using UnityEngine;
using UnityEngine.Events;

namespace _Scripts._GameScene._PlayerControl
{
  public class SoldierController : MonoBehaviour
  {
   /*   private GameSpace _gameSpace;
    public UnityEvent EventStartMovement
    {
        get => _eventStartMovement;
    }

    private UnityEvent _eventStartMovement;

    [Header("PathFinding")]
    public Vector2Int gridSize;
    public float cellSize;
    public MovementType movementType;

    private PathFinding pathFinding;
    private List<Vector2Int> blockedNodes;
    private List<Vector2> path;

    [Header("Player Settings")]
    public float speed;
    private Coroutine moveRoutine;

    [Header("Debug")]
    public bool showDetails;

    public void InIt()
    {
        //_eventStartMovement = new UnityEvent();
        //_eventStartMovement.AddListener(StartMovement);
        pathFinding = new PathFinding(gridSize.x, gridSize.y, cellSize, Vector3.zero); //Initiate pathfinding
    }

    public void StartMovement(Vector2Int movingCell) 
    {
		if(Input.GetKeyDown(KeyCode.Mouse0))//Left Click
        {
            Vector2 mousePos2D = new Vector2(movingCell.x, movingCell.y);
            Debug.Log(mousePos2D);
            if(moveRoutine != null)
                StopCoroutine(moveRoutine);
            moveRoutine = StartCoroutine(Movement(mousePos2D));
        }
	}

	private List<Vector2> GetPath(Vector2 targetPosition)
    {
        pathFinding = new PathFinding(gridSize.x, gridSize.y, cellSize, Vector3.zero); //Reset for every re use.
        MarkBlockedNodes();

        //Assign Blocked Coordinates in Pathfinding Grid
        if (blockedNodes.Count > 0)
        {
            for (int i = 0; i < blockedNodes.Count; i++)
            {
                pathFinding.GetGrid().GetGridObject(blockedNodes[i].x, blockedNodes[i].y).nodeType = NodeType.Blocked;
            }
        }

        //Find Path
        List<Vector2> path = pathFinding.FindPath(transform.position, targetPosition, movementType);
        return path;
    }
    private IEnumerator Movement(Vector2 targetPosition)//movement code
    {
        path = GetPath(targetPosition);//Get path

        int currentPathIndex = 0;
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);

        if(path != null)
        {
            while (playerPosition != path[path.Count - 1])
            {
                //go from node to node until reaching to the target
                if (Vector2.Distance(transform.position, path[currentPathIndex]) > 0.1f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, path[currentPathIndex], Time.deltaTime * speed);
                }
                else
                {
                    currentPathIndex++;
                    if (currentPathIndex >= path.Count)
                    {
                        break;
                    }
                }
                yield return null;
            }
        }
        yield return null;
    }

    //Debug
    private void OnDrawGizmos()
    {
        if(Application.isPlaying && showDetails && pathFinding != null)
        {
            // Draw a grid
            Gizmos.color = Color.black;
            for (int x = 0; x < pathFinding.GetGrid().GetWidth(); x++)
            {
                for (int y = 0; y < pathFinding.GetGrid().GetHeight(); y++)
                {
                    if(pathFinding.GetGrid().GetGridObject(x,y).nodeType == NodeType.Blocked)
                        Gizmos.DrawCube(pathFinding.GetGrid().GetWorldPosition(x, y), new Vector3(cellSize, cellSize, cellSize));
                    else
                        Gizmos.DrawWireCube(pathFinding.GetGrid().GetWorldPosition(x, y), new Vector3(cellSize, cellSize, cellSize));
                }
            }

            //Draw path
            if (path != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < path.Count; i++)
                {
                    Gizmos.DrawSphere(path[i], cellSize / 6);
                    if(i != path.Count - 1)
                        Gizmos.DrawLine(path[i], path[i+1]);
                }
            }

        }

        
    }
    
    
    private void MarkBlockedNodes()
    {
        blockedNodes.Clear();
        foreach (var building in _gameSpace.RealProductList )
        {
            foreach (var product in building.ProductList )
            {
                for (int i = product.StartPositionByCell.x; i <= product.EndPositionByCell.x; i++)
                {
                    for (int j = product.StartPositionByCell.y; j <= product.EndPositionByCell.y; j++)
                    {
                        blockedNodes.Add(new Vector2Int(i,j));
                    }
                }
            }
            
            for (int i = building.StartPositionByCell.x; i <= building.EndPositionByCell.x; i++)
            {
                for (int j = building.StartPositionByCell.y; j <= building.EndPositionByCell.y; j++)
                {
                    blockedNodes.Add(new Vector2Int(i,j));
                }
            }
        }

        
        
            
        
    }
   */
}  
}

