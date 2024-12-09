using System;
using System.Collections.Generic;

namespace Domino
{
    [Serializable]
    public class GameState
    {
        public List<Tile> Board { get; set; }
        public List<Tile> HumanPlayerTiles { get; set; }
        public List<Tile> AIPlayerTiles { get; set; }
        public bool IsPlayerTurn { get; set; }
    }

}
