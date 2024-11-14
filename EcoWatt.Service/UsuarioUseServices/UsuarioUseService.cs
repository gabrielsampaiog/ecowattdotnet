using EcoWatt.Model;
using EcoWatt.Model.DTO.Request;
using EcoWatt.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoWatt.Service.UsuarioUseServices
{
    public class UsuarioUseService(IRepository<UsuarioUse> usuarioUseRepository) : IUsuarioUseService
    {
        private readonly IRepository<UsuarioUse> _usuarioUseRepository = usuarioUseRepository;

        public async Task AddUsuarioUse(UsuarioUseRequest usuarioUseRequest)
        {
            UsuarioUse usuarioUse = new UsuarioUse();

            usuarioUse.IdUsuario = usuarioUseRequest.IdUsuario;
            usuarioUse.IdBateria = usuarioUseRequest.IdBateria;
            usuarioUse.UsedAt = usuarioUseRequest.UsedAt;

            await _usuarioUseRepository.Add(usuarioUse,3);
        }

        public async Task<IEnumerable<UsuarioUse>> GetUsuarioUses()
        {
            return await _usuarioUseRepository.GetAll();
        }
    }
}
