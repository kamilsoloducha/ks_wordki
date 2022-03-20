using Blueprints.Domain;

namespace Cards.Domain
{
    public readonly struct Language
    {
        public int Id { get; }

        private Language(int id)
        {
            Id = id;
        }

        public static Language Create(int id)
        {
            if (id < 0) throw new BuissnessArgumentException(nameof(id), id);

            return new Language(id);
        }
    }
}