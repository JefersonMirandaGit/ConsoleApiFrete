using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApiFrete
{
    public class RetornoPostFrete
    {
        public int Status { get; set; }
        public string Mensagem { get; set; }
        public int Id { get; set; }
    }

    public class PostFrete : ModelPadrao
    {
        public RetornoPostFrete Retorno { get; set; }
    }
}
