# unity-editor-ex
Extension for Unity editor system

# install
Use this repository directly in Unity.

# usage

### Editor & Property Drawer
* `ExtendedDrawer` with extension methods for simple area types like folding / tabs
* `ExtendedEditor` with extension methods for simple area types like folding / tabs

### Attributes
* `ReadOnly` to make field read only
* `LayerMask` to mark integer field as layer
* `AssetChooser` to mark field as asset reference to search in database
* `Scene` to mark string field as scene reference

### Extensions
* Helper methods to find multiple properties for each array element `FindProperties` / `FindPropertiesRelative`
* `PlayerSettingsEx` to handle easier:
  * Check scripting symbol is defined
  * Add or delete scripting symbols
