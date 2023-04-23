using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MvcApiPracticaChollos.Models
{
    public class Chollo
    {
        public int IdChollo { get; set; }

        public string Titulo { get; set; }

        public string Link { get; set; }

        public string Descripcion { get; set; }

        public DateTime Fecha { get; set; }

    }
}
