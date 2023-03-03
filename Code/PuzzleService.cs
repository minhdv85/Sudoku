
using System;
using System.Collections.Generic;
using System.Linq;

namespace MySudoku.Code
{
	public enum PuzzleStatus
	{
		Normal,
		Invalid,
		Complete
	}

	/// <summary>
	/// This class provides all the methods needed to play a game of Sudoku.
	/// It initializes a Sudoku board, and determines the status of a puzzle.
	/// </summary>
	public class PuzzleService : IPuzzleService
	{
		public List<Cell> SetupBoard()
		{
			List<Cell> board = new List<Cell>();
			
			for (int x = 0; x < Constants.BoardSize; x++)
			{
				for (int y = 0; y < Constants.BoardSize; y++)
				{
					Cell newCell = new Cell() 
					{ 
						XCoordinate = x + 1, 
						YCoordinate = y + 1,
						BlockNumber = Constants.BlockSize * (x / Constants.BlockSize) + (y / Constants.BlockSize) + 1
					};
					board.Add(newCell);
				}
			}
			
			return board;
		}
	}
}
