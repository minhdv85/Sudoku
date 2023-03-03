using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MySudoku.Code
{
	/// <summary>
	/// This class loads SuDoku puzzles that have been saved as Xml Documents.
	/// </summary>
	public class XDocPuzzleLoader : IPuzzleLoader
	{
		private Random random = new Random();
		private IPuzzleRepository xDocPuzzleRepository = null;
		
		public XDocPuzzleLoader(IPuzzleRepository puzzleRepository)
		{
			xDocPuzzleRepository = puzzleRepository;
		}
		
		public void LoadNewPuzzle(List<Cell> cellList, out int puzzleNumber)
		{
			puzzleNumber = GetRandomPuzzleNumber();
			LoadPuzzleFromSetupXDoc(puzzleNumber, cellList);
		}
		
		public void ReloadPuzzle(List<Cell> cellList, int puzzleNumber)
		{
			LoadPuzzleFromSetupXDoc(puzzleNumber, cellList);
		}
		
		private int GetRandomPuzzleNumber()
		{
			XDocument puzzleSetupDoc = xDocPuzzleRepository.LoadPuzzleSetupXDoc();
			
			var puzzleNumberList = puzzleSetupDoc.Descendants("Puzzle").Select(b => (int)b.Element("Number")).ToList();
			int puzzleConfigCount = puzzleNumberList.Count();
			int randomPuzzleIndex = random.Next(puzzleConfigCount);
			return puzzleNumberList[randomPuzzleIndex];
		}
		
		private void LoadPuzzleFromSetupXDoc(int puzzleNumber, List<Cell> cellList)
		{
			XDocument puzzleSetupXDoc = xDocPuzzleRepository.LoadPuzzleSetupXDoc();
			
			XElement x = puzzleSetupXDoc.Descendants("Puzzle").First(b => (int)b.Element("Number") == puzzleNumber);
			LoadCellListFromPuzzleXElement(x, cellList);
		}
		
		private void LoadCellListFromPuzzleXElement(XElement puzzleXElement, List<Cell> cellList)
		{
			var y = puzzleXElement.Descendants("Cells").Descendants("Cell").ToList();
			y.ForEach(c => cellList[(int)c.Attribute("index")].Value = (int?)c.Attribute("value"));
		}
	}
}
