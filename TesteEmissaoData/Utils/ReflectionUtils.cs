using System;
using System.Linq;

namespace EmissorNfse.Module.Utils
{
    public static class ReflectionUtils
    {
        /// <summary>
        /// Verifica se o tipo informado é um tipo primario.
        /// [string, int, bool, decimal, DateTime, byte, sbyte, byte[]]
        /// </summary>
        /// <param name="type">Tipo a ser verificado.</param>
        /// <returns>Verdadeiro se o tipo for primario, caso contrário falso.</returns>
        public static bool IsPrimaryType(Type type)
        {
            return type.Equals(typeof(string)) ||
                   type.Equals(typeof(int)) ||
                   type.Equals(typeof(bool)) ||
                   type.Equals(typeof(decimal)) ||
                   type.Equals(typeof(DateTime)) ||
                   type.Equals(typeof(byte)) ||
                   type.Equals(typeof(sbyte)) ||
                   type.Equals(typeof(byte[]));
        }

        public static T GetEnumMemberWithMember<T>(this int value)
        {
            return Enum.GetValues(typeof(T)).OfType<T>().FirstOrDefault(f => f.ToString().EndsWith(value.ToString()));
        }

        public static T GetEnumMemberWithMember<T>(this string valor)
        {
            return Enum.GetValues(typeof(T)).OfType<T>().FirstOrDefault(f => f.ToString().Equals(valor));
        }

    }
}
