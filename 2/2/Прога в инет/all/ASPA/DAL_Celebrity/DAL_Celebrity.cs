using System;
using System.Collections.Generic;
namespace DAL_Celebrity
{

    public interface IRepository<T1, T2> : IMix<T1, T2>, ICelebrity<T1>, ILifevent<T2> { }

    public interface IMix<T1, T2>
    {
        List<T2> GetLifEventsByCelebrityId(int celebrityId); // Получить все события по Id знаменитости
        T1? GetCelebrityByLifEventId(int lifEventId); // Получить знаменитость по Id события
    }

    public interface ICelebrity<T> : IDisposable
    {
        List<T> GetAllCelebrities(); // Получить все знаменитости
        T GetCelebrityById(int id); // Получить знаменитость по Id
        bool DeleteCelebrity(int id); // Удалить знаменитость по Id
        bool AddCelebrity(T celebrity); // Добавить знаменитость
        bool UpdateCelebrity(int id, T celebrity); // Изменить знаменитость по Id
        int GetCelebrityIdByName(string name); // Получить первый Id по вхождению подстроки
    }

    public interface ILifevent<T> : IDisposable
    {
        List<T> GetAllLifevents(); // Получить все события
        T GetLifeventById(int id); // Получить событие по Id
        bool DeleteLifevent(int id); // Удалить событие по Id
        bool AddLifevent(T lifEvent); // Добавить событие
        bool UpdateLifevent(int id, T lifEvent); // Изменить событие по Id
    }
    public class DAL_Celebrity
    {

    }
}
