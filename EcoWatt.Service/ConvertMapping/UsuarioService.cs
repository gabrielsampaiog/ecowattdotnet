using EcoWatt.Model;
using EcoWatt.Model.DTO.Request;
using EcoWatt.Model.DTO.Response;

namespace EcoWatt.Service.ConvertMapping
{
    public class UsuarioService
    {
        //Métodos de conversão
        public Usuario requestToUsuario(UsuarioRequest usuarioRequest)
        {
            Usuario usuario = new Usuario();

            usuario.DsUsuario = usuarioRequest.DsUsuario;
            usuario.DsSenha = usuarioRequest.DsSenha;
            usuario.DsNomeCompleto = usuarioRequest.DsNomeCompleto;
            usuario.DtCriacao = usuarioRequest.DtCriacao;
            usuario.DtModificacao = usuarioRequest.DtModificacao;

            return usuario;
        }

        public UsuarioResponse usuarioToResponse(Usuario usuario)
        {
            UsuarioResponse usuarioResponse = new UsuarioResponse();

            usuarioResponse.IdUsuario = usuario.IdUsuario;
            usuarioResponse.DsUsuario = usuario.DsUsuario;
            usuarioResponse.DsNomeCompleto = usuario.DsNomeCompleto;
            usuarioResponse.DtCriacao = usuario.DtCriacao;
            usuarioResponse.DtModificacao = usuario.DtModificacao;


            return usuarioResponse;
        }

        public IEnumerable<UsuarioResponse> usuariosToResponseIEnumerable(IEnumerable<Usuario> usuarios)
        {
            var usuarioResponses = usuarios.Select(usuario => new UsuarioResponse
            {
                IdUsuario = usuario.IdUsuario,
                DsUsuario = usuario.DsUsuario,
                DsNomeCompleto = usuario.DsNomeCompleto,
                DtCriacao = usuario.DtCriacao,
                DtModificacao = usuario.DtModificacao
            });

            return usuarioResponses;
        }
    }
}
