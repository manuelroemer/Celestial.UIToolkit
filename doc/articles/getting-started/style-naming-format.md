
## Style Naming Format
The styles in the toolkit follow the following naming convention:

```
StyleName := [SizeModifier]<ColorTheme>[Specialization]<ControlName>

where:

SizeModifier   := "Large" | "Compact"
ColorTheme     := "Standard" | "Accent1" | "Accent2"
Specialization := "[Any possible string. Up to the Style Author.]"
ControlName    := "[The .NET type name of the Control.]"
```

There are some exceptions to the `ControlName` rule.
Sometimes, a single style is used for multiple Controls.
This can happen when the controls have a shared base class from which they inherit, 
e.g. `Button` and `ToggleButton`.

In this case, the styles have a generalized control name.
For buttons, the styles are called `...Button`.

### Examples

**The simplest Style names:**

```
StandardTextBox
Accent1ComboBox
Accent2Button

== <ColorTheme><ControlName>
```


**Specialized Style names:**

```
StandardFlatButton
Accent1CircleButton
Accent1OutlinedComboBox
Accent2OutlinedTextBox

== <ColorTheme>[Specialization]<ControlName>
```


**Size variations:**

```
LargeStandardListBox
CompactAccent1ListView
LargeAccent2ListView

== [SizeModifier]<ColorTheme><ControlName>
```


**All together:**
*(Note: This style doesn't exist - it is just an example.)*

```
CompactAccent1SpecializedButton

== [SizeModifier]<ColorTheme>[Specialization]<ControlName>
```
