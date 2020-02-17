using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace EmissorNfse.Module.Utils
{
    /// <summary>
    /// Classe com operações de Threads
    /// </summary>
    public static class ThreadUtils
    {
        /// <summary>
        /// Aguarda o tempo determinado para executar a função.
        /// </summary>
        /// <typeparam name="T">Tipo do Retorno.</typeparam>
        /// <param name="func">Função que deve ser executada.</param>
        /// <param name="waitTime">Temop de espera.</param>
        /// <returns>Instancia de [T].</returns>
        public static T WaitAndExecute<T>(Func<T> func, int waitTime)
        {
            Thread.Sleep(waitTime);
            return func();
        }
    }
}
