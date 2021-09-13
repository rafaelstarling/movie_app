using MovieApplication.Domain.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MovieApplication.Infrastructure.Data
{
    public class DataGenerator
    {
        private readonly AwardContext _dbContext;

        public DataGenerator(AwardContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void ImportFromFile(string filename)
        {
            LineParser parser = null;
            int lineIndex = 0;

            try
            {
                foreach (var line in File.ReadAllLines(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, filename)))
                {
                    if (lineIndex == 0)
                    {
                        parser = new LineParser(line);
                        lineIndex++;
                        continue;
                    }

                    AwardNominee nominee = parser.ParseLine(line);
                    nominee.Producers = InsertOrGet(nominee.Producers);
                    nominee.Studios = InsertOrGet(nominee.Studios);
                    _dbContext.Add(nominee);

                    lineIndex++;
                }
            }
            catch (Exception e)
            {
                throw new Exception("Erro ao inicializar dados do BD", e);
            }
        }

        /// <summary>
        /// Insere cada produtor no banco de dados, ou recupera (para evitar duplicação), caso ja esteja cadastrado;
        /// Retorna uma lista com os novos produtores (com ids) já cadastrados no banco de dados;
        /// </summary>
        /// <param name="producers"></param>
        /// <returns></returns>
        private List<Producer> InsertOrGet(ICollection<Producer> producers)
        {
            var databaseInserted = new List<Producer>();

            foreach (var producer in producers)
            {
                var insertedProducer = _dbContext.Producers
                    .FirstOrDefault(p => p.Name == producer.Name);

                if (insertedProducer == null)
                {
                    insertedProducer = producer;
                    _dbContext.Add(insertedProducer);
                    _dbContext.SaveChanges();
                }

                databaseInserted.Add(insertedProducer);
            }

            return databaseInserted;
        }

        /// <summary>
        /// Insere cada estúdio no banco de dados, ou recupera (para evitar duplicação), caso ja esteja cadastrado;
        /// Retorna uma lista com os novos estúdios (com ids) já cadastrados no banco de dados;
        /// </summary>
        /// <param name="producers"></param>
        /// <returns></returns>
        private List<Studio> InsertOrGet(ICollection<Studio> studios)
        {
            var databaseInserted = new List<Studio>();

            foreach (var studio in studios)
            {
                var insertedStudio = _dbContext.Studios
                    .FirstOrDefault(s => s.Name == studio.Name);

                if (insertedStudio == null)
                {
                    insertedStudio = studio;
                    _dbContext.Add(insertedStudio);
                    _dbContext.SaveChanges();
                }

                databaseInserted.Add(insertedStudio);
            }

            return databaseInserted;
        }
    }
}
