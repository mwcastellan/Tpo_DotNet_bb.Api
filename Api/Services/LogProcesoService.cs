namespace Tpo_DotNet_bb.Api.Services
{
    public class LogProcesoService : ILogProcesoService
    {
        private readonly Entities.AppDbContext _context;

        public LogProcesoService(
            Entities.AppDbContext context)
        {
            _context = context;
        }

        public async Task GrabarAsync(
            string mensaje)
        {
            var log = new Entities.Logs_Procesos
            {
                MENSAJE = mensaje
            };

            _context.Logs_Procesos.Add(log);

            await _context.SaveChangesAsync();
        }
    }
}
