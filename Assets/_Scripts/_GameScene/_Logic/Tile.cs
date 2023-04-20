using UnityEngine;
using UnityEngine.Tilemaps;

namespace _Scripts._GameScene._Logic
{
    public class Tile : MonoBehaviour
    {
        /*private RectTransform MyTilePosition;
        [SerializeField] private Vector2 _myCoordinate;
        [SerializeField] private Vector2 _mySize; 


        public void init()
        {
        
        }
        
        

        public void GetMyCoordinate(Vector2 coordinate)
        {
            _myCoordinate = coordinate;
        }
    

        
    }*/
        public Tilemap tilemap;
        public TileBase tile;

        void Start()
        {
            // Tilemap koordinatları
            Vector3Int tilePosition = new Vector3Int(2, 2, 0);

            // Tilemap koordinatına kırmızı bir çizgi çiz
            DrawLine(tilePosition, Color.red);
        }

        void DrawLine(Vector3Int position, Color color)
        {
            // Tilemap koordinatına tek bir tile atayın
            tilemap.SetTile(position, tile);

            // Tilemap koordinatına yeni bir tile'ın eklenmesi için gerekli matris boyutu
            Vector3Int[] positions = new Vector3Int[] { position };

            // Tilemap koordinatına bir çizgi çizmek için gerekli matris
            TileBase[] tiles = new TileBase[] { tile };

            // Tilemap koordinatına belirtilen renkte bir çizgi çizin
            tilemap.SetTiles(positions, tiles);
            tilemap.SetColor(position, color);
        }
    }
}
    