namespace SimpleAuthorizer.Common.Domain
{
    public abstract class Entity<TId>
    {
        protected Entity()
        {
        }

        protected Entity(TId id)
        {
            this.Id = id;
        }

        public TId Id { get; private set; } = default!;
    }
}
