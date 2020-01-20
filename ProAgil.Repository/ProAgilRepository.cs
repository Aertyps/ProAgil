using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Proagil.Repository;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    
    public class ProAgilRepository : IProAgilRepository
    {
        private readonly ProAgilContext _context;
        public ProAgilRepository(ProAgilContext context){
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        public void Add<T>(T entity) where T : class
        {
           _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
             _context.Remove(entity);
        }
        public void Update<T>(T entity) where T : class
        {
             _context.Update(entity);
        }
        public async Task<bool> SaveChangesAsync()
        {
             return (await _context.SaveChangesAsync()) > 0;
        }
        
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }
            query = query.OrderByDescending(c => c.DataEvento);
            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncById(int EventoId, bool includePalestrantes)
        {
         IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }
            query = query.OrderByDescending(c => c.DataEvento)
            .Where(c => c.Id == EventoId);

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedeSociais);

            if(includePalestrantes){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Palestrante);
            }
            query = query.OrderByDescending(c => c.DataEvento)
            .Where(c => c.Tema.Contains(tema));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsync(int PalestranteId, bool includeEventos = false)
        {
           IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(includeEventos){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Evento);
            }
            query = query.OrderBy(c => c.Nome)
            .Where(c => c.Id == PalestranteId);

            return await  query.ToArrayAsync();
        }

        public async Task<Palestrante []> GetAllPalestrantesAsyncByName(string nome,bool includeEventos = false)
        {
           IQueryable<Palestrante> query = _context.Palestrantes
            .Include(c => c.RedesSociais);

            if(includeEventos){
                query = query
                        .Include(pe => pe.PalestranteEventos)
                        .ThenInclude(p => p.Evento);
            }
            query = query
            .Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await  query.ToArrayAsync();
        }

        
    }
}