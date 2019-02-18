using System.Threading.Tasks;
namespace Services.Services
{
    public interface ICurrencyHttpService
    {
        Task<double> GetEuroToUSdRate();
    }
}
