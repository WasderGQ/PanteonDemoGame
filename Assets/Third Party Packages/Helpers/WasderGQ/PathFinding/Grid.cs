using System;
using UnityEngine;

namespace Third_Party_Packages.Helpers.WasderGQ.PathFinding
{
    public class Grid<T>
    {
        private int width, height;
        private float cellSize;
        private Vector3 originPosition;
        private T[,] gridArray;

        public Grid(int width, int height, float cellSize, Vector3 originPosition, Func<int, int, T> createGridObject)
        {
            this.width = width;
            this.height = height;
            this.cellSize = cellSize;
            this.originPosition = originPosition;

            gridArray = new T[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int y = 0; y < gridArray.GetLength(1); y++)
                {
                    gridArray[x, y] = createGridObject(x, y);
                }
            }
        }

        public int GetWidth()
        {
            return width;
        }
        public int GetHeight()
        {
            return height;
        }
        public float GetCellSize()
        {
            return cellSize;
        }
        public Vector2 GetWorldPosition(int x, int y)
        {
            return new Vector2(x, y) * cellSize + new Vector2(originPosition.x, originPosition.y);
        }
        public void GetXY(Vector3 worldPosition, out int x, out int y)
        {
            x = Mathf.RoundToInt((worldPosition - originPosition).x / cellSize);
            y = Mathf.RoundToInt((worldPosition - originPosition).y / cellSize);
        }
        public void SetGridObject(int x, int y, T value)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                gridArray[x, y] = value;
            }
        }
        public void SetGridObject(Vector3 worldPosition, T value)
        {
            int x;
            int y;
            GetXY(worldPosition, out x, out y);
            SetGridObject(x, y, value);
        }
        public T GetGridObject(int x, int y)
        {
            if (x >= 0 && y >= 0 && x < width && y < height)
            {
                return gridArray[x, y];
            }
            else
            {
                return default(T);
            }
        }
        public T GetGridObject(Vector3 worldPosition)
        {
            int x;
            int y;
            GetXY(worldPosition, out x, out y);
            return GetGridObject(x, y);
        }
    }
}
