/*
 * Created by SharpDevelop.
 * User: Amos
 * Date: 5/26/2016
 * Time: 7:39 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Linq;

namespace MySudoku
{
	/// <summary>
	/// Description of IPuzzleRepository.
	/// </summary>
	public interface IPuzzleRepository
	{
		XDocument LoadPuzzleSetupXDoc();
		
		XDocument LoadSavedGameXDoc();
		
		void SavePuzzleSetupXDoc(XDocument xDoc);
		
		void SaveSavedGameXDoc(XDocument xDoc);
	}
}
