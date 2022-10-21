# unity-editor-ex
Extension for Unity editor system

# install
Use this repository directly in Unity.

# usage

### Editor & Property Drawer
* `ExtendedDrawer` with extension methods for simple area types like folding / tabs
* `ExtendedEditor` with extension methods for simple area types like folding / tabs

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
