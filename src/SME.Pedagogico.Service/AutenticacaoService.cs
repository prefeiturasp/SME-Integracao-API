using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SME.Pedagogico.Interface.Autenticacao;
using SME.Pedagogico.Interface.Settings;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using static System.Text.Encoding;

namespace SME.Pedagogico.Service
{
    public class AutenticacaoService : IAutenticacaoService
    {
        private readonly IAutenticacaoRepository autenticacaoRepository;
        private readonly JwtTokenSettings jwtTokenSettings;
        private readonly AutorizacaoSettings autorizacaoSettings;

        public AutenticacaoService(IAutenticacaoRepository autenticacaoRepository,
                                    IOptions<JwtTokenSettings> jwtTokenSettings,
                                    IOptions<AutorizacaoSettings> autorizacaoSettings)
        {
            this.autenticacaoRepository = autenticacaoRepository;
            this.jwtTokenSettings = jwtTokenSettings?.Value;
            this.autorizacaoSettings = autorizacaoSettings?.Value;
        }

        public bool IsValido(string tokenStr, out string username)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            SecurityToken tokenValidado = default;
            username = string.Empty;

            var parametrosValidacaoJwt = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateAudience = true,
                ValidateIssuer= true,
                ValidIssuer = jwtTokenSettings.Issuer,
                ValidAudience = jwtTokenSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(UTF8
                        .GetBytes(jwtTokenSettings.IssuerSigningKey))
            };

            if (jwtHandler.CanReadToken(tokenStr))
            {
                try
                {
                    username = jwtHandler
                        .ValidateToken(tokenStr, 
                                       parametrosValidacaoJwt, 
                                       out tokenValidado)
                        .FindFirst("username")?
                        .Value;
                    return tokenValidado != default(SecurityToken);
                    //return true && IsHashValido(hash);
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        private bool IsHashValido(string hashAcesso)
        {
            return hashAcesso.Equals(autorizacaoSettings.HashSGP);
        }

        private string CriaHash(string senha)
        {
            byte[] hash;

            using (var algoritmoHash = SHA256.Create())
            {
                hash = algoritmoHash.ComputeHash(UTF8.GetBytes(senha));
            }

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                builder.Append(hash[i].ToString("x2"));
            }

            return builder.ToString();
        }

        public void TesteConexaoPostGre()
        {
            autenticacaoRepository.TesteConexaoPostGre();
        }
    }
}
