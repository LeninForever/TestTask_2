namespace TestTask.Repositories.Filters
{
    public class ClientEntityFilter  : PageFilterBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateOnly? BirthDateFrom { get; set; }
        public DateOnly? BirthDateTo { get; set; }
    }
}
