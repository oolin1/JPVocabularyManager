namespace DatabaseHandler {
    public interface IModifiable {
        IEntity GetEntity(string identifier);
        void AddEntity(IEntity entity);
        void RemoveEntity(IEntity entity);
    }
}