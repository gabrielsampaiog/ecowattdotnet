using EcoWatt.Model;
using EcoWatt.Model.DTO.Request;


namespace EcoWatt.Service.UsuarioUseServices
{
    public interface IUsuarioUseService
    {
        void AddUsuarioUse(UsuarioUseRequest usuarioUseRequest);

        Task<IEnumerable<UsuarioUse>> GetUsuarioUses();
    }
}
