namespace ClasseUser.cs;

public class Class1
{

}

//Classe Usuário
using System;

public class Usuario
{
    // Propriedades do usuário
    public string NomeUsuario { get; set; }
    public string Senha { get; set; }
    public string CodigoConfirmacao { get; set; }

    // Método para validar o usuário
    public bool ValidarUsuario()
    {
        // Validação do Nome de Usuário
        if (string.IsNullOrEmpty(NomeUsuario) || NomeUsuario.Length < 7)
        {
            Console.WriteLine("Nome de usuário inválido. Deve ter pelo menos 7 caracteres.");
            return false;
        }

        // Validação da Senha
        if (string.IsNullOrEmpty(Senha) || Senha.Length < 7 || !Senha.Any(char.IsDigit) || !Senha.Any(char.IsUpper) || !Senha.Any(char.IsLower) || Senha.Any(char.IsWhiteSpace))
        {
            Console.WriteLine("Senha inválida. Deve ter pelo menos 7 caracteres e incluir números, letras maiúsculas e minúsculas, e não deve conter espaços em branco.");
            return false;
        }

        // Validação do Código de Confirmação
        if (string.IsNullOrEmpty(CodigoConfirmacao) || CodigoConfirmacao.Length != 4)
        {
            Console.WriteLine("Código de confirmação inválido. Deve ter exatamente 4 caracteres.");
            return false;
        }

        // Autenticação - Simulação de verificação no banco de dados
        if (!Autenticador.VerificarCodigoConfirmacao(CodigoConfirmacao))
        {
            Console.WriteLine("Credenciais incorretas.");
            return false;
        }

        // Todas as validações passaram
        return true;
    }
}

public static class Autenticador
{
    // Método para simular verificação do código de confirmação no banco de dados
    public static bool VerificarCodigoConfirmacao(string codigo)
    {
        // Simulação: Código de confirmação válido se for "1234"
        return codigo == "1234";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Exemplo de uso
        Usuario usuario = new Usuario();
        usuario.NomeUsuario = "usuario123";
        usuario.Senha = "Senha123";
        usuario.CodigoConfirmacao = "1234";

        if (usuario.ValidarUsuario())
        {
            Console.WriteLine("Usuário validado com sucesso.");
            // Redirecionar para a tela inicial
        }
        else
        {
            Console.WriteLine("Falha na validação do usuário.");
            // Fornecer feedback adequado ao usuário
        }
    }
}


//Classe NavBar
using System;

public class NavBar
{
    // Propriedades da NavBar
    public Usuario Usuario { get; set; }
    public string[] Dashboard { get; set; }

    // Método para verificar as permissões do usuário e redirecionar para o dashboard
    public void AcessarDashboard(string opcao)
    {
        // Verificar se o usuário está autenticado
        if (Usuario == null || !Usuario.ValidarUsuario())
        {
            Console.WriteLine("Usuário não autenticado. Acesso negado.");
            return;
        }

        // Redirecionar para o dashboard selecionado
        if (Dashboard.Contains(opcao))
        {
            Console.WriteLine($"Redirecionando para o dashboard: {opcao}");
        }
        else
        {
            Console.WriteLine($"Dashboard {opcao} não encontrado.");
        }
    }

    // Método para processar filtros
    public void ProcessarFiltros()
    {
        // Implementação do processamento de filtros
        Console.WriteLine("Filtros processados com sucesso.");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Exemplo de uso
        NavBar navBar = new NavBar();
        navBar.Usuario = new Usuario();
        navBar.Usuario.NomeUsuario = "usuario123";
        navBar.Usuario.Senha = "Senha123";
        navBar.Usuario.CodigoConfirmacao = "1234";

        navBar.AcessarDashboard("GPTW");
        navBar.ProcessarFiltros();
    }
}

//Classe Filtro
using System;

public class Filtro
{
    // Propriedades do Filtro
    public string Campo { get; set; }
    public string Tipo { get; set; }
    public string Operador { get; set; }

    // Método para obter campos disponíveis para filtragem
    public string[] ObterCamposDisponiveis()
    {
        // Implementação para fornecer campos disponíveis para filtragem
        return new string[] { "Cargo", "Tipo de Contrato", "Planta", "Ano" };
    }

    // Método para obter tipos de filtro disponíveis
    public string[] ObterTiposDisponiveis()
    {
        // Implementação para fornecer tipos de filtro disponíveis
        return new string[] { "Texto", "Numérico" };
    }

    // Método para obter operadores de filtro disponíveis
    public string[] ObterOperadoresDisponiveis()
    {
        // Implementação para fornecer operadores de filtro disponíveis
        return new string[] { "Igual a", "Diferente de", "Maior que", "Menor que" };
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Exemplo de uso
        Filtro filtro = new Filtro();
        string[] campos = filtro.ObterCamposDisponiveis();
        string[] tipos = filtro.ObterTiposDisponiveis();
        string[] operadores = filtro.ObterOperadoresDisponiveis();

        Console.WriteLine("Campos disponíveis para filtragem:");
        foreach (var campo in campos)
        {
            Console.WriteLine(campo);
        }

        Console.WriteLine("\nTipos de filtro disponíveis:");
        foreach (var tipo in tipos)
        {
            Console.WriteLine(tipo);
        }

        Console.WriteLine("\nOperadores de filtro disponíveis:");
        foreach (var operador in operadores)
        {
            Console.WriteLine(operador);
        }
    }
}
