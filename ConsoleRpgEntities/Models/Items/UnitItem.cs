using ConsoleRpgEntities.DataTypes;
using ConsoleRpgEntities.Models.Items;
using ConsoleRpgEntities.Models.Units.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

[PrimaryKey("UnitId", "ItemId")]
public class UnitItem
{
    // UnitItem is a class that holds the properties of an item that is owned by a unit. It is used to store the
    // properties of an item that is owned by a unit.

    [ForeignKey("Unit")]
    public int UnitId { get; set; }
    public virtual Unit Unit { get; set; }

    [ForeignKey("Item")]
    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
    public virtual EquipmentSlot Slot { get; set; } = EquipmentSlot.None;
    public virtual int Quantity { get; set; }
}