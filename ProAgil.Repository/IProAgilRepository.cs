using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    //Geral
    public interface IProAgilRepository
    {
         void Add<T>(T entity) where T : class;
         void Update<T>(T entity) where T : class;
         void Delete<T>(T entity) where T : class;
         Task<bool> SaveChangesAsync();
//Eventos
         Task<Evento[]>GetAllEventosAsyncByTema(string tema, bool includePalestrantes);
         Task<Evento[]>GetAllEventosAsync(bool includePalestrantes);
         Task<Evento[]>GetAllEventosAsyncById(int EventoId,bool includePalestrantes);
//Palestrantes
         Task<Palestrante[]>GetAllPalestrantesAsyncByName(string name, bool includeEventos);
         Task<Palestrante[]>GetAllPalestrantesAsync(int EventoId,bool includeEventos);

    }
}