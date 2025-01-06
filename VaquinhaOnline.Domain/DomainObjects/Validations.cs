using System.Text.RegularExpressions;

namespace VaquinhaOnline.Domain.DomainObjects;

public class Validations
{
    public static void ValidarSeIgual(object obj1, object obj2, string mensagem)
    {
        if (!obj1.Equals(obj2))
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeDiferente(object obj1, object obj2, string mensagem)
    {
        if (obj1.Equals(obj2))
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarCaracteres(string valor, int maximo, string mensagem)
    {
        var lenght = valor.Trim().Length;
        if (lenght > maximo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarCaracteres(string valor, int minimo, int maximo, string mensagem)
    {
        var lenght = valor.Trim().Length;
        if (lenght < minimo || lenght > maximo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarExpressao(string pattern, string valor, string mensagem)
    {
        var regex = new Regex(pattern);

        if (!regex.IsMatch(pattern))
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeNulo(string valor, string mensagem)
    {
        if (valor == null || valor.Trim().Length == 0)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeNulo(object valor, string mensagem)
    {
        if (valor == null)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarCaracteres(Object @object, string mensagem)
    {
        if (@object == null)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarMinimoMaximo(double valor, double minimo, double maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarMinimoMaximo(float valor, float minimo, float maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarMinimoMaximo(int valor, int minimo, int maximo, string mensagem)
    {
        if (valor < minimo || valor > maximo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeMenorIgualMinimo(long valor, long minimo, string mensagem)
    {
        if (valor <= minimo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeMenorIgualMinimo(double valor, double minimo, string mensagem)
    {
        if (valor <= minimo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeMenorIgualMinimo(decimal valor, decimal minimo, string mensagem)
    {
        if (valor <= minimo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeMenorIgualMinimo(int valor, int minimo, string mensagem)
    {
        if (valor <= minimo)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeFalso(bool boolvalor, string mensagem)
    {
        if (boolvalor)
        {
            throw new DomainException(mensagem);
        }
    }

    public static void ValidarSeVerdadeiro(bool boolvalor, string mensagem)
    {
        if (!boolvalor)
        {
            throw new DomainException(mensagem);
        }
    }

}


