# Celestial.UIToolkit
A custom WPF UI-Toolkit which is inspired by a lot of the current design languages, including Microsoft's Fluent Design and Google's Material Design.

[![Build Status](https://dev.azure.com/ManuelRoemer/Celestial%20UIToolkit/_apis/build/status/Celestial.UIToolkit)](https://dev.azure.com/ManuelRoemer/Celestial%20UIToolkit/_build/latest?definitionId=2)
[![NuGet - Celestial.UIToolkit.Core](https://img.shields.io/nuget/v/Celestial.UIToolkit.Core.svg?label=Celestial.UIToolkit.Core)](https://www.nuget.org/packages/Celestial.UIToolkit.Core/)
[![NuGet - Celestial.UIToolkit](https://img.shields.io/nuget/v/Celestial.UIToolkit.svg?label=Celestial.UIToolkit)](https://www.nuget.org/packages/Celestial.UIToolkit/)


## Table of Contents



## 1. Getting Started
### 1.1 What is this project?
There are a lot of amazing WPF UI Frameworks, like *MahApps.Metro* or *MaterialDesignInXaml*.
None of these fulfilled all requirements for an upcoming project of mine though - I was looking
for an easy to use UI toolkit with a modern look and modern controls - at best, similar to the UWP.

Thus, this project was born. It started out as a library of custom control styles, but quickly
developed certain features that are also useable (and hopefully useful!) to other libraries.

### 1.2 Installation
The libraries can be installed via NuGet (preferred way), or via
[GitHub Releases](https://github.com/manuelroemer/Celestial.UIToolkit/releases/latest).

**Install the whole toolkit:**
```
Install-Package Celestial.UIToolkit
```

**Install only the Core library:**
```
Install-Package Celestial.UIToolkit.Core
```

**Install the Control Gallery:**

The *ControlGallery* application (the toolkit's interactive companion) can be downloaded on 
[GitHub Releases](https://github.com/manuelroemer/Celestial.UIToolkit/releases/latest).


### 1.3 Requirements
| Component                    | Minimum .NET Framework Requirement(s) |
| ---------------------------- | ------------------------------------- |
| `Celestial.UIToolkit.Core`   | .NET 4.5                              |
| `Celestial.UIToolkit`        | .NET 4.5                              |
| `ControlGallery`             | .NET 4.6                              |
| Solution (for custom builds) | .NET 4.6                              |


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
