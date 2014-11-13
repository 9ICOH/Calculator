using System;
using System.Data.Entity;
using System.Linq;

namespace Calculator.Data
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        protected readonly bool ShareContext;

        private readonly DbContext context;

        public EfRepository(DbContext context)
            : this(context, false)
        {
        }

        public EfRepository(DbContext context, bool sharedContext)
        {
            this.context = context;
            this.ShareContext = sharedContext;
        }

        protected DbSet<T> DbSet
        {
            get { return this.context.Set<T>(); }
        }

        public IQueryable<T> All()
        {
            return this.DbSet.AsQueryable();
        }

        public bool Any(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Any(predicate);
        }

        public int Count
        {
            get { return this.DbSet.Count(); }
        }

        public T Create(T t)
        {
            this.DbSet.Add(t);
            if (!this.ShareContext)
            {
                this.context.SaveChanges();
            }

            return t;
        }

        public int Delete(T t)
        {
            this.DbSet.Remove(t);
            if (!this.ShareContext)
            {
                return this.context.SaveChanges();
            }

            return 0;
        }

        public int Delete(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            var records = this.FindAll(predicate);
            foreach (var record in records)
            {
                this.DbSet.Remove(record);
            }

            if (!this.ShareContext)
            {
                return this.context.SaveChanges();
            }

            return 0;
        }

        public T Find(params object[] keys)
        {
            return this.DbSet.Find(keys);
        }

        public T Find(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.SingleOrDefault(predicate);
        }

        public IQueryable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return this.DbSet.Where(predicate).AsQueryable();
        }

        public IQueryable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> predicate, int index, int size)
        {
            var skip = index * size;
            IQueryable<T> query = this.DbSet;

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (skip != 0)
            {
                query = query.Skip(skip);
            }

            return query.Take(size).AsQueryable();
        }

        public int Update(T t)
        {
            var entry = this.context.Entry(t);
            this.DbSet.Attach(t);

            entry.State = EntityState.Modified;

            if (!this.ShareContext)
            {
                return this.context.SaveChanges();
            }

            return 0;
        }

        public void Dispose()
        {
            if (!this.ShareContext && this.context != null)
            {
              //  try
              //  {
                    this.context.Dispose();
             //   }
             //   catch { }

            }
        }
    }
}