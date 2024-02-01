using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TestTask.Converters;

namespace TestTask.Filters
{
    public class ClientFilter : PageFilterBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? BirthDateFrom { get; set; }

        public string? BirthDateTo { get; set; }
    }
}
