using System;
using System.Collections.Generic;
using UnityEngine;

namespace Third_Party_Packages.Helpers.WasderGQ.PathFinding
{
    public class PathFinding
    {
        private const int MOVE_STRAIGTH_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        private Grid<Node> grid;

        public MovementType workMethod;

        public PathFinding(int width, int height, float cellSize, Vector3 originPosition)
        {
            grid = new Grid<Node>(width, height, cellSize, originPosition, (int x, int y) => new Node(x, y));
        }
        public Grid<Node> GetGrid()
        {
            return grid;
        }
        public List<Vector2> FindPath(Vector3 startWorldPosition, Vector2 endWorldPosition, MovementType workMethod = MovementType.Standart)
        {
            int startX;
            int startY;
            int endX;
            int endY;
            grid.GetXY(startWorldPosition, out startX, out startY);
            grid.GetXY(endWorldPosition, out endX, out endY);

            this.workMethod = workMethod;

            Node[] path = FindPath(startX, startY, endX, endY);
            if (path == null)
                return null;
            else
            {
                List<Vector2> vectorPath = new List<Vector2>();
                for (int i = 0; i < path.Length; i++)
                {
                    vectorPath.Add(grid.GetWorldPosition(path[i].x, path[i].y) * grid.GetCellSize()/* + Vector2.one * grid.GetCellSize() * 0.5f*/);//adjust this to pivot
                }
                return vectorPath;
            }
        }
        private Node[] FindPath(int startX, int startY, int endX, int endY)
        {
            ResetNodesParents();

            Node startNode = GetNode(startX, startY);
            if(startNode == null)
            {
                Debug.LogError("Player is invalid Position. Game is broken.");
                return null;
            }
            Node endNode = GetNode(endX, endY);
            if(endNode == null)
            {
                Debug.Log("EndNode is null. Possibly, target is out of the grid.");
                return null;
            }   

            Heap<Node> openSet = new Heap<Node>(grid.GetWidth() * grid.GetHeight());
            HashSet<Node> closedSet = new HashSet<Node>();
            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet.RemoveFirst();
                closedSet.Add(currentNode);

                if (currentNode == endNode)
                {
                    Node[] wayPointsNodes = RetracePath(startNode, endNode);
                    return wayPointsNodes;
                }

                foreach (Node neighbour in GetNeighbourList(currentNode, workMethod))
                {
                    //Debug.Log("neighbour: " + grid.GetWorldPosition(neighbour.x, neighbour.y));
                    if (closedSet.Contains(neighbour) || (neighbour.nodeType != NodeType.Empty))
                    {
                        continue;
                    }

                    int tentativeGCost = currentNode.gCost + CalculateDistanceCost(currentNode, neighbour);
                    if ((tentativeGCost < neighbour.gCost || !openSet.Contains(neighbour)))
                    {
                        neighbour.gCost = tentativeGCost;
                        neighbour.hCost = CalculateDistanceCost(neighbour, endNode);
                        neighbour.parentNode = currentNode;

                        if (!openSet.Contains(neighbour))
                            openSet.Add(neighbour);
                        else
                            openSet.UpdateItem(neighbour);
                    }
                }
            }
            //Out of nodes on openList
            return null;
        }
        private Node GetNode(int x, int y)
        {
            return grid.GetGridObject(x, y);
        }
        private void ResetNodesParents()
        {
            for (int x = 0; x < grid.GetWidth(); x++)
                for (int y = 0; y < grid.GetHeight(); y++)
                    GetNode(x, y).parentNode = null;
        }
        private Node[] RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            path.Add(endNode);
            Node currentNode = endNode;
            while (currentNode.parentNode != null)
            {
                path.Add(currentNode.parentNode);
                currentNode = currentNode.parentNode;
            }
            //you can open code below if you don't want first node in path
            //path.RemoveAt(path.Count - 1);
            Node[] waypoints = path.ToArray();
            Array.Reverse(waypoints);
            return waypoints;
        }
        private List<Vector2> SimplifyPath(List<Vector2> path)
        {
            List<Vector2> waypoints = new List<Vector2>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Count; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].x - path[i].x, path[i - 1].y - path[i].y);
                if (directionNew != directionOld)
                {
                    waypoints.Add(new Vector2(path[i].x, path[i].y));
                }
                directionOld = directionNew;
            }
            return waypoints;
        }
        private Vector2[] SimplifyPath(Vector2[] path)
        {
            List<Vector2> waypoints = new List<Vector2>();
            Vector2 directionOld = Vector2.zero;

            for (int i = 1; i < path.Length; i++)
            {
                Vector2 directionNew = new Vector2(path[i - 1].x - path[i].x, path[i - 1].y - path[i].y);
                if (directionNew != directionOld)
                {
                    waypoints.Add(new Vector2(path[i].x, path[i].y));
                }
                directionOld = directionNew;
            }
            return waypoints.ToArray();
        }
        private List<Node> GetNeighbourList(Node currentNode, MovementType neighbourType)//critical for finding path, You can manupilate this for platformer pathfinfing
        {
            List<Node> neighbourList = new List<Node>();

            switch (neighbourType)
            {
                case MovementType.Standart:
                    if (currentNode.x - 1 >= 0)
                    {
                        //Left
                        neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
                        //Left Down
                        if (currentNode.y - 1 >= 0)
                            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y - 1));
                        //Left Up
                        if (currentNode.y + 1 < grid.GetHeight())
                            neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y + 1));
                    }
                    if (currentNode.x + 1 < grid.GetWidth())
                    {
                        //Right
                        neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
                        //Right Down
                        if (currentNode.y - 1 >= 0)
                            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y - 1));
                        //Right Up
                        if (currentNode.y + 1 < grid.GetHeight())
                            neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y + 1));
                    }
                    //Down
                    if (currentNode.y - 1 >= 0)
                        neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
                    //Up
                    if (currentNode.y + 1 < grid.GetHeight())
                        neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
                    break;
                case MovementType.JustStraigth:
                    //Left
                    if (currentNode.x - 1 >= 0)
                        neighbourList.Add(GetNode(currentNode.x - 1, currentNode.y));
                    //Right
                    if (currentNode.x + 1 < grid.GetWidth())
                        neighbourList.Add(GetNode(currentNode.x + 1, currentNode.y));
                    //Down
                    if (currentNode.y - 1 >= 0)
                        neighbourList.Add(GetNode(currentNode.x, currentNode.y - 1));
                    //Up
                    if (currentNode.y + 1 < grid.GetHeight())
                        neighbourList.Add(GetNode(currentNode.x, currentNode.y + 1));
                    break;
            }
            
            return neighbourList;
        }
        private int CalculateDistanceCost(Node a, Node b)
        {
            int xDistance = Mathf.Abs(a.x - b.x);
            int yDistance = Mathf.Abs(a.y - b.y);
            int remaining = Mathf.Abs(xDistance - yDistance);
            return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGTH_COST * remaining;
        }
    }
    public enum MovementType
    {
        Standart = 0,
        JustStraigth
    }
}
