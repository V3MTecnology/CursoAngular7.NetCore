using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public readonly ProAgilContext _context;

        public ProAgilRepository(ProAgilContext context )
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
        
        //===============================Gerais==============================
        public void Add<T>(T entry) where T : class
        {
            _context.Add(entry);
        }       

        public void Update<T>(T entry) where T : class
        {
            _context.Update(entry);
        }        
        public void Delete<T>(T entry) where T : class
        {
            _context.Remove(entry);
        }        
        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        //===============================Evento============================
        //Palestrantes = false porque posso ou não passar este parâmetro
        public async Task<Evento[]> GetAllEventoAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos )
                .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderByDescending( c => c.DataEvento); //Se quiser passar o campo a ser ordernado pode ser passado por parametro igual includePalestrantes

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes =false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos )
                .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderByDescending( c => c.DataEvento)
            .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if (includePalestrantes)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos )
                .ThenInclude(p => p.Palestrante);
            }

            query = query.AsNoTracking().OrderByDescending( c => c.DataEvento)
            .Where(c => c.Id ==EventoId);

            return await query.FirstOrDefaultAsync();
        }

        //===============================Papestrante=======================================
        
        public async Task<Palestrante> GetPalestranteAsync(int PalestranteId, bool includeEvento = false)
        {
              IQueryable<Palestrante> query = _context.Palestrantes 
            .Include(c => c.RedesSociais);

            if (includeEvento)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos )
                .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking().OrderBy( p => p.Nome)
            .Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }
        public async Task<Palestrante[]> GetAllPalestranteAsyncByName(string name , bool includeEvento = false)
        {
              IQueryable<Palestrante> query = _context.Palestrantes 
            .Include(c => c.RedesSociais);

            if (includeEvento)
            {
                query = query
                .Include(pe => pe.PalestrantesEventos )
                .ThenInclude(e => e.Evento);
            }

            query = query.AsNoTracking()
                         .Where(p => p.Nome.ToLower().Contains(name.ToLower()));
            
            return await query.ToArrayAsync();
        }        

        
    }
}