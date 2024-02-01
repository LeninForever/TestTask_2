using System.ComponentModel.DataAnnotations;

namespace TestTask.Filters
{
    public class PageFilterBase
    {
        [Range(0, int.MaxValue, ErrorMessage = "Размер страницы не может быть отрицательным")]
        public int? PageSize { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Номер страницы не может быть меньше нуля")]
        public int? PageNumber { get; set; }
    }

}
