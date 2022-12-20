using System;
using UnityEngine;
using Tile = UnityEngine.Tilemaps.Tile;

public class GridManager : MonoBehaviour
{
   [SerializeField] private  int _width;
   [SerializeField] private  int _height;
   [SerializeField] private  TileManager _tileprefab;
   [SerializeField] private  Transform _camera;

   private void Start()
   {
      GenerateGrid();
   }

   void GenerateGrid()
   {
      for (int x = 0; x < _width; x++)
      {
         for (int y = 0; y < _height; y++)
         {
            var spawnedTile = Instantiate(_tileprefab, new Vector3(x, y), Quaternion.identity);
            spawnedTile.name = $"Tile {x} {y}";
            spawnedTile.transform.parent = transform;
            var isOffset = (x % 2 == 0 && y % 2 == 0) || (x % 2 != 0 && y % 2 != 0);
            spawnedTile.Init(isOffset);
         }
      }

      _camera.transform.position = new Vector3((float) _width / 2 - 0.5f, (float) _height / 2 - 0.5f,-10);
   }
}
