using System.ComponentModel.DataAnnotations;

namespace MySudoku.Models
{
    public class SolveView
    {
        public int Id { get; set; }
        public string SudokuResult { get; set; }
        public DateTime SolveDate { get; set; }
    }
}
