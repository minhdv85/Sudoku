
using System;
using System.Collections.Generic;

namespace MySudoku.Code
{
	/// <summary>
	/// Description of IPuzzleService.
	/// </summary>
	public interface IPuzzleService
	{
		List<Cell> SetupBoard();
	}
}
