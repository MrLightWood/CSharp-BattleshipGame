using System;
using Domain.Enums;
using MenuSystem;

namespace BattleShipUI
{
    public class GameUI
    {
        public static void DrawBoard(ECellState[,] board1, ECellState[,] board2, string playerA, string playerB)
        {
            // add plus 1, since this is 0 based. length 0 is returned as -1;
            var width = board1.GetUpperBound(0) + 1; // x
            var height = board1.GetUpperBound(1) + 1; // y
            
            string abc = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; 

            Console.WriteLine($"   \t \t Player {playerA} \t \t \t \t \t \t \t  {playerB}");
            
            for (int colIndex = 0; colIndex < width; colIndex++)
            {
                Console.Write($"   {colIndex+1}");    
            }
            Console.Write("\t \t \t");
            for (int colIndex = 0; colIndex < width; colIndex++)
            {
                Console.Write($"   {colIndex + 1}");
            }

            Console.WriteLine();
            
            for (int rowIndex = 0; rowIndex < height; rowIndex++)
            {
                Console.Write(abc[rowIndex]);
                for (int colIndex = 0; colIndex < width; colIndex++)
                {
                    DrawCellWithColor(board1[colIndex, rowIndex]);
                }
                Console.Write($"  {abc[rowIndex]}");
                
                Console.Write("\t \t \t");
                
                Console.Write(abc[rowIndex]);
                for (int colIndex = 0; colIndex < width; colIndex++)
                {
                    DrawCellWithColor(board2[colIndex, rowIndex]); 
                }
                Console.Write($"  {abc[rowIndex]}");
                
                Console.WriteLine();
            }
            
            for (int colIndex = 0; colIndex < width; colIndex++)
            {
                Console.Write($"   {colIndex+1}");
            }
            Console.Write("\t \t \t");
            for (int colIndex = 0; colIndex < width; colIndex++)
            {
                Console.Write($"   {colIndex+1}");
            }
            
            Console.WriteLine();
        }
        
        public static string CellString(ECellState cellState)
        {
            switch (cellState)
            {
                case ECellState.Empty: return "#";
                case ECellState.Bomb: return "X";
                case ECellState.Ship: return "#";
                case ECellState.Shiphit: return "@";
            }

            return "-";
            
        }

        private static void DrawCellWithColor(ECellState cs)
        {
            switch (cs)
            {
                case ECellState.Empty:
                    Menu.WriteWithColor($"  {CellString(cs)}", "green");
                    break;
                case ECellState.Bomb:
                    Menu.WriteWithColor($"  {CellString(cs)}", "red");
                    break;
                case ECellState.Ship:
                    Menu.WriteWithColor($"  {CellString(cs)}", "green");
                    break;
                case ECellState.Shiphit:
                    Menu.WriteWithColor($"  {CellString(cs)}", "yellow");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(cs), cs, null);
            }
        }
    }
}