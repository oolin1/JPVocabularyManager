using Microsoft.EntityFrameworkCore;
using System;

namespace DatabaseHandler {
    public class DatabaseHandler<DatabaseContext> : IDisposable where DatabaseContext : DbContext, IModifiable {
        private readonly DatabaseContext dataBase;

        public DatabaseHandler(DatabaseContext dataBase) {
            this.dataBase = dataBase;
            this.dataBase.Database.EnsureCreated();
        }

        public IEntity GetEntity(string identifier) {
            try {
                return dataBase.GetEntity(identifier);
            }
            catch {
                return null;
            }
        }

        public bool AddOrUpdateEntity(IEntity entity) {
            try {
                if (!RemoveEntity(entity.GetIdentifier())) {
                    return false;
                }
                
                dataBase.AddEntity(entity);
                dataBase.SaveChanges();
                return true;
            }
            catch {
                return false;
            }
        }

        public bool RemoveEntity(string identifier) {
            try {
                IEntity foundEntity = GetEntity(identifier);
                if (foundEntity != null) {
                    dataBase.RemoveEntity(foundEntity);
                    dataBase.SaveChanges();
                }
                return true;
            }
            catch {
                return false;
            }
        }

        public virtual void Dispose() {
            dataBase.Dispose();
        }
    }
}