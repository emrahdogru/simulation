using Simulation.Domain.Enums;
using Simulation.Domain.Items;
using Simulation.Domain.Items.Abstractions;
using Simulation.Domain.Items.Foods;

namespace Simulation.Domain
{
    public class Citizen
    {
        public Citizen(Section section, string name, int? age = null)
        {
            ArgumentNullException.ThrowIfNull(section, nameof(section));
            ArgumentNullException.ThrowIfNullOrWhiteSpace(name, nameof(name));
            Section = section;
            Name = name;
            BirthTime = age == null ? section.Game.Time : section.Game.Time.AddYears(-age.Value);
        }

        public static Citizen Random(Section section, int minAge, int maxAge)
        {
            ArgumentNullException.ThrowIfNull(section, nameof(section));
            var name = CitizenNames.GetRandomName();
            var age = CitizenNames.GetRandomAge(minAge, maxAge);
            return new Citizen(section, name, age);
        }

        public Section Section { get; private set; }
        public Game Game => Section.Game;

        public string Name { get; }
        public Date BirthTime { get; }
        public Date? DeathTime { get; private set; }
        public int Age => (Game.Time - BirthTime).Year;
        public CitizenGender Gender { get; }
        public bool IsAlive => !DeathTime.HasValue || DeathTime.Value <= Section.Game.Time;
        public Citizen? Partner { get; set; }
        public BuildingContainer? WorkPlace { get; set; }
        public Dictionary<Date, ItemCollection<IFood>> Consumption { get; } = [];

        public float Efficiency { get; } = 1f;

        public Dictionary<NutrientType, float> GetLast7DaysNutrients()
        {
            var nutrients = Enum.GetValues<NutrientType>().ToDictionary(x => x, x => 0f);
            var lastConsumptions = Consumption.Where(x => x.Key >= Game.Time.OnlyDate.AddDays(-7)).SelectMany(x => x.Value);
            foreach (var consumption in lastConsumptions)
            {
                foreach (var nutrient in consumption.Key.Nutrients)
                {
                    nutrients[nutrient.Key] += nutrient.Value * consumption.Value;
                }
            }
            return nutrients;
        }

        private (IFood? item, float amount) GetMostNitriusFoodInSection(NutrientType nutrientType)
        {
            var food = Section.Items
                .IsTypeOf<IFood>()
                .Where(x => x.Key.Nutrients.ContainsKey(nutrientType) && x.Value > 0)
                .OrderByDescending(x => x.Key.Nutrients[nutrientType])
                .FirstOrDefault(new KeyValuePair<IFood, float>(null!, 0));

            return (food.Key, food.Value);
        }

        private void Feed()
        {
            if (!IsAlive) return;

            if (Game.Random.Next(12) == 6)
            {
                // Feeding time!
                if (!Consumption.ContainsKey(Game.Time.OnlyDate))
                    Consumption[Game.Time.OnlyDate] = [];

                var nutrients = GetLast7DaysNutrients();

                foreach (var nutrient in nutrients.OrderBy(x => x.Value * (1 + (Game.Random.NextDouble() * 0.2 - 0.1))))
                {
                    float neededNutrientAmount = Convert.ToSingle((7 - nutrient.Value) * Game.Random.NextDouble());
                    if (neededNutrientAmount > 0)
                    {

                        var nutrientType = nutrient.Key;
                        var food = GetMostNitriusFoodInSection(nutrientType);

                        // If there isn't enough of this nutrient, consume carbohydrates instead.
                        if (nutrientType != NutrientType.Carbohydrate && nutrientType != NutrientType.Water && (food.item == null || food.amount <= 0))
                        {
                            // Check if we can consume carbohydrates instead.
                            if (nutrients[NutrientType.Carbohydrate] >= 25)
                                continue;

                            nutrientType = NutrientType.Carbohydrate;
                            food = GetMostNitriusFoodInSection(nutrientType);
                            neededNutrientAmount = 1;
                        }

                        if (food.item != null && food.amount > 0)
                        {
                            var amount = neededNutrientAmount / food.item.Nutrients.GetValueOrDefault(nutrientType);
                            Section.Items[food.item] -= amount;
                            Consumption[Game.Time.OnlyDate][food.item] = Consumption[Game.Time.OnlyDate].GetValueOrDefault(food.item, 0f) + amount;
                        }
                    }
                }
            }
        }

        private void Kill()
        {
            if (!IsAlive) return;
            if (Game.Random.Next(1000) == 6)
            {
                DeathTime = Game.Time;
                WorkPlace = null;
            }
        }

        internal void Process()
        {
            Kill();
            Feed();
        }
    }

    public enum CitizenGender
    {
        Male,
        Female
    }
}
