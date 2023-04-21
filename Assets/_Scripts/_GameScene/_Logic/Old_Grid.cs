using UnityEngine;
using WasderGQ.Utils;

namespace _Scripts._GameScene._Logic
{
   public class Old_Grid
   {

      private int _width;
      private int _height;
      private int[,] _gridArray;
      private float _cellSize;
   
      public Old_Grid(int width, int height,float cellSize)
      {
         this._width = width;
         this._height = height;
         this._cellSize = cellSize;
         _gridArray = new int[_width, _height];

         for (int x = 0; x < _gridArray.GetLength(0); x++)
         {
            for (int y = 0; y < _gridArray.GetLength(1); y++)
            {
               UtilsClass.CreateWorldText(_gridArray[x, y].ToString(), null, GetWorldPosition(x, y),20, Color.white,
                  TextAnchor.MiddleCenter);
            }
         }

      
      
      
      
      
      }

      private Vector3 GetWorldPosition(int x ,int y)
      {
         return new Vector3(x, y) * _cellSize;


      }
   }
}
