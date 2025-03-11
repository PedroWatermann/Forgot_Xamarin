using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Forgot.Models
{
    [Table("Anotacoes")] // Anotação de banco

    public class ModNotas
    {
        [PrimaryKey, AutoIncrement]
        public int id { get; set; }

        [NotNull]
        public String titulo{ get; set; }

        [NotNull]
        public String dados { get; set; }

        [NotNull]
        public Boolean favorito { get; set; }

        public ModNotas()
        {
            this.id = 0;
            this.titulo = "";
            this.dados = "";
            this.favorito = false;
        }
    }
}
