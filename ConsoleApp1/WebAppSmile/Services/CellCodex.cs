using WebAppSmile.Models;

namespace WebAppSmile.Services;

public static class CellCodex
{
    private static readonly IReadOnlyList<CellTypeInfo> Entries =
    [
        new() { TypeKey = "Ground", TitleRu = "Земля", Category = "Местность", Teaser = "Обычный пол подземелья — по нему можно ходить свободно." },
        new() { TypeKey = "Wall", TitleRu = "Стена", Category = "Местность", Teaser = "Каменная преграда. Сквозь неё не пройти." },
        new() { TypeKey = "Dirt", TitleRu = "Грязь", Category = "Местность", Teaser = "Трясина под ногами — следы остаются дольше обычного." },
        new() { TypeKey = "Ice", TitleRu = "Лёд", Category = "Местность", Teaser = "Скользкая плита: шаг может унести вас дальше." },
        new() { TypeKey = "Coin", TitleRu = "Монета", Category = "Добыча", Teaser = "Золото подземелья. Подберите — и счётчик монет вырастет." },
        new() { TypeKey = "Diamond", TitleRu = "Алмаз", Category = "Добыча", Teaser = "Редкий камень. При удаче принесёт солидный куш." },
        new() { TypeKey = "Flower", TitleRu = "Цветок", Category = "Добыча", Teaser = "Хрупкий бутон. Собирайте — пригодится в инвентаре." },
        new() { TypeKey = "PileOfSand", TitleRu = "Куча песка", Category = "Добыча", Teaser = "Горсть песка. Можно унести с собой." },
        new() { TypeKey = "HealthPotion", TitleRu = "Зелье здоровья", Category = "Добыча", Teaser = "Флакон с красной жидкостью — запас для трудного часа." },
        new() { TypeKey = "Tree", TitleRu = "Дерево", Category = "Опасность", Teaser = "Живое древо: шагните — и окажетесь в другом месте.", Link = "Tree"},
        new() { TypeKey = "Snake", TitleRu = "Змея", Category = "Опасность", Teaser = "Ползучий гость. Жалит и ползёт дальше по коридорам." },
        new() { TypeKey = "Thief", TitleRu = "Вор", Category = "Опасность", Teaser = "Тень в капюшоне. Любит чужие монеты." },
        new() { TypeKey = "Amongus", TitleRu = "Among Us", Category = "Опасность", Teaser = "Подозрительный гость. Лучше держаться настороже." },
        new() { TypeKey = "MimicChest", TitleRu = "Сундук-мимик", Category = "Опасность", Teaser = "Сундук… или нет? Удача решит, сокровище это или укус." },
        new() { TypeKey = "Crater", TitleRu = "Яма", Category = "Опасность", Teaser = "Провал в полу. Падение больно бьёт по здоровью." },
        new() { TypeKey = "Portal", TitleRu = "Портал", Category = "Особое", Teaser = "Фиолетовый разлом — телепорт в другую точку лабиринта." },
        new() { TypeKey = "Rainbow", TitleRu = "Радуга", Category = "Особое", Teaser = "Разноцветная арка с неожиданным эффектом." },
        new() { TypeKey = "PaidDoor", TitleRu = "Платная дверь", Category = "Особое", Teaser = "Дверь за монеты. Без платы не откроется." },
        new() { TypeKey = "VodkaBar", TitleRu = "Водочный бар", Category = "Особое", Teaser = "Стойка с напитком. После визита мир слегка плывёт." },
    ];

    public static IReadOnlyList<CellTypeInfo> All => Entries;

    public static CellTypeInfo? Find(string? typeKey)
    {
        if (string.IsNullOrWhiteSpace(typeKey))
        {
            return null;
        }

        return Entries.FirstOrDefault(e =>
            string.Equals(e.TypeKey, typeKey.Trim(), StringComparison.OrdinalIgnoreCase));
    }
}
