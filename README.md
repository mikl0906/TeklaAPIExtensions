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

## Installation

.NET CLI:

```sh
dotnet add package TeklaAPIExtensions.Model
dotnet add package TeklaAPIExtensions.Drawing
```

Project reference:
```xml
<ItemGroup>
  <PackageReference Include="TeklaAPIExtensions.Model" Version="0.0.2" />
  <PackageReference Include="TeklaAPIExtensions.Drawing" Version="0.0.1" />
</ItemGroup>
```

## Usage

```c#
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;
using TeklaAPIExtensions.Model;
using TeklaAPIExtensions.Drawing;

// Filter model objects by type
var selectedObjects = new Tekla.Structures.Model.UI.ModelObjectSelector().GetSelectedObjects();
var beams = selectedObjects.OfType<Beam>();

// Get property values safely
var modelObject = beams.First();
if (modelObject.TryGetProperty("PROFILE", out string profile))
{
    // Use profile value
}

// Transform points
List<Point> points = [new(0, 0, 0), new(1000, 1000, 1000)];
CoordinateSystem coordinateSystem = new(new Point(100, 100, 100), new Vector(1, 0, 0), new Vector(0, 1, 0));
var transformedPoints = points.Transform(coordinateSystem);

// Get visible faces
Solid solid = beams.First().GetSolid();
Vector viewDirection = new(0, 0, 1);
List<Face> visibleFaces = solid.GetVisibleFaces(viewDirection).ToList();
```

## Requirements

- .NET Standard 2.0
- Tekla Structures 2024 or newer
- Valid Tekla Open API license
  
## Licence

MIT License. See [`LICENSE`](LICENSE) file for details.

Note: This is an independent library and is not endorsed by or affiliated with Trimble Solutions Corporation. Tekla Structures and Tekla Open API are trademarks of Trimble Solutions Corporation.

