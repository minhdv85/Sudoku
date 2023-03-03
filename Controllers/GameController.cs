using System;
using MySudoku.Code;
using MySudoku.Model;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using System.Text;
using Newtonsoft.Json;
using MySudoku.Models;

namespace MVCSuDoku4.Controllers
{
    /// <summary>
    /// Description of GameController.
    /// </summary>
    public class GameController : Controller
    {
        private IPuzzleLoader puzzleLoader;
        private IPuzzleService puzzleService;

        public GameController(IPuzzleLoader loader, IPuzzleService service)
        {
            puzzleLoader = loader;
            puzzleService = service;
            // Load Constants into ViewData for partial view
            ViewData["BoardSize"] = Constants.BoardSize;
            ViewData["BlockSize"] = Constants.BlockSize;
        }

        public IActionResult NewGame()
        {
            FullBoard board = new FullBoard() { BoardList = puzzleService.SetupBoard(), BoardSize = Constants.BoardSize, BlockSize = Constants.BlockSize };
            int puzzleNumber;
            puzzleLoader.LoadNewPuzzle(board.BoardList, out puzzleNumber);
            board.BoardNumber = puzzleNumber;

            return View("GameView", board);
        }

        public ActionResult SolveGame(FullBoard board)
        {
            ProblemSolver(board);
            board.BoardSize = Constants.BoardSize;
            board.BlockSize = Constants.BlockSize;
            int puzzleNumber;
            puzzleLoader.LoadNewPuzzle(board.BoardList, out puzzleNumber);
            board.BoardNumber = puzzleNumber;

            return View("GameView", board);
        }
        public async Task<JsonResult> Save(FullBoard board)
        {
            string apiUrl = "https://localhost:7058/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            HttpResponseMessage response = client
                                .PostAsJsonAsync<dynamic>("api/SudokuAPI/Save", board.BoardList).Result;
            if (response.IsSuccessStatusCode)
            {
                return Json(new { status = true, msg = "Save success" });
            }

            return Json(new { status = false, msg = "Something wrong!" });
        }
        public async Task<ActionResult> LoadGame()
        {
            return View("SolverView");
        }
        public async Task<JsonResult> GetData()
        {
            List<SolveView> reservationList = null;
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7058/api/SudokuAPI/GetData"))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationList = JsonConvert.DeserializeObject<List<SolveView>>(apiResponse);
                }
            }
            return Json(reservationList);
        }
        public bool ProblemSolver(FullBoard board)
        {
            for (int i = 0; i < board.BoardList.Count; i++)
            {
                if (board.BoardList[i].Value == null)
                {
                    for (int j = 1; j <= 9; j++)
                    {
                        //if (isVaild(board.BoardList, board.BoardList[i].XCoordinate, board.BoardList[i].YCoordinate, j))
                        if(IsValid(board.BoardList))
                        {
                            board.BoardList[i].Value = j;
                            //break;
                            if (ProblemSolver(board))
                                return true;
                            else
                                board.BoardList[i].Value = null;
                        }
                    }
                    return false;
                }
            }
            return true;
        }

        public  bool IsValid(List<Cell> cellList)
        {
            bool isValid = AreRowsValid(cellList);
            isValid &= AreColumnsValid(cellList);
            isValid &= AreBlocksValid(cellList);

            return isValid;
        }

        private bool AreRowsValid(List<Cell> cellList)
        {
            bool isValid = true;

            cellList.GroupBy(c => c.XCoordinate).Select(g => g.ToList()).ToList().ForEach(s => isValid &= IsValueUniqueInSet(s));

            return isValid;
        }

        private  bool AreColumnsValid(List<Cell> cellList)
        {
            bool isValid = true;

            cellList.GroupBy(c => c.YCoordinate).Select(g => g.ToList()).ToList().ForEach(s => isValid &= IsValueUniqueInSet(s));

            return isValid;
        }

        private  bool AreBlocksValid(List<Cell> cellList)
        {
            bool isValid = true;

            cellList.GroupBy(c => c.BlockNumber).Select(g => g.ToList()).ToList().ForEach(s => isValid &= IsValueUniqueInSet(s));

            return isValid;
        }
        private static bool IsValueUniqueInSet(List<Cell> cellGroup)
        {
            // Validate that each non-NULL value in this group is unique.  Ignore NULL values.
            return cellGroup.Where(c => c.Value.HasValue).GroupBy(c => c.Value.Value).All(g => g.Count() <= 1);
        }
        //public bool isVaild(List<Cell> cells, int x, int y, int vals)
        //{
        //    for (int i = 1; i <= 9; i++)
        //    {
        //        var row = cells.Where(c => c.XCoordinate == i && c.YCoordinate == y);
        //        //check row
        //        if (row.Any(x => x.Value != null) && row.Any(x => x.Value == vals))
        //            return false;
        //        var col = cells.Where(c => c.XCoordinate == x && c.YCoordinate == i);
        //        //check col
        //        if (col.Any(x => x.Value != null) && col.Any(c => c.Value == vals))
        //            return false;
        //        //check block
        //        var _x = 3 * (x / 3) + i / 3;
        //        var _y = 3 * (y / 3) + i % 3;
        //        var block = cells.Where(c => c.XCoordinate == _x && c.YCoordinate == _y);
        //        if (block.Any(x => x.Value != null) && block.Any(x => x.Value == vals))
        //            return false;
        //    }
        //    return true;
        //}
    }
}