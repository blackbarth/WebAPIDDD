using System.Threading.Tasks;
using API.Domain.DTOs;
using API.Domain.Entities;

namespace API.Domain.Interfaces.Services.User
{
    public interface ILoginService
    {
        Task<object> FindByLogin(LoginDTO user);
    }
}