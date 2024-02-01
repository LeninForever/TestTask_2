using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TestTask.Converters;

namespace TestTask.Dto.Client
{
    public record ClientDto
    {
        public ClientDto(long id, string firstName, string lastName, DateOnly birthDate)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        [Required]
        public long Id { get; init; }

        [Required]
        public string FirstName { get; init; }

        [Required]
        public string LastName { get; init; }

        [Required]
        [JsonConverter(typeof(DateOnlyJsonConverter))]
        public DateOnly BirthDate { get; init; }


    }
}
