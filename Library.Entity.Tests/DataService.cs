using Library.Tools.Debug;
using Library.Tools.Extensions;
using Library.Tools.Misc;
using Library.Tools.Tasks;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Core.Metadata.Edm;
using System.Collections.Generic;

namespace Library.Entity.Tests
{
    public class DataService : Repository
    {
        #region Public PROPERTIES

        public override DbContext RepositoryDBContext
        {
            get
            {
                if (_RepositoryDBContext == null)
                    _RepositoryDBContext = new EntityContext();
                return _RepositoryDBContext;
            }
        }

        #endregion

        #region Public CONSTRUCTORS

        public DataService()
            : base(true)
        { }

        #endregion

        #region Public METHODS

        public void ResetDatabase()
        {
            var objectContext = ((IObjectContextAdapter)RepositoryDBContext).ObjectContext;
            EntityContainer container = objectContext.MetadataWorkspace.GetEntityContainer(objectContext.DefaultContainerName,System.Data.Entity.Core.Metadata.Edm.DataSpace.CSpace);
            List<string> tableNames = (from meta in container.BaseEntitySets
                      where meta.BuiltInTypeKind == BuiltInTypeKind.EntitySet
                      select meta.Name).ToList<string>();

            foreach (var tableName in tableNames)
                RepositoryDBContext.Database.ExecuteSqlCommand(string.Format("DELETE FROM {0}", tableName));

            RepositoryDBContext.SaveChanges();
        }

        public void DeleteDatabase()
        {
            RepositoryDBContext.Database.Delete();
        }

        #endregion

        #region Private FIELDS

        private DbContext _RepositoryDBContext;

        #endregion
    }
}