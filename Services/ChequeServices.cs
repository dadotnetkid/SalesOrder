using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Services
{
    public class ChequeServices : IChequeService
    {
        private ModelDb db;

        public ChequeServices(ModelDb db)
        {
            this.db = db;
        }
        public List<Cheques> Get(Func<Cheques, bool> filter = null, string includeProperties = null)
        {
            IQueryable<Cheques> cheques = db.Cheques;
            if (includeProperties != null)
                cheques = cheques.Include(includeProperties);
            if (filter != null)
                return cheques.Where(filter).ToList();
            return cheques.ToList();

        }

        public Cheques Find(Func<Cheques, bool> filter = null, string includeProperties = null)
        {
            IQueryable<Cheques> cheques = db.Cheques;
            if (includeProperties != null)
                cheques = cheques.Include(includeProperties);
            if (filter != null)
                return cheques.FirstOrDefault(filter);
            return cheques.FirstOrDefault();
        }

        public Cheques Insert(Cheques model)
        {
            model.Id = Guid.NewGuid().ToString();
            db.Cheques.Add(model);
            db.SaveChanges();
            return model;
        }

        public Cheques Update(Cheques model)
        {
            db.Cheques.Update(model);
            db.SaveChanges();
            return model;
        }

        public int Delete(Func<Cheques, bool> filter = null)
        {
            db.Cheques.Remove(db.Cheques.FirstOrDefault(filter));
            db.SaveChanges();
            return db.SaveChanges();
        }
    }
}
