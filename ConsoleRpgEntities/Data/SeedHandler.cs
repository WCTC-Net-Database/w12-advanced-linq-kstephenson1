using ConsoleRpgEntities.DataTypes;
using ConsoleRpgEntities.Models.Abilities;
using ConsoleRpgEntities.Models.Combat;
using ConsoleRpgEntities.Models.Dungeons;
using ConsoleRpgEntities.Models.Items;
using ConsoleRpgEntities.Models.Items.ConsumableItems;
using ConsoleRpgEntities.Models.Items.EquippableItems.ArmorItems;
using ConsoleRpgEntities.Models.Items.EquippableItems.WeaponItems;
using ConsoleRpgEntities.Models.Rooms;
using ConsoleRpgEntities.Models.Units.Abstracts;
using ConsoleRpgEntities.Models.Units.Characters;
using ConsoleRpgEntities.Models.Units.Monsters;
using ConsoleRpgEntities.Services;
using Spectre.Console;

namespace ConsoleRpgEntities.Data;

public class SeedHandler
{
    private GameContext _db;
    private RoomFactory _roomFactory;
    private List<Room> _rooms = new();

    private FlyAbility _abilityFly = new();
    private HealAbility _abilityHeal = new();
    private StealAbility _abilitySteal = new();
    private TauntAbility _abilityTaunt = new();

    private ItemPotion _itemPotion;
    private ItemBook _itemBook;
    private ItemLockpick _itemLockpick;

    private PhysicalWeaponItem _itemSword;
    private PhysicalWeaponItem _itemAxe;
    private PhysicalWeaponItem _itemDagger;
    private PhysicalWeaponItem _itemBow;
    private PhysicalWeaponItem _itemStaff;
    private PhysicalWeaponItem _itemMace;

    private MagicWeaponItem _itemFire;
    private MagicWeaponItem _itemIce;
    private MagicWeaponItem _itemLightning;
    private MagicWeaponItem _itemDecay;
    private MagicWeaponItem _itemSmite;

    private HeadArmorItem _itemHood;
    private ChestArmorItem _itemShirt;
    private ChestArmorItem _itemCloak;
    private LegArmorItem _itemPants;
    private FeetArmorItem _itemShoes;

    private HeadArmorItem _itemCap;
    private ChestArmorItem _itemTunic;
    private LegArmorItem _itemStuddedPants;
    private FeetArmorItem _itemBoots;

    private HeadArmorItem _itemHelm;
    private ChestArmorItem _itemPlate;
    private LegArmorItem _itemGreaves;
    private FeetArmorItem _itemSabatons;

    public SeedHandler(GameContext context, RoomFactory roomFactory)
    {
        _db = context;
        _roomFactory = roomFactory;
    }

    public void SeedDatabase()
    {
        // Checks if the database is empty before seeding. If it is empty, it generates the items, dungeons, abilities,
        // and characters. If the database is not empty, it skips the seeding process. A cute 'lil loading screen was
        // added to make it look like the game is loading.
        if (!_db.Items.Any())
        {
            DisplaySeedProgressBar();
            GenerateItems();
        }

        if (!_db.Dungeons.Any())
            GenerateDungeons();

        if (!_db.Abilities.Any())
            GenerateAbilities();

        if (!_db.Units.Any())
            GenerateCharacters();

        // Saves the changes to the database after seeding.
        _db.SaveChanges();
    }

    private void GenerateItems()
    {
        // Generates the items for the game. The items are added to the database and saved.

        // Consumable Items
        _itemPotion = new();
        _itemLockpick = new();
        _itemBook = new();

        //Physical Weapons
        _itemSword = new()
        {
            Name = "Sword",
            Description = "A basic sword.",
            WeaponType = WeaponType.Sword,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 5,
            Hit = 80,
            Crit = 0,
            Range = 1,
            Weight = 4,
            ExpModifier = 1,
        };

        _itemAxe = new()
        {
            Name = "Axe",
            Description = "A basic axe",
            WeaponType = WeaponType.Axe,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 7,
            Hit = 70,
            Crit = 0,
            Range = 1,
            Weight = 6,
            ExpModifier = 1,
        };

        _itemDagger = new()
        {
            Name = "Dagger",
            Description = "A basic dagger",
            WeaponType = WeaponType.Sword,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 4,
            Hit = 100,
            Crit = 10,
            Range = 1,
            Weight = 2,
            ExpModifier = 1,
        };

        _itemBow = new()
        {
            Name = "Bow",
            Description = "A basic bow",
            WeaponType = WeaponType.Bow,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 7,
            Hit = 75,
            Crit = 0,
            Range = 2,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemStaff = new()
        {
            Name = "Staff",
            Description = "A basic staff",
            WeaponType = WeaponType.Lance,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 6,
            Hit = 85,
            Crit = 0,
            Range = 1,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemMace = new()
        {
            Name = "Mace",
            Description = "A basic mace",
            WeaponType = WeaponType.Axe,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 8,
            Hit = 70,
            Crit = 0,
            Range = 1,
            Weight = 5,
            ExpModifier = 1,
        };

        // Magic Weapons

        _itemFire = new()
        {
            Name = "Fire",
            Description = "A basic fire spell",
            WeaponType = WeaponType.Elemental,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 6,
            Hit = 80,
            Crit = 0,
            Range = 2,
            Weight = 4,
            ExpModifier = 1,
        };

        _itemIce = new()
        {
            Name = "Ice",
            Description = "A basic ice spell",
            WeaponType = WeaponType.Elemental,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 6,
            Hit = 80,
            Crit = 0,
            Range = 2,
            Weight = 4,
            ExpModifier = 1,
        };

        _itemLightning = new()
        {
            Name = "Lightning",
            Description = "A basic lightning spell",
            WeaponType = WeaponType.Elemental,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 6,
            Hit = 80,
            Crit = 0,
            Range = 2,
            Weight = 4,
            ExpModifier = 1,
        };

        _itemDecay = new()
        {
            Name = "Decay",
            Description = "A basic decay spell",
            WeaponType = WeaponType.Dark,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 7,
            Hit = 70,
            Crit = 0,
            Range = 2,
            Weight = 4,
            ExpModifier = 1,
        };

        _itemSmite = new()
        {
            Name = "Smite",
            Description = "A basic smite spell",
            WeaponType = WeaponType.Light,
            RequiredRank = Rank.E,
            MaxDurability = 45,
            Durability = 45,
            Might = 5,
            Hit = 90,
            Crit = 10,
            Range = 2,
            Weight = 4,
            ExpModifier = 1,
        };

        // Armor Items
        _itemHood = new()
        {
            Name = "Hood",
            Description = "A basic hood",
            ArmorType = ArmorType.Head,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 0,
            Resistance = 2,
            Weight = 1,
            ExpModifier = 1,
        };

        _itemShirt = new()
        {
            Name = "Shirt",
            Description = "A basic shirt",
            ArmorType = ArmorType.Chest,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 0,
            Resistance = 2,
            Weight = 1,
            ExpModifier = 1,
        };

        _itemCloak = new()
        {
            Name = "Cloak",
            Description = "A basic cloak",
            ArmorType = ArmorType.Chest,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 0,
            Resistance = 2,
            Weight = 1,
            ExpModifier = 1,
        };

        _itemPants = new()
        {
            Name = "Pants",
            Description = "A basic pants",
            ArmorType = ArmorType.Legs,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 0,
            Resistance = 2,
            Weight = 1,
            ExpModifier = 1,
        };

        _itemShoes = new()
        {
            Name = "Shoes",
            Description = "A basic shoes",
            ArmorType = ArmorType.Feet,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 0,
            Resistance = 2,
            Weight = 1,
            ExpModifier = 1,
        };

        _itemCap = new()
        {
            Name = "Leather Cap",
            Description = "A basic leather cap",
            ArmorType = ArmorType.Head,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 1,
            Resistance = 1,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemTunic = new()
        {
            Name = "Leather Tunic",
            Description = "A basic leather tunic",
            ArmorType = ArmorType.Chest,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 1,
            Resistance = 1,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemStuddedPants = new()
        {
            Name = "Studded Pants",
            Description = "A basic studded pants",
            ArmorType = ArmorType.Legs,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 1,
            Resistance = 1,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemBoots = new()
        {
            Name = "Leather Boots",
            Description = "A basic leather boots",
            ArmorType = ArmorType.Feet,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 1,
            Resistance = 1,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemHelm = new()
        {
            Name = "Helm",
            Description = "A basic plate helm",
            ArmorType = ArmorType.Head,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 2,
            Resistance = 0,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemPlate = new()
        {
            Name = "Plate Armor",
            Description = "A basic plate armor",
            ArmorType = ArmorType.Chest,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 2,
            Resistance = 0,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemGreaves = new()
        {
            Name = "Greaves",
            Description = "A basic plate greaves",
            ArmorType = ArmorType.Legs,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 2,
            Resistance = 0,
            Weight = 3,
            ExpModifier = 1,
        };

        _itemSabatons = new()
        {
            Name = "Sabatons",
            Description = "A basic plate sabatons",
            ArmorType = ArmorType.Feet,
            RequiredRank = Rank.E,
            MaxDurability = 30,
            Durability = 30,
            Defense = 2,
            Resistance = 0,
            Weight = 3,
            ExpModifier = 1,
        };

        _db.Items.AddRange(_itemPotion, _itemLockpick, _itemBook,
            _itemSword, _itemAxe, _itemDagger, _itemBow, _itemStaff, _itemMace,
            _itemFire, _itemIce, _itemLightning, _itemDecay, _itemSmite,
            _itemHood, _itemShirt, _itemCloak, _itemPants, _itemShoes,
            _itemCap, _itemTunic, _itemStuddedPants, _itemBoots,
            _itemHelm, _itemPlate, _itemGreaves, _itemSabatons);

        _db.SaveChanges();
    }
    private void GenerateCharacters()
    {
        List<Item> items = _db.Items.ToList();

        Unit unit = new Fighter();
        unit.Name = "John, Brave";
        unit.Class = "Fighter";
        unit.Level = 1;
        AddItem(unit, _itemSword, EquipmentSlot.Weapon);
        AddItem(unit, _itemTunic, EquipmentSlot.Chest);
        AddItem(unit, _itemPotion);
        unit.Stat = new Stat
        {
            HitPoints = 28,
            MaxHitPoints = 28,
            Movement = 5,
            Constitution = 7,
            Strength = 10,
            Magic = 6,
            Dexterity = 7,
            Speed = 7,
            Luck = 8,
            Defense = 6,
            Resistance = 2
        };
        unit.CurrentRoom = GetRandomRoom();

        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);


        unit = new Wizard();
        unit.Name = "Jane";
        unit.Class = "Wizard";
        unit.Level = 2;
        AddItem(unit, _itemDecay, EquipmentSlot.Weapon);
        AddItem(unit, _itemHood, EquipmentSlot.Head);
        AddItem(unit, _itemStaff, EquipmentSlot.Weapon);
        AddItem(unit, _itemPotion, EquipmentSlot.Weapon);
        AddItem(unit, _itemBook, EquipmentSlot.Weapon);


        unit.Stat = new Stat
        {
            HitPoints = 25,
            MaxHitPoints = 25,
            Movement = 5,
            Constitution = 4,
            Strength = 5,
            Magic = 11,
            Dexterity = 8,
            Speed = 7,
            Luck = 9,
            Defense = 3,
            Resistance = 5
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new Rogue();
        unit.Name = "Bob, Sneaky";
        unit.Class = "Rogue";
        unit.Level = 3;
        AddItem(unit, _itemLockpick);
        AddItem(unit, _itemDagger, EquipmentSlot.Weapon);
        AddItem(unit, _itemPants, EquipmentSlot.Legs);
        AddItem(unit, _itemShoes, EquipmentSlot.Feet);
        AddItem(unit, _itemPotion);
        unit.Abilities.Add(_abilitySteal);
        unit.Stat = new Stat
        {
            HitPoints = 26,
            MaxHitPoints = 26,
            Movement = 6,
            Constitution = 5,
            Strength = 8,
            Magic = 11,
            Dexterity = 8,
            Speed = 12,
            Luck = 12,
            Defense = 9,
            Resistance = 8
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new Cleric();
        unit.Name = "Alice";
        unit.Class = "Cleric";
        unit.Level = 4;
        AddItem(unit, _itemPotion);
        AddItem(unit, _itemPlate, EquipmentSlot.Chest);
        AddItem(unit, _itemGreaves, EquipmentSlot.Legs);
        AddItem(unit, _itemSmite, EquipmentSlot.Weapon);
        AddItem(unit, _itemMace);

        unit.Abilities.Add(_abilityHeal);
        unit.Stat = new Stat
        {
            HitPoints = 27,
            MaxHitPoints = 27,
            Movement = 5,
            Constitution = 4,
            Strength = 7,
            Magic = 9,
            Dexterity = 7,
            Speed = 7,
            Luck = 10,
            Defense = 6,
            Resistance = 7
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new Knight();
        unit.Name = "Reginald III, Sir";
        unit.Class = "Knight";
        unit.Level = 5;
        AddItem(unit, _itemPotion);
        AddItem(unit, _itemSword, EquipmentSlot.Weapon);
        AddItem(unit, _itemHelm, EquipmentSlot.Head);
        AddItem(unit, _itemPlate, EquipmentSlot.Chest);
        AddItem(unit, _itemGreaves, EquipmentSlot.Legs);
        AddItem(unit, _itemSabatons, EquipmentSlot.Feet);
        unit.Abilities.Add(_abilityTaunt);
        unit.Stat = new Stat
        {
            HitPoints = 30,
            MaxHitPoints = 30,
            Movement = 4,
            Constitution = 10,
            Strength = 10,
            Magic = 9,
            Dexterity = 7,
            Speed = 5,
            Luck = 10,
            Defense = 13,
            Resistance = 5
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new EnemyGhost();
        unit.Name = "Poltergeist";
        unit.Class = "Ghost";
        unit.Level = 1;
        AddItem(unit, _itemAxe, EquipmentSlot.Weapon);
        unit.Stat = new Stat
        {
            HitPoints = 25,
            MaxHitPoints = 25,
            Movement = 5,
            Constitution = 3,
            Strength = 8,
            Magic = 6,
            Dexterity = 7,
            Speed = 8,
            Luck = 8,
            Defense = 5,
            Resistance = 4
        };
        unit.Abilities.Add(_abilityFly);
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new EnemyGoblin();
        unit.Name = "Ruthless Treasure-Gather";
        unit.Class = "Goblin";
        unit.Level = 2;
        AddItem(unit, _itemSword, EquipmentSlot.Weapon);
        unit.Stat = new Stat
        {
            HitPoints = 28,
            MaxHitPoints = 28,
            Movement = 5,
            Constitution = 5,
            Strength = 9,
            Magic = 7,
            Dexterity = 7,
            Speed = 8,
            Luck = 9,
            Defense = 6,
            Resistance = 2
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new EnemyArcher();
        unit.Name = "Sniper";
        unit.Class = "Archer";
        unit.Level = 3;
        AddItem(unit, _itemBow, EquipmentSlot.Weapon);

        unit.Stat = new Stat
        {
            HitPoints = 27,
            MaxHitPoints = 27,
            Movement = 5,
            Constitution = 6,
            Strength = 9,
            Magic = 7,
            Dexterity = 7,
            Speed = 8,
            Luck = 9,
            Defense = 6,
            Resistance = 2
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new EnemyMage();
        unit.Name = "Studious Spellcaster";
        unit.Class = "Mage";
        unit.Level = 4;
 
        AddItem(unit, _itemLightning, EquipmentSlot.Weapon);
        AddItem(unit, _itemPotion);
        unit.Stat = new Stat
        {
            HitPoints = 26,
            MaxHitPoints = 26,
            Movement = 5,
            Constitution = 4,
            Strength = 6,
            Magic = 10,
            Dexterity = 8,
            Speed = 9,
            Luck = 9,
            Defense = 6,
            Resistance = 7
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);

        unit = new EnemyCleric();
        unit.Name = "Doctor of the Fallen";
        unit.Class = "Cleric";
        unit.Level = 5;
        AddItem(unit, _itemPotion);
        AddItem(unit, _itemMace);
        AddItem(unit, _itemPlate, EquipmentSlot.Chest);
        AddItem(unit, _itemSmite, EquipmentSlot.Weapon);
        unit.Abilities.Add(_abilityHeal);
        unit.Stat = new Stat
        {
            HitPoints = 29,
            MaxHitPoints = 29,
            Movement = 5,
            Constitution = 4,
            Strength = 8,
            Magic = 11,
            Dexterity = 8,
            Speed = 8,
            Luck = 8,
            Defense = 7,
            Resistance = 6
        };
        unit.CurrentRoom = GetRandomRoom();
        _db.Units.Add(unit);
        _db.Stats.Add(unit.Stat);
        foreach (UnitItem unitItem in unit.UnitItems)
            _db.UnitItems.Add(unitItem);
    }

    private void GenerateAbilities()
    {
        // Generates the abilities for the game.
        _db.Abilities.Add(_abilityFly);
        _db.Abilities.Add(_abilityHeal);
        _db.Abilities.Add(_abilitySteal);
        _db.Abilities.Add(_abilityTaunt);
    }

    private void GenerateDungeons()
    {
        // Generates the dungeons and rooms for the game.
        Dungeon dungeon = new Dungeon();
        dungeon.Name = "Intro Dungeon";
        dungeon.Description = "The first dungeon in the game";
        Room entrance = _roomFactory.CreateRoom("intro.entrance");
        Room jail = _roomFactory.CreateRoom("intro.jail");
        Room kitchen = _roomFactory.CreateRoom("intro.kitchen");
        Room hallway = _roomFactory.CreateRoom("intro.hallway");
        Room library = _roomFactory.CreateRoom("intro.entrance");
        Room dwelling = _roomFactory.CreateRoom("intro.dwelling");
        Room dwelling2 = _roomFactory.CreateRoom("intro.dwelling2");
        entrance.AddAdjacentRoom(jail, Direction.West);
        entrance.AddAdjacentRoom(kitchen, Direction.East);
        entrance.AddAdjacentRoom(hallway, Direction.North);
        hallway.AddAdjacentRoom(dwelling2, Direction.West);
        hallway.AddAdjacentRoom(library, Direction.East);
        hallway.AddAdjacentRoom(dwelling, Direction.North);
        _rooms.AddRange<Room>(entrance, jail, kitchen, hallway, library, dwelling, dwelling2);

        dungeon.StartingRoom = entrance;

        _db.Dungeons.Add(dungeon);

        foreach (Room room in _rooms)
        {
            _db.Rooms.Add(room);
        }
    }

    private Room GetRandomRoom()
    {
        // Returns a random room from the list of rooms.
        Random numberGenerator = new Random();
        int random = numberGenerator.Next(0, 7);
        return _rooms[random];
    }

    private void AddItem(Unit unit, Item item)
    {
        // Adds an item to the unit's inventory
        AddItem(unit, item, EquipmentSlot.None);
    }

    private void AddItem(Unit unit, Item item, EquipmentSlot slot)
    {
        // Adds an item to the unit's inventory or equipment slot. If the unit's inventory is null, it creates a new list.
        if (unit.UnitItems == null)
            unit.UnitItems = new();
        unit.UnitItems.Add(new()
        {
            Item = item,
            ItemId = item.ItemId,
            Unit = unit,
            UnitId = unit.UnitId,
            Slot = slot
        });
    }

    async private void DisplaySeedProgressBar()
    {
        // Displays a progress bar while the database is being seeded. The progress bar is displayed using the
        // Spectre.Console library.
        AnsiConsole.Progress()
        .AutoRefresh(true)
        .AutoClear(false)
        .HideCompleted(false)
        .Columns(new ProgressColumn[]
        {
            new TaskDescriptionColumn(),
            new ProgressBarColumn(),
            new PercentageColumn(),
            new RemainingTimeColumn(),
            new SpinnerColumn(),
            new DownloadedColumn(),
            new TransferSpeedColumn(),
        })
        .Start(ctx =>
        {
            double progress = 55;
            ProgressTask taskTotal = ctx.AddTask("[white][[Seeding Database]][/]", true, 21716);
            ProgressTask taskItems = ctx.AddTask("[white]Generating Items[/]", true, 8362);
            ProgressTask taskRooms = ctx.AddTask("[white]Generating Rooms[/]", true, 1091);
            ProgressTask taskDungeon = ctx.AddTask("[white]Generating Dungeon[/]", true, 850);
            ProgressTask taskAbilities = ctx.AddTask("[white]Generating Abilties[/]", true, 159);
            ProgressTask taskUnits = ctx.AddTask("[white]Generating Units[/]", true, 7643);
            ProgressTask taskStats = ctx.AddTask("[white]Generating Stats[/]", true, 2410);
            ProgressTask taskInventory = ctx.AddTask("[white]Generating Inventory[/]", true, 1201);

            while (!ctx.IsFinished)
            {
                Thread.Sleep(10);
                taskTotal.Increment(progress * 1.30);
                if (!taskItems.IsFinished)
                {
                    taskItems.Increment(progress);
                    taskRooms.Increment(progress / 2);
                    taskDungeon.Increment(progress / 4);
                }
                else if (!taskRooms.IsFinished)
                {
                    taskRooms.Increment(progress);
                    taskDungeon.Increment(progress / 2);
                    taskAbilities.Increment(progress / 4);
                }
                else if (!taskDungeon.IsFinished)
                {
                    taskDungeon.Increment(progress);
                    taskAbilities.Increment(progress/2);
                    taskUnits.Increment(progress/4);
                }
                else if (!taskAbilities.IsFinished)
                {
                    taskAbilities.Increment(progress);
                    taskUnits.Increment(progress/2);
                    taskStats.Increment(progress/4);
                }
                else if (!taskUnits.IsFinished)
                {
                    taskUnits.Increment(progress);
                    taskStats.Increment(progress/2);
                    taskInventory.Increment(progress/4);
                }
                else if (!taskStats.IsFinished)
                {
                    taskStats.Increment(progress);
                    taskInventory.Increment(progress/2);
                }
                else if (!taskInventory.IsFinished)
                {
                    taskInventory.Increment(progress);
                }
            }
        });
    }
}