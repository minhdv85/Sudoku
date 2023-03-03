using System;
using System.Collections.Generic;
using System.Linq;
using MySudoku.Code;

namespace MySudoku.Model
{
	/// <summary>
	/// A Sudoku board
	/// </summary>
	public class FullBoard
	{
		public List<Cell> BoardList { get; set; }
		public int BoardNumber { get; set; }
		public PuzzleStatus Status { get; set; }
		public int BoardSize { get; set; }
		public int BlockSize { get; set; }
		public FullBoard()
		{
			BoardList = new List<Cell>();
			Status = PuzzleStatus.Normal;
		}
	}
}
