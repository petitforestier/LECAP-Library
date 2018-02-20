using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.Tasks;
using LinqKit;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Library.Entity
{
    public abstract partial class Repository : ProgressableCancellable
    {
        #region Public PROPERTIES

        public abstract DbContext RepositoryDBContext { get; }

        #endregion

        #region Public METHODS

        public void FixEfProviderServicesProblem()
        {
            //The Entity Framework provider type 'System.Data.Entity.SqlServer.SqlProviderServices, EntityFramework.SqlServer'
            //for the 'System.Data.SqlClient' ADO.NET provider could not be loaded.
            //Make sure the provider assembly is available to the running application.
            //See http://go.microsoft.com/fwlink/?LinkId=260882 for more information.

            var instance = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        /// <summary>
        /// Ajouter une entité dans le dbcontext.
        /// </summary>
        public T Add<T>(T iEntity) where T : class, IEntity
        {
            Func<T> theTask = () =>
            {
                lock (OperationLocker)
                {
                    RepositoryDBContext.Entry(iEntity).State = EntityState.Added;
                    SaveChanges();
                    return iEntity;
                }
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Modifie une entité dans le dbcontext.
        /// </summary>
        public void Update<T>(T iEntity) where T : class, IEntity
        {
            Action theTask = () =>
            {
                lock (OperationLocker)
                {
                    RepositoryDBContext.Entry(iEntity).State = EntityState.Modified;
                    SaveChanges();
                }
            };

            RetryExecution(theTask);
        }

        /// <summary>
        /// Supprimer une entité dans le dbcontext.
        /// </summary>
        public void Delete<T>(T iEntity) where T : class, IEntity
        {
            Action theTask = () =>
            {
                lock (OperationLocker)
                {
                    RepositoryDBContext.Entry(iEntity).State = EntityState.Deleted;
                    SaveChanges();
                }
            };

            RetryExecution(theTask);
        }

        /// <summary>
        /// Ajouter une liste d'une même entité dans le dbcontext, avec optimisation des performances, et retour des eventuelles erreurs.
        /// </summary>
        public void AddList<T>(List<T> iEntityList) where T : class, IEntity
        {
            Action theTask = () =>
            {
                lock (OperationLocker)
                {
                    foreach (var entity in iEntityList)
                        RepositoryDBContext.Entry(entity).State = EntityState.Added;
                    SaveChanges();
                }
            };

            RetryExecution(theTask);
        }

        /// <summary>
        /// Supprimer une liste d'une même entité dans le dbcontext, avec optimisation des performances, et retour des eventuelles erreurs.
        /// </summary>
        public void DeleteList<T>(List<T> iEntityList) where T : class, IEntity
        {
            Action theTask = () =>
            {
                lock (OperationLocker)
                {
                    foreach (var entity in iEntityList)
                        RepositoryDBContext.Entry(entity).State = EntityState.Deleted;
                    SaveChanges();
                }
            };

            RetryExecution(theTask);
        }

        /// <summary>
        /// Modifier une liste d'une même entité dans le dbcontext, avec optimisation des performances, et retour des eventuelles erreurs.
        /// </summary>
        public void UpdateList<T>(List<T> iEntityList) where T : class, IEntity
        {
            Action theTask = () =>
            {
                lock (OperationLocker)
                {
                    foreach (var entity in iEntityList)
                        RepositoryDBContext.Entry(entity).State = EntityState.Modified;
                    SaveChanges();
                }
            };

            RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne une query.
        /// </summary>
        public IQueryable<T> GetQuery<T>(List<Expression<Func<T, object>>> iNavigationProperties) where T : class, IEntity
        {
            Func<IQueryable<T>> theTask = () =>
            {
                return GetQuery<T, T>(null, iNavigationProperties);
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne une liste chargée en mémoire.
        /// </summary>
        public IEnumerable<T> GetList<T>(
            Expression<Func<T, bool>> iWhere = null,
            List<Expression<Func<T, object>>> iNavigationProperties = null) where T : class, IEntity
        {
            Func<IEnumerable<T>> theTask = () =>
            {
                return GetQuery<T, object>(iWhere, iNavigationProperties).ToList();
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne une liste chargée en mémoire.
        /// </summary>
        public async Task<IEnumerable<T>> GetListAsync<T>(
            Expression<Func<T, bool>> iWhere = null,
            List<Expression<Func<T, object>>> iNavigationProperties = null) where T : class, IEntity
        {
            Func<Task<IEnumerable<T>>> theTask = async () =>
            {
                return await Task.Run(() => GetQuery<T, object>(iWhere, iNavigationProperties));
            };

            return await RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne une liste chargée en mémoire.
        /// </summary>
        public IEnumerable<T> GetList<T, TKey>(
            Expression<Func<T, bool>> iWhere = null,
            List<Expression<Func<T, object>>> iNavigationProperties = null,
            Expression<Func<T, TKey>> iOrderByProperty = null, ListSortDirection? iSortDirection = null, int? iSkip = null, int? iTake = null) where T : class, IEntity
        {
            Func<IEnumerable<T>> theTask = () =>
            {
                if (iOrderByProperty != null && iSortDirection == null) throw new ArgumentNullException();
                if (iTake != null && iOrderByProperty == null) throw new ArgumentNullException();
                if (iSkip != null && iOrderByProperty == null) throw new ArgumentNullException();

                return GetQuery<T, TKey>(iWhere, iNavigationProperties, iOrderByProperty, iSortDirection, iSkip, iTake).ToList();
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne une liste chargée en mémoire.
        /// </summary>
        public async Task<IEnumerable<T>> GetListAsync<T, TKey>(
            Expression<Func<T, bool>> iWhere = null,
            List<Expression<Func<T, object>>> iNavigationProperties = null,
            Expression<Func<T, TKey>> iOrderByProperty = null, ListSortDirection? iSortDirection = null, int? iSkip = null, int? iTake = null) where T : class, IEntity
        {
            Func<Task<IEnumerable<T>>> theTask = async () =>
            {
                if (iOrderByProperty != null && iSortDirection == null) throw new ArgumentNullException();
                if (iTake != null && iOrderByProperty == null) throw new ArgumentNullException();
                if (iSkip != null && iOrderByProperty == null) throw new ArgumentNullException();

                return await Task.Run(() => GetQuery<T, TKey>(iWhere, iNavigationProperties, iOrderByProperty, iSortDirection, iSkip, iTake));
            };

            return await RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne une liste chargée en mémoire.
        /// </summary>
        public IEnumerable<T> GetListBatchLoading<T, TKey>(
            Expression<Func<T, TKey>> iOrderByProperty, ListSortDirection iSortDirection,
            List<Expression<Func<T, object>>> iNavigationProperties = null,
            Expression<Func<T, bool>> iWhere = null,
            int? iSkip = null, int? iTake = null) where T : class, IEntity
        {
            Func<IEnumerable<T>> theTask = () =>
            {
                if (iOrderByProperty == null) throw new ArgumentNullException();
                if (iTake != null && iOrderByProperty == null) throw new ArgumentNullException();
                if (iSkip != null && iOrderByProperty == null) throw new ArgumentNullException();

                //batch loading
                Int64 entitiesCount = GetQuery<T, TKey>(iWhere, iNavigationProperties, iOrderByProperty, iSortDirection, iSkip, iTake).Count();
                int totalPage = (int)Math.Ceiling(decimal.Divide(entitiesCount, TAKECOUNT));

                //query
                var result = new List<T>();

                for (int a = 0; a < totalPage; a++)
                {
                    Notifier.ThrowIfCancellationRequested();
                    result.AddRange(GetQuery<T, TKey>(iWhere, iNavigationProperties, iOrderByProperty, iSortDirection, iSkip, iTake).Skip(TAKECOUNT * (a)).Take(TAKECOUNT).ToList());
                    MyDebug.PrintInformation("Chargement des données page par page", a + 1, totalPage);
                }

                return result;
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne une liste chargée en mémoire.
        /// </summary>
        public async Task<IEnumerable<T>> GetListBatchLoadingAsync<T, TKey>(
            Expression<Func<T, TKey>> iOrderByProperty, ListSortDirection iSortDirection,
            List<Expression<Func<T, object>>> iNavigationProperties = null,
            Expression<Func<T, bool>> iWhere = null,
            int? iSkip = null, int? iTake = null) where T : class, IEntity
        {
            Func<Task<IEnumerable<T>>> theTask = async () =>
            {
                return await Task.Run(() => GetListBatchLoading<T, TKey>(iOrderByProperty, iSortDirection, iNavigationProperties, iWhere, iSkip, iTake));
            };

            return await RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne s'il existe une ou plusieurs entités correspondant à la clause
        /// </summary>
        public bool Any<T>(Expression<Func<T, bool>> iWhere, List<Expression<Func<T, object>>> iNavigationProperties = null) where T : class, IEntity
        {
            Func<bool> theTask = () =>
            {
                IQueryable<T> dbQuery = RepositoryDBContext.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in iNavigationProperties.Enum())
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                return dbQuery
                    .AsNoTracking()
                    .Any(iWhere);
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne l'entité repondant à la clause avec le chargement des propriétés demandés.
        /// </summary>
        public T GetSingleOrDefault<T>(Expression<Func<T, bool>> iWhere, List<Expression<Func<T, object>>> iNavigationProperties = null) where T : class, IEntity
        {
            Func<T> theTask = () =>
            {
                IQueryable<T> dbQuery = RepositoryDBContext.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in iNavigationProperties.Enum())
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                return dbQuery
                    .AsNoTracking()
                    .SingleOrDefault(iWhere);
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne l'entité repondant à la clause avec le chargement des propriétés demandés.
        /// </summary>
        public async Task<T> GetSingleOrDefaultAsync<T>(Expression<Func<T, bool>> iWhere, List<Expression<Func<T, object>>> iNavigationProperties = null) where T : class, IEntity
        {
            Func<Task<T>> theTask = async () =>
            {
                return await Task.Run(() => GetSingleOrDefault<T>(iWhere, iNavigationProperties));
            };

            return await RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne l'entité repondant à la clause avec le chargement des propriétés demandés.
        /// </summary>
        public T GetSingle<T>(Expression<Func<T, bool>> iWhere, List<Expression<Func<T, object>>> iNavigationProperties = null) where T : class, IEntity
        {
            Func<T> theTask = () =>
            {
                IQueryable<T> dbQuery = RepositoryDBContext.Set<T>();

                //Apply eager loading
                foreach (Expression<Func<T, object>> navigationProperty in iNavigationProperties.Enum())
                    dbQuery = dbQuery.Include<T, object>(navigationProperty);

                return dbQuery
                    .AsNoTracking()
                    .Single(iWhere);
            };

            return RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne l'entité repondant à la clause avec le chargement des propriétés demandés.
        /// </summary>
        public async Task<T> GetSingleAsync<T>(Expression<Func<T, bool>> iWhere, List<Expression<Func<T, object>>> iNavigationProperties = null) where T : class, IEntity
        {
            Func<Task<T>> theTask = async () =>
            {
                return await Task.Run(() => GetSingle<T>(iWhere, iNavigationProperties));
            };

            return await RetryExecution(theTask);
        }

        /// <summary>
        /// Retourne si la base de données est disponible ou non
        /// </summary>
        public bool IsConnected()
        {
            try
            {
                return ((IMyDbContext)RepositoryDBContext).IsConnected();
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Retourne la date et heure du serveur
        /// </summary>
        /// <returns></returns>
        public DateTime GetCurrentDateTime()
        {
            Func<DateTime> theTask = () =>
            {
                return RepositoryDBContext.Database.SqlQuery<DateTime>("SELECT CURRENT_TIMESTAMP").AsEnumerable().FirstOrDefault();
            };

            return RetryExecution(theTask);
        }

        #endregion

        #region Protected CONSTRUCTORS

        protected Repository(ProgressCancelNotifier iNotifier, bool iLazyLoading)
            : base(iNotifier)
        {
            SetEntityContext(iLazyLoading);
        }


        protected Repository(bool iLazyLoading, string iConnectionString)
            : base(new ProgressCancelNotifier())
        {
            if (iConnectionString.IsNullOrEmpty())
                throw new Exception("La chaine de connexion ne peut pas être nulle");

            _ConnectionString = iConnectionString;
            SetEntityContext(iLazyLoading);
        }

        protected Repository(bool iLazyLoading)
            : base(new ProgressCancelNotifier())
        {
            SetEntityContext(iLazyLoading);
        }

        private void SetEntityContext(bool iLazyLoading)
        {
            var type = typeof(System.Data.Entity.SqlServer.SqlProviderServices);
            RepositoryDBContext.Configuration.AutoDetectChangesEnabled = false;
            RepositoryDBContext.Configuration.LazyLoadingEnabled = iLazyLoading;
            RepositoryDBContext.Configuration.ProxyCreationEnabled = false;
            RepositoryDBContext.Configuration.ValidateOnSaveEnabled = false;
            RepositoryDBContext.Database.CommandTimeout = 5 * 60;

        }
        protected string _ConnectionString;


        #endregion

        #region Private FIELDS

        private const int TAKECOUNT = 1000;

        private const int MAXRETRY = 5;

        private static object OperationLocker = new object();

        #endregion

        #region Private METHODS

        private IQueryable<T> GetQuery<T, TKey>(
                    Expression<Func<T, bool>> iWhere = null,
                    List<Expression<Func<T, object>>> iNavigationProperties = null,
                    Expression<Func<T, TKey>> iOrderByProperty = null, ListSortDirection? iSortDirection = null, int? iSkip = null, int? iTake = null) where T : class
        {
            if (iOrderByProperty != null && iSortDirection == null) throw new ArgumentNullException();
            if (iOrderByProperty == null && iSortDirection != null) throw new ArgumentNullException();
            if (iTake != null && iOrderByProperty == null) throw new ArgumentNullException();
            if (iSkip != null && iOrderByProperty == null) throw new ArgumentNullException();

            IQueryable<T> theQueryable = RepositoryDBContext.Set<T>();

            //Eager loading
            foreach (Expression<Func<T, object>> navigationProperty in iNavigationProperties.Enum())
                theQueryable = theQueryable.Include<T, object>(navigationProperty);

            theQueryable = theQueryable.AsNoTracking().AsExpandable();

            //Where clause
            if (iWhere != null)
                theQueryable = theQueryable.Where(iWhere);

            //Orderby
            if (iOrderByProperty != null)
            {
                if (iSortDirection == ListSortDirection.Ascending)
                    theQueryable = theQueryable.AsQueryable().OrderBy(iOrderByProperty);
                else if (iSortDirection == ListSortDirection.Descending)
                    theQueryable = theQueryable.AsQueryable().OrderByDescending(iOrderByProperty);

                //Pagination
                if (iSkip != null)
                    theQueryable = ((IOrderedQueryable<T>)theQueryable).Skip((int)iSkip).AsQueryable();
                if (iTake != null)
                    theQueryable = ((IOrderedQueryable<T>)theQueryable).Take((int)iTake).AsQueryable();
            }

            return theQueryable;
        }

        private void RollBack()
        {
            var context = RepositoryDBContext;
            var changedEntries = context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
                entry.State = EntityState.Detached;

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
                entry.State = EntityState.Unchanged;

            SaveChanges();
        }

        private async Task RollBackAsync()
        {
            var context = RepositoryDBContext;
            var changedEntries = context.ChangeTracker.Entries().Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Modified))
            {
                entry.CurrentValues.SetValues(entry.OriginalValues);
                entry.State = EntityState.Unchanged;
            }

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Added))
                entry.State = EntityState.Detached;

            foreach (var entry in changedEntries.Where(x => x.State == EntityState.Deleted))
                entry.State = EntityState.Unchanged;

            await SaveChangesAsync();
        }

        private void SaveChanges()
        {
            try
            {
                try
                {
                    RepositoryDBContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    string entryList = string.Empty;
                    foreach (var entryItem in RepositoryDBContext.ChangeTracker.Entries().Enum())
                        entryList = entryList.AddLine(entryItem.Entity.ToString());
                    RollBack();
                    throw new Exception("Erreur à la sauvegarde des entrées : " + ex.Message + Environment.NewLine + entryList, ex);
                }
            }
            finally
            {
                foreach (var entry in RepositoryDBContext.ChangeTracker.Entries().Enum())
                    entry.State = EntityState.Detached;
            }
        }

        private async Task SaveChangesAsync()
        {
            try
            {
                try
                {
                    await RepositoryDBContext.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    string entryList = string.Empty;
                    foreach (var entryItem in RepositoryDBContext.ChangeTracker.Entries().Enum())
                        entryList = entryList.AddLine(entryItem.Entity.ToString());
                    RollBack();
                    throw new Exception("Erreur à la sauvegarde des entrées : " + ex.Message + Environment.NewLine + entryList, ex);
                }
            }
            finally
            {
                foreach (var entry in RepositoryDBContext.ChangeTracker.Entries().Enum())
                    entry.State = EntityState.Detached;
            }
        }

        private void RetryExecution(Action iAction)
        {
            RetryExecution(() => { iAction(); return 0; });
        }

        private T RetryExecution<T>(Func<T> iAction)
        {
            decimal waitingSecondTime = 1;
            Exception theError = null;

            for (int tryCounter = 1; tryCounter <= MAXRETRY; tryCounter++)
            {
                Notifier.ThrowIfCancellationRequested();
                try
                {
                    return iAction();
                }
                catch (Exception ex)
                {
                    if (Notifier.IsCanceled)
                        throw;

                    System.Threading.Thread.Sleep(Convert.ToInt32(waitingSecondTime * 1000));
                    waitingSecondTime = waitingSecondTime * 1.2m;
                    theError = ex;
                }
            }
            throw theError;
        }

        #endregion
    }

    public partial class Repository : IDisposable
    {
        #region Public METHODS

        public void Dispose()
        {
            if (RepositoryDBContext != null)
                RepositoryDBContext.Dispose();

            //Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region Protected METHODS

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here.
                if (RepositoryDBContext != null)
                    RepositoryDBContext.Dispose();
            }

            // Free any unmanaged objects here.

            disposed = true;
        }

        #endregion

        #region Private FIELDS

        private bool disposed = false;

        #endregion
    }
}