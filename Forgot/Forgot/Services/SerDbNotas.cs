using System;
using System.Collections.Generic;
using System.Text;

using SQLite;
using Forgot.Models;

namespace Forgot.Services
{
    public class SerDbNotas
    {
        SQLiteConnection conn;

        public string StatusMessage { get; set; }

        public SerDbNotas(string dbPath)
        {
            if (dbPath == "")
                dbPath = App.DbPath;

            conn = new SQLiteConnection(dbPath);

            conn.CreateTable<ModNotas>();
        }

        public void Inserir(ModNotas nota)
        {
            try
            {
                if (string.IsNullOrEmpty(nota.titulo))
                    throw new Exception("Título da anotação não informado!");

                if (string.IsNullOrEmpty(nota.dados))
                    throw new Exception("Dados da anotação não informados!");

                int result = conn.Insert(nota);

                this.StatusMessage = result != 0 ? string.Format("Anotação salva: [Nota: {0}]", nota.titulo) : string.Format("Ocorreu um erro!\n\nTente novamente!");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<ModNotas> Listar()
        {
            List<ModNotas> lista = new List<ModNotas>();

            try
            {
                lista = conn.Table<ModNotas>().ToList();

                this.StatusMessage = "Listagem das anotações";

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Alterar(ModNotas nota)
        {
            try
            {
                if (string.IsNullOrEmpty(nota.titulo))
                    throw new Exception("Título da anotação não informado!");

                if (string.IsNullOrEmpty(nota.dados))
                    throw new Exception("Dados da anotação não informados!");
                if (nota.id <= 0)
                    throw new Exception("Id da anotação não informado!");

                int result = conn.Update(nota);

                this.StatusMessage = result != 0 ? string.Format("Anotação alterada: [Nota: {0}]", nota.titulo) : string.Format("Ocorreu um erro!\n\nTente novamente!");
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public void Excluir(int id)
        {
            try
            {
                int result = conn.Table<ModNotas>().Delete(r => r.id == id); // r vai receber o id do ModNotas quando este id for igual ao parâmetro id, logo o registro excluído será o de Id = 2

                StatusMessage = result == 1 ? string.Format("{0} registro excluído!", result) : string.Format("{0} registros excluídos!", result);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public List<ModNotas> Localizar(string titulo)
        {
            try
            {
                var response = from p in conn.Table<ModNotas>() 
                               where p.titulo.ToLower().Contains(titulo.ToLower()) 
                               select p; // O select está no fim, pois a expressão deve ser ulgada primeiro
                /* Equivalente a: 
                    SELECT * 
                    FROM anotacoes
                    WHERE LOWER(titulo) LIKE '%' || LOWER(@titulo) || '%'*/
                // Retornará todos os registros com a ocorrencia do valor passado, não importando a posição
                return response.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public List<ModNotas> Localizar(string titulo, Boolean favorito)
        {
            try
            {
                var response = from p in conn.Table<ModNotas>() 
                               where p.titulo.ToLower().Contains(titulo.ToLower()) && p.favorito == favorito 
                               select p; // O select está no fim, pois a expressão deve ser ulgada primeiro
                /* Equivalente a: 
                    SELECT * 
                    FROM anotacoes
                    WHERE LOWER(titulo) LIKE '%' || LOWER(@titulo) || '%'
                    AND favorito = @favorito*/
                // Retornará todos os registros com a ocorrencia do valor passado, não importando a posição, quando este registro for favorito
                return response.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }

        public ModNotas GetNota(int id)
        {
            ModNotas m = new ModNotas();

            try
            {
                m = conn.Table<ModNotas>().First(n => n.id == id);

                StatusMessage = "Anotação encontrada!";

                return m;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Erro: {0}", ex.Message));
            }
        }
    }
}
