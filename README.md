# Celestial.UIToolkit
A custom WPF toolkit which is inspired by a lot of the current design languages, including 
Microsoft's Fluent Design and Google's Material Design.

[![Build Status](https://dev.azure.com/ManuelRoemer/Celestial%20UIToolkit/_apis/build/status/Celestial.UIToolkit)](https://dev.azure.com/ManuelRoemer/Celestial%20UIToolkit/_build/latest?definitionId=2)
[![NuGet - Celestial.UIToolkit.Core](https://img.shields.io/nuget/v/Celestial.UIToolkit.Core.svg?label=Celestial.UIToolkit.Core)](https://www.nuget.org/packages/Celestial.UIToolkit.Core/)
[![NuGet - Celestial.UIToolkit](https://img.shields.io/nuget/v/Celestial.UIToolkit.svg?label=Celestial.UIToolkit)](https://www.nuget.org/packages/Celestial.UIToolkit/)

![Example Image](./.github/images/MainExample.png)

## Table of Contents
* [1. Getting started](#1-getting-started)
  * [1.1 What is Celestial.UIToolkit?](#11-what-is-celestialuitoolkit)
  * [1.2 Installation and Requirements](#12-installation)
  * [1.3 Minimum System Requirements](#13-minimum-system-requirements)
  * [1.4 Which DLL do I need?](#14-which-dll-do-i-need)
  * [1.5 The Control Gallery](#15-the-control-gallery)
* [2. Documentation](#2-documentation)
* [3. Contributing](#3-contributing)

## 1. Getting Started
### 1.1 What is Celestial.UIToolkit?
There are amazing UI Frameworks for WPF out there, like *MahApps.Metro* or *MaterialDesignInXaml*.
None of these fulfilled all requirements for an upcoming project of mine though.

Thus, this project was born. It started out as a library of custom styles, but quickly
developed certain features that are also useable by other libraries or projects.

It provides a vast set of utilities for the day-to-day WPF development, custom animations (for 
instance `BrushAnimation` classes), a set of commonly used converters, a Behavior API, a lot of
modern controls, custom and consistent styles for all types of controls and lots of other features.

### 1.2 Installation
The libraries can be installed via NuGet (preferred way), or via
[GitHub Releases](https://github.com/manuelroemer/Celestial.UIToolkit/releases/latest).

*Unsure about what to install? See [Section 1.4](#14-which-dll-do-i-need) for details.*

*Done with installing, but unsure about how to use the toolkit? Head over to the 
[Guides](https://manuelroemer.github.io/Celestial.UIToolkit/doc/articles/introduction.html) 
for a tutorial*.

#### Install the whole toolkit:
```
Install-Package Celestial.UIToolkit
```

#### Install only the Core library:
```
Install-Package Celestial.UIToolkit.Core
```

#### Install the Control Gallery:

The [Control Gallery](#15-the-control-gallery)
application (the toolkit's interactive companion) can be downloaded on 
[GitHub Releases](https://github.com/manuelroemer/Celestial.UIToolkit/releases/latest).

### 1.3 Minimum System Requirements

| Component                    | Minimum .NET Framework Requirement(s) |
| ---------------------------- | ------------------------------------- |
| `Celestial.UIToolkit.Core`   | .NET 4.5                              |
| `Celestial.UIToolkit`        | .NET 4.5                              |
| Control Gallery              | .NET 4.6                              |
| Solution (for custom builds) | .NET 4.6                              |

### 1.4 Which DLL do I need?
In most cases, you will want to install the `Celestial.UIToolkit` library, since it provides every single feature of the toolkit. 
If you don't require any styles or custom controls for your project, you only need the `Celestial.UIToolkit.Core` library.

See the following image for a basic overview of the features:

![DLL Overview](./.github/Images/DllArchitecture.png)

### 1.5 The Control Gallery
The Control Gallery is a demo application built upon the toolkit. It displays most of the available
controls and styles, which allows you to *directly interact* with them.

In addition, thanks to the amazing [ShowMeTheXAML](https://github.com/Keboo/ShowMeTheXAML) project,
it shows you the exact XAML code which is required to recreate an individual example.
Thus, the Control Gallery is also a living piece of **documentation** that you should use for
discovering the features of the toolkit.

You can either [download](#install-the-control-gallery) the application directly, or you can
clone this repo and build it yourself.

Here are some screenshots of the application:

![Control Gallery Screenshot 1](./.github/Images/ControlGalleryExample1.png)
![Control Gallery Screenshot 2](./.github/Images/ControlGalleryExample2.png)
![Control Gallery Screenshot 3](./.github/Images/ControlGalleryExample3.png)


## 2. Documentation
There are three sources of documentation available:

| Documentation type | Description |
| --- | --- |
| [ControlGallery](https://github.com/manuelroemer/Celestial.UIToolkit/releases/latest) | The ControlGallery application is your information source for controls and examples on how to use them. [More info on what the ControlGallery provides.](#15-the-control-gallery) |
| [Guides](https://manuelroemer.github.io/Celestial.UIToolkit/doc/articles/introduction.html) | There are several guides available, ranging from topics on getting started, up to more specific concepts and details of the toolkit. |
| [API Reference](https://manuelroemer.github.io/Celestial.UIToolkit/doc/api/Celestial.UIToolkit.html) | A list of all current API members, like MSDN. |