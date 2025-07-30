# Tekla API Extensions

A collection of extension methods for Tekla Structures Open API to simplify common operations and improve code readability.

## Features

- Extension methods for working with geometry:
  - [`PointExtensions`](src/TeklaAPIExtensions.Model/PointExtensions.cs): Transform points, calculate averages, remove duplicates
  - [`LineSegmentExtensions`](src/TeklaAPIExtensions.Model/LineSegmentExtensions.cs): Cut line segments by planes and bounding boxes
  - [`SolidExtensions`](src/TeklaAPIExtensions.Model/SolidExtensions.cs): Get faces, calculate bounding boxes
  - [`FaceExtensions`](src/TeklaAPIExtensions.Model/FaceExtensions.cs): Extract loops and vertices
- Model object extensions:
  - [`ModelObjectExtensions`](src/TeklaAPIExtensions.Model/ModelObjectExtensions.cs): Easy access to properties and UDAs
  - [`ModelObjectEnumeratorExtensions`](src/TeklaAPIExtensions.Model/ModelObjectEnumeratorExtensions.cs): LINQ-style filtering
- Drawing extensions:
  - [`DrawingObjectEnumeratorExtensions`](src/TeklaAPIExtensions.Drawing/DrawingObjectEnumeratorExtensions.cs): LINQ-style filtering
  - [`PointListExtensions`](src/TeklaAPIExtensions.Drawing/PointListExtensions.cs): Convert between point collections

## Packages

- TeklaAPIExtensions.Model - Extension methods to types in Tekla.Structures.Model assembly
- TeklaAPIExtensions.Drawing - Extension methods to types in Tekla.Structures.Drawing assembly

## Installation

.NET CLI:

```sh
dotnet add package TeklaAPIExtensions.Model
dotnet add package TeklaAPIExtensions.Drawing
```

Project reference:
```xml
<ItemGroup>
  <PackageReference Include="TeklaAPIExtensions.Model" Version="0.0.9" />
  <PackageReference Include="TeklaAPIExtensions.Drawing" Version="0.0.6" />
</ItemGroup>
```

The packages do not include Tekla.Structures assemblies. They should be added separately in the project. Both packages support net48 and netstandard2.0. Both packages were build against corresponding versions of Tekla.Structures assemblies (both Tekla.Structures.Model and Tekla.Structures.Drawing)

|Target framework | Tekla assembly version |
|---|---|
|net48 | 2022.0.10715 |
|netstandard2.0 | 2024.0.4 |

## Usage examples

<code>OfType\<T></code> extension method is added to common enumerators to convert them into <code>IEnumerable\<T></code>

❌ Old way to retrieve beams using <code>ModelObjectEnumerator</code>:

```c#
using Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

var enumerator = new TSMUI.ModelObjectSelector().GetSelectedObjects();
var beams = new List<Beam>();
while (enumerator.MoveNext())
{
    var modelObject = enumerator.Current as Beam;
    if (modelObject != null)
    {
        beams.Add(modelObject);
    }
}
```

✅ With TeklaAPIExtensions.Model becomes

```c#
using Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;
using TeklaAPIExtensions.Model;

var beams = new TSMUI.ModelObjectSelector()
    .GetSelectedObjects()
    .OfType<Beam>()
    .ToList();
```

"Try style" methods to retrieve UDA, Properties and Dynamic string properties with out parameters

```c#
ModelObject anyModelObject;

// ❌ Retrieve string UDA with default API
string value = "";
if (!anyModelObject.GetUserProperty("MY_ATTRIBUTE", ref value))
{
    Console.WriteLine("UDA not found");
}

// ✅ Retrieve string UDA with TeklaAPIExtensions.Model
if (!anyModelObject.TryGetUDA("MY_ATTRIBUTE", out string value))
{
    Console.WriteLine("UDA not found");
}

// ✅ Double UDA
if (!anyModelObject.TryGetUDA("MY_ATTRIBUTE", out double value))
{
    Console.WriteLine("UDA not found");
}

// ✅ Int UDA
if (!anyModelObject.TryGetUDA("MY_ATTRIBUTE", out int value))
{
    Console.WriteLine("UDA not found");
}

// ✅ String property
if (!anyModelObject.TryGetProperty("PROFILE", out string value))
{
    Console.WriteLine("PROFILE not found");
}

// ✅ Double property
if (!anyModelObject.TryGetProperty("WEIGHT", out double value))
{
    Console.WriteLine("WEIGHT not found");
}

// ✅ Int property
if (!anyModelObject.TryGetProperty("CLASS_ATTR", out int value))
{
    Console.WriteLine("CLASS_ATTR not found");
}

// ✅ Dynamic string
if (!anyModelObject.TryGetDynamicStringProperty("MY_STRING", out string value))
{
    Console.WriteLine("MY_STRING not found");
}
```

Geometry manipulations

```c#
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;
using TeklaAPIExtensions.Model;

// Transform points
List<Point> points = [new(0, 0, 0), new(1000, 1000, 1000)];
CoordinateSystem coordinateSystem = new(new Point(100, 100, 100), new Vector(1, 0, 0), new Vector(0, 1, 0));
var transformedPoints = points.Transform(coordinateSystem);

// Get visible faces
Solid solid = beams.First().GetSolid();
Vector viewDirection = new(0, 0, 1);
List<Face> visibleFaces = solid.GetVisibleFaces(viewDirection).ToList();
```

And much more...

## Requirements

- .NET Standard 2.0 or .NET Framework 4.8
- Tekla Structures 2022 or newer
- Valid Tekla Open API license
  
## Licence

MIT License. See [`LICENSE`](LICENSE) file for details.

Note: This is an independent library and is not endorsed by or affiliated with Trimble Solutions Corporation. Tekla Structures and Tekla Open API are trademarks of Trimble Solutions Corporation.
