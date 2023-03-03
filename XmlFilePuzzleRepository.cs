/*
 * Created by SharpDevelop.
 * User: Amos
 * Date: 5/21/2016
 * Time: 12:18 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Microsoft.AspNetCore.Hosting.Server;
using System;
using System.IO;
using System.Xml.Linq;

namespace MySudoku
{
	/// <summary>
	/// Description of XmlFilePuzzleRepository.
	/// </summary>
	public class XmlFilePuzzleRepository : IPuzzleRepository
	{
		private const string puzzleSetupXmlString = "PuzzleSetup.xml";
		private const string savedGameXmlString = "SavedGame.xml";
		
		readonly private string puzzleSetupXmlPath = string.Empty;
		readonly private string savedGameXmlPath = string.Empty;
		
		public XmlFilePuzzleRepository()
		{
			puzzleSetupXmlPath = Path.Combine("C:\\Users\\Admin\\Downloads\\Sudoku project\\MySudoku\\MySudoku", puzzleSetupXmlString);
			savedGameXmlPath = Path.Combine("C:\\Users\\Admin\\Downloads\\Sudoku project\\MySudoku\\MySudoku", savedGameXmlString);
		}
		
		public XDocument LoadPuzzleSetupXDoc()
		{
			return XDocument.Load(puzzleSetupXmlPath);
		}
		
		public XDocument LoadSavedGameXDoc()
		{
			return XDocument.Load(savedGameXmlPath);
		}
		
		public void SavePuzzleSetupXDoc(XDocument xDoc)
		{
			xDoc.Save(puzzleSetupXmlPath);
		}
		
		public void SaveSavedGameXDoc(XDocument xDoc)
		{
			xDoc.Save(savedGameXmlPath);
		}
	}
}
