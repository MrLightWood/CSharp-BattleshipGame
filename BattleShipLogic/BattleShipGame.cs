using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using DAL;
using MenuSystem;
using Domain.Enums;

namespace BattleShipLogic
{
    public class BattleShipGame
    {
        readonly Random gen = new Random();
        readonly char[] abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        private string _playerA { get; set; } = null!;
        private string _playerB { get; set; } = null!;
        private EBoatsCanTouch _boatsCanTouch { get; set; }
        private int _boardWidth {get; set; }
        private int _boardHeight { get; set; }
        private bool _nextMoveByP1 { get; set; } = true;
        private EPlayerType _playerAType { get; set; }
        private EPlayerType _playerBType { get; set; }
        private bool _gameFinished { get; set; } = false;
        private bool _gameStarted { get; set; } = false;
        private int _totalShips { get; set; }
        private ECellState[,] _board1 = null!;
        private ECellState[,] _board2 = null!;
        private Dictionary<int, string> _ships1 = new Dictionary<int, string>();
        private Dictionary<int, string> _ships2 = new Dictionary<int, string>();

        private string? _botFirstMove { get; set; }
        private string? _botLastShipHit { get; set; }
        private string? _botCurrentDirection { get; set; } = "none";
        
        public void BattleShipGameInit(string pA, string pB, EPlayerType pAType, EPlayerType pBType, int bW, int bH, EBoatsCanTouch bct)
        {
            _playerA = pA;
            _playerB = pB;
            _playerAType = pAType;
            _playerBType = pBType;
            _boardWidth = bW;
            _boardHeight = bH;
            _boatsCanTouch = bct;
            _board1 = new ECellState[bW,bH];
            _board2 = new ECellState[bW,bH];
            _gameStarted = true;
        }

        public ECellState[,] GetBoard1()
        {
            var res = new ECellState[_board1.GetLength(0),_board1.GetLength(1)];
            Array.Copy(_board1, res, _board1.Length );
            return res;
        }
        public ECellState[,] GetBoard2()
        {
            var res = new ECellState[_board2.GetLength(0),_board2.GetLength(1)];
            Array.Copy(_board2, res, _board2.Length );
            return res;
        }

        private void SetShip(int shipSize, string shipCells,  int boardNum)
        {
            switch (boardNum)
            {
                case 2:
                    if (_ships2.Count == 5)
                    {
                        Console.WriteLine("Can't add more ships. Maximum limit size is reached");
                        break;
                    }

                    _ships2.Add(shipSize, shipCells);
                    break;
                default:
                    if (_ships1.Count == 5)
                    {
                        Console.WriteLine("Can't add more ships. Maximum limit size is reached");
                        break;
                    }

                    _ships1.Add(shipSize, shipCells);
                    break;
            }
        }

        private void SetCellShip(string shipCells, int boardNum)
        {
            
            var shipSize = GetShipSize(shipCells);
            for (int i = 0; i < shipSize; i++)
            {
                if (shipCells[0] == shipCells[3])
                {
                    SetCellValue((int) char.GetNumericValue(shipCells[1])+i, Array.IndexOf(abc, shipCells[0]), boardNum, ECellState.Ship);
                }
                else
                {
                    SetCellValue((int) char.GetNumericValue(shipCells[1]), Array.IndexOf(abc, shipCells[0]) + i, boardNum, ECellState.Ship);
                }
            }
            
        }

        public (int moveX, int moveY) BotMove()
        {
            int ?moveX = null; 
            int ?moveY = null;
            //Random gen = new Random();
            int firstMoveX, firstMoveY;

            if (_botLastShipHit == "fail")
            {
                if(PossibleDirections((int) char.GetNumericValue(_botFirstMove![0]),
                    (int) char.GetNumericValue(_botFirstMove![2])) == UsedDirections((int) char.GetNumericValue(_botFirstMove![0]),
                    (int) char.GetNumericValue(_botFirstMove![2])))
                {
                    _botFirstMove = null;
                    _botLastShipHit = null;
                    _botCurrentDirection = "none";
                }
                else
                {
                    _botLastShipHit = null;
                }
            }
            
            if ((_botFirstMove == null) || (_botFirstMove != null && GetCellValue((int) char.GetNumericValue(_botFirstMove![0]),
                (int) char.GetNumericValue(_botFirstMove![2]), 1) == ECellState.Bomb))
            {
                do
                {
                    firstMoveX = gen.Next(_boardHeight);
                    firstMoveY = gen.Next(_boardHeight);
                    if (GetCellValue(firstMoveX, firstMoveY, 1) == ECellState.Empty || GetCellValue(firstMoveX, firstMoveY, 1) == ECellState.Ship)
                    {
                        _botFirstMove = $"{firstMoveX}-{firstMoveY}";
                        MakeAMove(firstMoveX, firstMoveY);
                        moveX = firstMoveX;
                        moveY = firstMoveY;
                        break;
                    }
                } while (true);
            } else if(_botFirstMove != null)
            {
                int? xx = null;
                int? yy = null;
                firstMoveX = (int) char.GetNumericValue(_botFirstMove[0]);
                firstMoveY = (int) char.GetNumericValue(_botFirstMove[2]);
                if (_botLastShipHit == null)
                {
                    _botCurrentDirection =  CheckDirection(firstMoveX, firstMoveY);
                    switch (_botCurrentDirection)
                    {
                        case "up": xx = firstMoveX; yy = firstMoveY-1; break;
                        case "down": xx = firstMoveX; yy = firstMoveY+1; break;
                        case "left": xx = firstMoveX-1; yy = firstMoveY; break;
                        case "right": xx = firstMoveX+1; yy = firstMoveY; break;
                    }
                    if (xx.HasValue && yy.HasValue)
                    {
                        MakeAMove(xx.Value, yy.Value);
                        if(GetCellValue(xx.Value, yy.Value, 1) == ECellState.Shiphit) { _botLastShipHit = $"{xx.Value}-{yy.Value}"; }
                        else if(GetCellValue(xx.Value, yy.Value, 1) == ECellState.Bomb) {_botLastShipHit = "fail";}

                        moveX = xx.Value;
                        moveY = yy.Value;
                    }
                } 
                else
                {
                    var lastShipHitX = (int) char.GetNumericValue(_botLastShipHit[0]);
                    var lastShipHitY = (int) char.GetNumericValue(_botLastShipHit[2]);
                    switch (_botCurrentDirection)
                    {
                        case "up": xx = lastShipHitX; yy = lastShipHitY-1; break;
                        case "down": xx = lastShipHitX; yy = lastShipHitY+1; break;
                        case "left": xx = lastShipHitX-1; yy = lastShipHitY; break;
                        case "right": xx = lastShipHitX+1; yy = lastShipHitY; break;
                        case "none": _botLastShipHit = "fail"; return (-1, -1);
                    }
                    if((xx > MaxVal || xx < 0) || (yy > MaxVal || yy < 0)){ _botLastShipHit = null; return(-1, -1); }

                    if (GetCellValue(xx!.Value, yy!.Value, 1) == ECellState.Bomb)
                    {
                        _botLastShipHit = "fail";
                        return (-1, -1);
                    }
                    moveX = xx;
                    moveY = yy;
                    MakeAMove(xx!.Value, yy!.Value);
                    _botLastShipHit = GetCellValue(xx.Value, yy.Value, 1) == ECellState.Shiphit ? $"{xx.Value}-{yy.Value}" : "fail";
                    
                }
            }
            
            if(moveX.HasValue && moveY.HasValue)
            {
                return (moveX.Value, moveY.Value);
            }
            return (-1, -1);
        }
        
        public int GetShipSize(string shipCells)
        {
            if (shipCells[0] == shipCells[3])
            {
                 return (int) char.GetNumericValue(shipCells[4]) - (int) char.GetNumericValue(shipCells[1]) + 1;
            }

            if (shipCells[1] == shipCells[4])
            {
                return Array.IndexOf(abc, shipCells[3]) - Array.IndexOf(abc, shipCells[0]) + 1;
            }

            throw new ArgumentException("ShipCells value is invalid");
        }

        public Dictionary<int, string> GetShip(int boardNum)
        {
            switch(boardNum)
            {
                case 2:
                    return _ships2;
                default:
                    return _ships1;
            }
        }

        public int GetLengthBoard(int d)
        {
            return _board1.GetLength(d);
        }

        public ECellState GetCellValue(int x, int y, int boardNum)
        {
            switch (boardNum)
            {
                case 1:
                    return _board1[x, y];

                case 2:
                    return _board2[x, y];
            }

            return 0;
        }

        private int UsedDirections(int x, int y)
        {
            var usedDirections = 0;
            for (var j = 0; j < 4; j++)
            {
                int? xx = null;
                int? yy = null;
                switch (j)
                {
                    case 0: if (y - 1 >= 0) { xx = x; yy = y - 1; } break;
                    case 1: if (x - 1 >= 0) { xx = x - 1; yy = y; } break;
                    case 2: if (x + 1 <= MaxVal) { xx = x + 1; yy = y; } break;
                    case 3: if (y + 1 <= MaxVal) { xx = x; yy = y + 1; } break;
                }

                if (!xx.HasValue || !yy.HasValue) continue;
                if (GetCellValue(xx.Value, yy.Value, 1) == ECellState.Bomb || GetCellValue(xx.Value, yy.Value, 1) == ECellState.Shiphit)
                {
                    usedDirections++;
                }
            }
            return usedDirections;
        }
        private int PossibleDirections(int x, int y)
        {
            var possibleDirectionsNum = 0;
            
            for (int j = 0; j < 4; j++)
            {
                switch (j)
                {
                    case 0: if (y - 1 >= 0) { possibleDirectionsNum++; } break;
                    case 1: if (x - 1 >= 0) { possibleDirectionsNum++; } break;
                    case 2: if (x + 1 <= MaxVal) { possibleDirectionsNum++; } break;
                    case 3: if (y + 1 <= MaxVal) { possibleDirectionsNum++; } break;
                }
            }

            return possibleDirectionsNum;
        }
        
        private string CheckDirection(int x, int y)
        {
            var possibleDirectionsNum = PossibleDirections(x, y);
            
            //Random gen = new Random();
            do
            {
                int? xx = null;
                int? yy = null;
                if (UsedDirections(x, y) == possibleDirectionsNum)
                {
                    break;
                }
                int? i = gen.Next(4);
                switch (i)
                {
                    case 0: if (y - 1 >= 0) { xx = x; yy = y - 1; } break;
                    case 1: if (x - 1 >= 0) { xx = x - 1; yy = y; } break;
                    case 2: if (x + 1 <= MaxVal) { xx = x + 1; yy = y; } break;
                    case 3: if (y + 1 <= MaxVal) { xx = x; yy = y + 1; } break;
                }

                if (!xx.HasValue || !yy.HasValue) continue;
                if (GetCellValue(xx.Value, yy.Value, 1) == ECellState.Empty || GetCellValue(xx.Value, yy.Value, 1) == ECellState.Ship)
                {
                    switch(i)
                    {
                        case 0: return "up"; //It means that there direction bot can use and check
                        case 1: return "left";
                        case 2: return "right";
                        case 3: return "down";
                    }
                }
            } while(true);
            
            return "none"; //It means that there is no directions
        }
        
        private bool CheckAround(int x, int y, int boardNum)
        {
            for (var i = 0; i < 8; i++)
            {
                int? xx = null;
                int? yy = null;
                switch (i)
                {
                    case 0: if (x - 1 >= 0 && y - 1 >= 0) { xx = x - 1; yy = y - 1; } break;
                    case 1: if (y - 1 >= 0) { xx = x; yy = y - 1; } break;
                    case 2: if (x - 1 >= 0) { xx = x - 1; yy = y; } break;
                    case 3: if (x + 1 <= MaxVal && y - 1 >= 0) { xx = x + 1; yy = y - 1; } break;
                    case 4: if (x - 1 >= 0 && y + 1 <= MaxVal) { xx = x - 1; yy = y + 1; } break;
                    case 5: if (x + 1 <= MaxVal) { xx = x + 1; yy = y; } break;
                    case 6: if (y + 1 <= MaxVal) { xx = x; yy = y + 1; } break;
                    case 7: if (x + 1 <= MaxVal && y + 1 <= MaxVal) { xx = x + 1; yy = y + 1; } break;
                }
                if(xx.HasValue && yy.HasValue)
                {
                    if (GetCellValue(xx.Value, yy.Value, boardNum) != ECellState.Empty)
                    {
                        return true; //It means that there is something around cell
                    }
                }
            }
            return false; //It means that there is nothing around cell
        }
        
        public void GenerateBoard(bool secondBoard = true)
        {
            for (var i = 0; i < _boardHeight/2; i++)
            {
                //Random gen = new Random();
                //bool isHorizontal = (gen.Next(100) % 2 == 0);
                int yVal2=0, xVal2=0;

                while(true)
                {
                    var startOver = false;
                    int xVal1;
                    int yVal1;
                    do
                    {
                        xVal1 = gen.Next(_boardHeight);
                        yVal1 = gen.Next(_boardHeight);
                            
                        if (secondBoard)
                        {
                            xVal2 = gen.Next(_boardHeight);
                            yVal2 = gen.Next(_boardHeight);
                        }
                    } while (xVal1+i > MaxVal || xVal2+i > MaxVal || yVal1+i > MaxVal || yVal2+i > MaxVal);
                    var isHorizontal1 = (gen.Next(101) % 2 == 0);
                    var isHorizontal2 = (gen.Next(101) % 2 == 0);

                    for (var j = 0; j < i+1; j++)
                    {
                        if (isHorizontal1)
                        {
                            if (GetCellValue(xVal1+j, yVal1, 1) != ECellState.Empty)
                            {
                                startOver = true;
                                break;
                            } else
                            {
                                if (_boatsCanTouch == EBoatsCanTouch.No &&
                                    CheckAround(xVal1 + j, yVal1, 1))
                                {
                                    startOver = true;
                                    break;    
                                }
                            }
                        }
                        else
                        {
                            if (GetCellValue(xVal1, yVal1+j, 1) != ECellState.Empty)
                            {
                                startOver = true;
                                break;
                            } else
                            {
                                if (_boatsCanTouch == EBoatsCanTouch.No &&
                                    CheckAround(xVal1, yVal1 + j, 1))
                                {
                                    startOver = true;
                                    break;    
                                }
                            }
                        }
                            
                        if(secondBoard)
                        {
                            if (isHorizontal2)
                            {
                                if (GetCellValue(xVal2+j, yVal2, 2) != ECellState.Empty)
                                {
                                    startOver = true;
                                    break;
                                } else
                                {
                                    if (_boatsCanTouch == EBoatsCanTouch.No &&
                                        CheckAround(xVal2 + j, yVal2, 2))
                                    {
                                        startOver = true;
                                        break;    
                                    }
                                }
                            }
                            else
                            {
                                if (GetCellValue(xVal2, yVal2+j, 2) != ECellState.Empty)
                                {
                                    startOver = true;
                                    break;
                                }
                                else
                                {
                                    if (_boatsCanTouch == EBoatsCanTouch.No &&
                                        CheckAround(xVal2, yVal2 + j, 2))
                                    {
                                        startOver = true;
                                        break;    
                                    }
                                }
                            }
                        }
                    }
                        
                    if (!startOver)
                    {
                        if (isHorizontal1)
                        {
                            SetShip(i + 1, $"{abc[yVal1]}{xVal1}-{abc[yVal1]}{xVal1 + i}", 1);
                            SetCellShip($"{abc[yVal1]}{xVal1}-{abc[yVal1]}{xVal1 + i}", 1);
                        }
                        else
                        {
                            SetShip(i + 1, $"{abc[yVal1]}{xVal1}-{abc[yVal1 + i]}{xVal1}", 1);
                            SetCellShip($"{abc[yVal1]}{xVal1}-{abc[yVal1 + i]}{xVal1}", 1);
                        }

                        if(secondBoard)
                        {
                            if (isHorizontal2)
                            {
                                SetShip(i + 1, $"{abc[yVal2]}{xVal2}-{abc[yVal2]}{xVal2 + i}", 2);
                                SetCellShip($"{abc[yVal2]}{xVal2}-{abc[yVal2]}{xVal2 + i}", 2);
                                break;
                            }
                            else
                            {
                                SetShip(i + 1, $"{abc[yVal2]}{xVal2}-{abc[yVal2 + i]}{xVal2}", 2);
                                SetCellShip($"{abc[yVal2]}{xVal2}-{abc[yVal2 + i]}{xVal2}", 2);
                                break;
                            }
                        }
                    }
                }
            }
        }

        
        private void SetCellValue(int x, int y, int boardNum, ECellState value)
        {
            switch (boardNum)
            {
                case 1:
                    _board1[x, y] = value;
                    break;
                case 2:
                    _board2[x, y] = value;
                    break;
            }
        }

        public void MakeAMove(int x, int y)
        {
            if (_nextMoveByP1 && (_board2[x, y] == ECellState.Empty || _board2[x, y] == ECellState.Ship))
            {
                if (_board2[x, y] == ECellState.Ship)
                {
                    _board2[x, y] = ECellState.Shiphit;
                } else if (_board2[x, y] == ECellState.Empty)
                {
                    _board2[x, y] = ECellState.Bomb;
                }
            }
            else if(!_nextMoveByP1 && (_board1[x, y] == ECellState.Empty || _board1[x, y] == ECellState.Ship))
            {
                if (_board1[x, y] == ECellState.Ship)
                {
                    _board1[x, y] = ECellState.Shiphit;
                } else if (_board1[x, y] == ECellState.Empty)
                {
                    _board1[x, y] = ECellState.Bomb;
                }
            }
            else
            {
                Menu.WriteWithColor("Can't make a move. Chosen cell has already been used", "red", true);
                return;
            }

            if (_gameFinished = CheckWinner())
            {
                Menu.WriteWithColor(NextMoveByP1 ? $"CONGRATULATIONS! Player {PlayerA} Has Won This Game" :
                    $"CONGRATULATIONS! Player {PlayerB} Has Won This Game", "green", true);
            }
            _nextMoveByP1 = !_nextMoveByP1;
        }

        public bool CheckWinner()
        {
            /*
            char[] abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            var totalShips = 0;
            if (_nextMoveByP1)
            {
                for (var i = 0; i < _ships2.Count; i++)
                {
                    for (var j = 0; j < GetShipSize(_ships2[i+1]); j++)
                    {
                        if (_ships2[i+1][0] == _ships2[i+1][3])
                        {
                            if (_board2[(int) char.GetNumericValue(_ships2[i+1][1]) + j,
                                Array.IndexOf(abc, _ships2[i+1][0])] == ECellState.Shiphit)
                            {
                                totalShips += 1;
                            }
                        }
                        else
                        {
                            if (_board2[(int) char.GetNumericValue(_ships2[i+1][1]),
                                Array.IndexOf(abc, _ships2[i+1][0]) + j] == ECellState.Shiphit)
                            {
                                totalShips += 1;
                            }
                        }
                    }
                }
                if (totalShips == 15)
                {
                    //Menu.WriteWithColor($"Player {PlayerA} has won this game", "green", true);
                    return true;
                    //_gameFinished = true;
                }
            }
            else
            {
                for (var i = 0; i < _ships1.Count; i++)
                {
                    for (var j = 0; j < GetShipSize(_ships1[i+1]); j++)
                    {
                        if (_ships1[i+1][0] == _ships1[i+1][3])
                        {
                            if (_board1[(int) Char.GetNumericValue(_ships1[i+1][1]) + j,
                                Array.IndexOf(abc, _ships1[i+1][0])] == ECellState.Shiphit)
                            {
                                totalShips += 1;
                            }
                        }
                        else
                        {
                            if (_board1[(int) Char.GetNumericValue(_ships1[i+1][1]),
                                Array.IndexOf(abc, _ships1[i+1][0]) + j] == ECellState.Shiphit)
                            {
                                totalShips += 1;
                            }
                        }
                    }
                }

                if (totalShips != 15) return false;
                //Menu.WriteWithColor($"Player {PlayerB} has won this game", "green", true);
                _gameFinished = true;
                return true;
            }
            */

            if (_totalShips == 0)
            {
                for (var y = 0; y < _boardHeight; y++)
                {
                    for (var x = 0; x < _boardWidth; x++)
                    {
                        if (_board1[x, y] == ECellState.Ship || _board1[x, y] == ECellState.Shiphit) { _totalShips++; }
                    }    
                }    
            }
            
            var totalShipsA = 0;
            var totalShipsB = 0;
            for (var y = 0; y < _boardHeight; y++)
            {
                for (var x = 0; x < _boardWidth; x++)
                {
                    if (_board1[x, y] == ECellState.Shiphit) { totalShipsA++; }
                    if (_board2[x, y] == ECellState.Shiphit) { totalShipsB++; }
                }    
            }
            return totalShipsA == _totalShips || totalShipsB == _totalShips;
        }
        
        public string GetSerializedGameState()
        {
            var state = new GameState
            {
                NextMoveByP1 = _nextMoveByP1,
                Width = _boardWidth, 
                Height = _boardHeight,
                PlayerA = _playerA,
                PlayerB = _playerB,
                PlayerAType = _playerAType,
                PlayerBType = _playerBType,
                BoatsCanTouch = _boatsCanTouch
            };
            
            state.Board1 = new ECellState[state.Width ][];
            state.Board2 = new ECellState[state.Width ][];

            state.ShipsA = new Dictionary<int, string>();
            state.ShipsB = new Dictionary<int, string>();
            
            for (var i = 0; i < _ships1.Count; i++)
            {
                state.ShipsA.Add(i+1 ,_ships1[i+1]);
                state.ShipsB.Add(i+1 ,_ships2[i+1]);
            }
            
            for (var i = 0; i < state.Board1.Length; i++)
            {
                state.Board1[i] = new ECellState[state.Height];
                state.Board2[i] = new ECellState[state.Height];
            }

            for (var x = 0; x < state.Width; x++)
            {
                for (var y = 0; y < state.Height; y++)
                {
                    state.Board1[x][y] = _board1[x, y];
                    state.Board2[x][y] = _board2[x, y];
                }
            }
            
            
            var jsonOptions = new JsonSerializerOptions()
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(state, jsonOptions);
        }
        
        
        public void SetGameStateFromJsonString(string jsonString)
        {
            var state = JsonSerializer.Deserialize<GameState>(jsonString);
            
            // restore actual state from deserialized state

            if (state == null)
            {
                Console.WriteLine("File is corrupted. Cannot load game");
            } else {
                _nextMoveByP1 = state.NextMoveByP1;
                _board1 =  new ECellState[state.Width, state.Height];
                _board2 =  new ECellState[state.Width, state.Height];
                _playerA = state.PlayerA;
                _playerB = state.PlayerB;
                _playerAType = state.PlayerAType;
                _playerBType = state.PlayerBType;
                _boatsCanTouch = state.BoatsCanTouch;
                _boardWidth = state.Width;
                _boardHeight = state.Height;
                _ships1 = new Dictionary<int, string>();
                _ships2 = new Dictionary<int, string>();
                
                
                for (var i = 0; i < state.ShipsA.Count; i++)
                {
                    _ships1.Add(i+1, state.ShipsA[i+1]);
                    _ships2.Add(i+1, state.ShipsB[i+1]);
                }
                
                for (var x = 0; x < state.Width; x++)
                {
                    for (var y = 0; y < state.Height; y++)
                    {
                        _board1[x, y] = state.Board1[x][y];
                        _board2[x, y] = state.Board2[x][y];
                    }
                }
            }
        }

        public void SetGameFromDatabase(int gameId)
        {
            var dbOptions = new DbContextOptionsBuilder<AppDbContext>().UseSqlServer(
                @"
                    Server=SAILEKEYEV; 
                    Database=csharp;
                    Trusted_Connection=True;
                    MultipleActiveResultSets=true"
            ).Options;
            
            using var db = new AppDbContext(dbOptions);
            //using var db = new AppDbContext();
            
            var dbGame = db.Games.Single(x=>x.GameId == gameId);
            var dbPlayerA = db.Players
                .Single(x => x.PlayerId == dbGame.PlayerAId);
            var dbPlayerB = db.Players
                .Single(x => x.PlayerId == dbGame.PlayerBId);

            var dbBoardA = db.BoardStates
                .Where(p => p.PlayerId == dbGame.PlayerAId)
                .ToList();

            var dbBoardB = db.BoardStates
                .Where(p => p.PlayerId == dbGame.PlayerBId)
                .ToList();
            
            var dbShipsA = db.Ships
                .Where(s => s.PlayerId == dbPlayerA.PlayerId)
                .ToList();
            
            var dbShipsB = db.Ships
                .Where(s => s.PlayerId == dbPlayerB.PlayerId)
                .ToList();

                if (dbPlayerB.EPlayerType == EPlayerType.AI)
            {
                var dbBot = db.Bots.FirstOrDefault(p => p.PlayerId == dbGame.PlayerBId);
                if (dbBot != null)
                {
                    _botCurrentDirection = _botCurrentDirection != null ? dbBot.BotCurrentDirection : "none";
                    if(dbBot.BotFirstMoveX != null && dbBot.BotFirstMoveY != null)
                    {
                        _botFirstMove = $"{dbBot.BotFirstMoveX}-{dbBot.BotFirstMoveY}";
                    }
                    else
                    {
                        _botFirstMove = null;
                    }
                    if(dbBot.BotLastShipHitX != null && dbBot.BotLastShipHitY != null)
                    {
                        if(dbBot.BotLastShipHitX != -1 && dbBot.BotLastShipHitY != -1)
                        {
                            _botLastShipHit = $"{dbBot.BotLastShipHitX}-{dbBot.BotLastShipHitY}";
                        }
                        else
                        {
                            _botLastShipHit = "fail";
                        }
                    }
                    else
                    {
                        _botLastShipHit = null;
                    }
                }
            }
            
            _board1 = new ECellState[dbGame.BoardWidth ,dbGame.BoardHeight];
            _board2 = new ECellState[dbGame.BoardWidth ,dbGame.BoardHeight];
            //Console.WriteLine(dbPlayerA.Name);
            _playerA = dbPlayerA.Name;
            _playerB = dbPlayerB.Name;
            _boatsCanTouch = dbGame.EBoatsCanTouch;
            _nextMoveByP1 = !Convert.ToBoolean(dbGame.ENextMoveAfterHit);
            _playerAType = dbPlayerA.EPlayerType;
            _playerBType = dbPlayerB.EPlayerType;
            _boardHeight = dbGame.BoardHeight;
            _boardWidth = dbGame.BoardHeight;
            _totalShips = dbBoardA.Where(p => p.PlayerId == dbGame.PlayerAId).Count(s => s.Value == ECellState.Ship || s.Value == ECellState.Shiphit);

            for (int i = 1; i < dbShipsA.Count+1; i++)
            {
                if(!_ships1.ContainsKey(i)) { _ships1.Add(i, dbShipsA[i-1].Cells); }
            }
            
            for (int i = 1; i < dbShipsB.Count+1; i++)
            {
                if(!_ships2.ContainsKey(i)) { _ships2.Add(i, dbShipsB[i-1].Cells); }
            }
            
            foreach (var board in dbBoardA)
            {
                _board1[board.ArrayIndexX, board.ArrayIndexY] = (ECellState) board.Value;
            }
            
            foreach (var board in dbBoardB)
            {
                _board2[board.ArrayIndexX, board.ArrayIndexY] = (ECellState) board.Value;
            }

            _gameFinished = false;
            _gameStarted = true;
        }

        public bool NextMoveByP1 => _nextMoveByP1;
        public string PlayerA => _playerA;
        public string PlayerB => _playerB;
        public EPlayerType PlayerAType => _playerAType;
        public EPlayerType PlayerBType => _playerBType;
        public int BoardWidth => _boardWidth;
        public int BoardHeight => _boardHeight;
        public bool GameFinished => _gameFinished;
        //public bool GameStarted => _gameStarted;
        public string? BotFirstMove => _botFirstMove;
        public string? BotLastShipHit => _botLastShipHit;
        public string? BotCurrentDirection => _botCurrentDirection;
        private int MaxVal => _boardHeight - 1;
    }
}