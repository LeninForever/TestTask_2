namespace TestTask.Dto.Statistics
{
    public record ClientsOrdersSumCostInBirthdayDto
    {
        public long Id { get; init; }
        public string FirstName { get; init; }
        public string LastName { get; init; }
        public double SummaryCost { get; init; }
    }
}
