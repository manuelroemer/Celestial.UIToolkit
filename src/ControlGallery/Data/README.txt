To add a new Category:
---------------------------------------

Add a "KnownCategories.[NAME].cs" file to the Categories folder.
In there, implement the following template/structure:

public static partial class KnownCategories
{

    public static ControlCategory NAME { get; } = new ControlCategory(
        "NAME",
        new PackIconMaterialLight() { Kind = PackIconMaterialLightKind.ICON },
        CONTROLS);

}

Then, head to ControlDataSource.cs and yield return the category in the GetCategories method.