namespace SME.Pedagogico.Interface.Autenticacao
{
    public interface IAutenticacaoService : IAutenticacao
    {
        bool IsValido(string tokenStr, out string username);
    }
}
