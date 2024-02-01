using TestTask.Dto.Client;
using TestTask.Entities;
using TestTask.Filters;

namespace TestTask.Services.Interfaces
{
    public interface IClientService
    {
        /// <summary>
        /// Возвращает всех клиентов. Если клиентов не нашлось, возвращает Enumarable.Empty
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IEnumerable<ClientDto>> GetAllClientsAsync(ClientFilter filter);
        
        /// <summary>
        /// Возвращает null, если клиент не был найден
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClientDto?> GetClientByIdAsync(long id);

        /// <summary>
        /// Добавляет клиента. Возвращает добавленного клиента
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ClientDto> CreateAsync(ClientDto dto);

        /// <summary>
        /// Обновляет информацию о клиенте. Если клиент не найден, возвращает null.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ClientDto?> UpdateAsync(ClientDto dto);

        /// <summary>
        /// Удаляет информацию о клиенте. Если клиент не был найден, возвращает null
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ClientDto?> DeleteAsync(long id);
    }
}
