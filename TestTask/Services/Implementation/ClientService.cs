using AutoMapper;
using TestTask.Dto.Client;
using TestTask.Entities;
using TestTask.Filters;
using TestTask.Repositories.Filters;
using TestTask.Repositories.Interfaces;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementation
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<ClientDto> CreateAsync(ClientDto dto)
        {
            Client client = _mapper.Map<Client>(dto);

            await _clientRepository.CreateAsync(client);
            await _clientRepository.SaveChangesAsync();

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto?> DeleteAsync(long id)
        {
            Client? client = await _clientRepository.FindAsync(id);

            if (client == null)
                return null;

            await _clientRepository.DeleteAsync(client);
            await _clientRepository.SaveChangesAsync();
            return _mapper.Map<ClientDto?>(client);

        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync(ClientFilter filter)
        {
            ClientEntityFilter entityFilter = _mapper.Map<ClientEntityFilter>(filter);

            IEnumerable<Client>? clients = await _clientRepository.GetAllAsync(entityFilter);

            if (clients == null)
                return Enumerable.Empty<ClientDto>();

            return clients.Select(x => _mapper.Map<ClientDto>(x));
        }

        public async Task<ClientDto?> GetClientByIdAsync(long id)
        {
            Client? client = await _clientRepository.FindAsync(id);

            if (client == null)
                return null;

            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto?> UpdateAsync(ClientDto dto)
        {
            Client? client = await _clientRepository.FindAsync(dto.Id);

            if (client == null)
                return null;

            client.BirthDate = dto.BirthDate;
            client.FirstName = dto.FirstName;
            client.LastName = dto.LastName;

            await _clientRepository.UpdateAsync(client);
            await _clientRepository.SaveChangesAsync();

            return _mapper.Map<ClientDto>(client);
        }
    }
}
