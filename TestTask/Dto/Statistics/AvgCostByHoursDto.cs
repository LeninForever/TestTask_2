namespace TestTask.Dto.Statistics
{
    public record AvgCostByHoursDto
    {
        public int Hour { get; init; }
        public double AvgCost { get; init; }
    }
}
