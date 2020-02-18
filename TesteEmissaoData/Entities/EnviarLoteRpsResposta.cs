using System;
using System.Collections.Generic;
using System.Text;
using TesteEmissaoData.Entities;

namespace TesteEmissaoData.Entities
{
    public class EnviarLoteRpsResposta
    {
        /*public string __invalid_name__@xmlns { get; set; }
        public string __invalid_name__@xmlns:pub { get; set; }
        public string __invalid_name__@xmlns:ds { get; set; }*/
        public ListaMensagemRetorno ListaMensagemRetorno { get; set; }
    }

    public class RootObject
    {
        public EnviarLoteRpsResposta EnviarLoteRpsResposta { get; set; }
    }
}
