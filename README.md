# unity-editor-ex
Extension for Unity editor system

# install
Use this repository directly in Unity.

# usage

### Editor & Property Drawer
* `ExtendedDrawer` with extension methods for simple area types like folding / tabs
* `ExtendedEditor` with extension methods for simple area types like folding / tabs
* `AutoEditor` with easy attribute usages for serialzed properties:
  * `SerializedPropertyReference` to setup reference in target object
  * `SerializedPropertyDefaultPresentation` to present property in default editor way
  * `SerializedPropertyIdentifiedObjectPresentation` to present property as `IIdentifiedObject` (must be an array of `SerializedProperty`)
  * `SerializedPropertyLabeledGroup` to show propety under this group
  * `SerializedPropertyFoldGroup` to show property under this fold group
  * `SerializedPropertyTabGroup` to show property under this tab
  
### Sepcific Reorderable Tables
* `TableReorderableTable` to show an table
  * Use `Columns.Add` to add:
    * `FixedColumn` - a column with fixed size
    * `FlexibleColumn` - a column with percentage size or -1 for empty space
  * Do not override `DrawHeaderCallback` or `DrawElementCallback`

### Attributes
* `Hide` to hide a field (conditional, see properties) 
* `ReadOnly` to make field read only (conditional, see properties)
* `LayerMask` to mark integer field as layer
* `AssetChooser` to mark field as asset reference to search in database
* `Scene` to mark string field as scene reference
* `Tag` to mark a field as tag field (combo box)
* `SortingLayer` to mark field as sorting layer choice (combo box)

### Extensions
* Helper methods to find multiple properties for each array element `FindProperties` / `FindPropertiesRelative`
* Helper methods for serialized properties as extension methods
* `PlayerSettingsEx` to handle easier:
  * Check scripting symbol is defined
  * Add or delete scripting symbols
