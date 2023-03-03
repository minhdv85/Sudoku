using System;
using System.Collections.Generic;

namespace MySudoku.Code
{
	/// <summary>
	/// Description of IPuzzleLoader.
	/// </summary>
	public interface IPuzzleLoader
	{
		void LoadNewPuzzle(List<Cell> cellList, out int puzzleNumber);
	}
}
