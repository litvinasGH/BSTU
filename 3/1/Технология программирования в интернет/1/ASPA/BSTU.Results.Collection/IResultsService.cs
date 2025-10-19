namespace BSTU.Results.Collection
{
    public record ResultItem(int Id, string Value);

    public interface IResultsService
    {
        /// <summary>
        /// Получить все Results
        /// </summary>
        /// <returns>List всех ResultItem</returns>
        Task<List<ResultItem>> GetAllAsync();
        /// <summary>
        /// Получить Result по id
        /// </summary>
        /// <param name="id">id результа</param>
        /// <returns>Result по id</returns>
        Task<ResultItem?> GetAsync(int id);
        /// <summary>
        /// Добавление Резулта
        /// </summary>
        /// <param name="value">Значение</param>
        /// <returns>ResultItem</returns>
        Task<ResultItem> AddAsync(string value);
        /// <summary>
        /// Изменяет Result по id
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="value">Значение</param>
        /// <returns>Успешность</returns>
        Task<bool> UpdateAsync(int id, string value);
        /// <summary>
        /// Удаление Result по id
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>Успешность</returns>
        Task<bool> DeleteAsync(int id);
    }
}
