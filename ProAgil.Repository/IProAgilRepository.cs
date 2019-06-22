using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public interface IProAgilRepository
    {
        //Geral qualquer entidade que quiser criar, alterar e deletar
        void Add<T>(T entry)where T : class;
        void Update<T>(T entry)where T : class;
        void Delete<T>(T entry)where T : class;
        Task<bool> SaveChangesAsync();

        //Eventos
        Task<Evento[]> GetAllEventoAsyncByTema( string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
        Task<Evento> GetEventoAsyncById( int EventoId, bool includePalestrantes);

        //Palestrante
        Task<Palestrante> GetPalestranteAsync( int PalestranteId, bool includeEvento);
        Task<Palestrante[]> GetAllPalestranteAsyncByName( string name ,bool includeEvento);
        


        // Task<Palestrante[]> GetAllPalestranteAsyncByName( string nome, bool includePalestrante);
        // Task<Palestrante[]> GetAllPalestranteAsync(bool includePalestrante);
        // Task<Palestrante> GetPalestranteAsync( int PalestranteId, bool includePalestrante);



    }
}