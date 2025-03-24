using TSM = Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace TeklaAPIExtensions.Model;

/// <summary>
/// Interface for interacting with a Tekla model.
/// </summary>
public interface ITeklaModelService
{
    /// <summary>
    /// Gets or sets the Tekla Structures model.
    /// </summary>
    TSM.Model Model { get; set; }

    /// <summary>
    /// Gets or sets the model object selector for the Tekla Structures model.
    /// </summary>
    TSMUI.ModelObjectSelector ModelObjectSelector { get; set; }

    /// <summary>
    /// Commits the changes made to the model.
    /// </summary>
    void CommitChanges();

    /// <summary>
    /// Gets the connection status of the model.
    /// </summary>
    /// <returns>True if the model is connected; otherwise, false.</returns>
    bool GetConnectionStatus();

    /// <summary>
    /// Gets the name of the model.
    /// </summary>
    /// <returns>The name of the model.</returns>
    string GetModelName();

    /// <summary>
    /// Gets the path of the model.
    /// </summary>
    /// <returns>The path of the model.</returns>
    string GetModelPath();

    /// <summary>
    /// Gets all currently selected objects from the Tekla Structures model that match the specified type.
    /// </summary>
    /// <typeparam name="T">The type of model objects to retrieve. Must be derived from TSM.ModelObject.</typeparam>
    /// <returns>An enumerable collection of selected model objects of type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// If no objects of the specified type are selected in the model, an empty collection will be returned.
    /// </remarks>
    IEnumerable<T> GetSelectedObjects<T>() where T : TSM.ModelObject;
}

/// <summary>
/// Provides services to interact with a Tekla Structures model.
/// </summary>
public class TeklaModelService : ITeklaModelService
{
    /// <summary>
    /// Gets or sets the Tekla Structures model.
    /// </summary>
    public TSM.Model Model { get; set; } = new();

    /// <summary>
    /// Gets or sets the model object selector for the Tekla Structures model.
    /// </summary>
    public TSMUI.ModelObjectSelector ModelObjectSelector { get; set; } = new();

    /// <summary>
    /// Commits any changes made to the Tekla Structures model.
    /// </summary>
    public void CommitChanges()
    {
        if (Model.GetConnectionStatus())
        {
            Model.CommitChanges();
        }
    }

    /// <summary>
    /// Gets the connection status of the Tekla Structures model.
    /// </summary>
    /// <returns>True if the model is connected; otherwise, false.</returns>
    public bool GetConnectionStatus()
    {
        return Model.GetConnectionStatus();
    }

    /// <summary>
    /// Gets the name of the Tekla Structures model.
    /// </summary>
    /// <returns>The model name if connected; otherwise, an empty string.</returns>
    public string GetModelName()
    {
        return Model.GetConnectionStatus() ? Model.GetInfo().ModelName : string.Empty;
    }

    /// <summary>
    /// Gets the path of the Tekla Structures model.
    /// </summary>
    /// <returns>The model path if connected; otherwise, an empty string.</returns>
    public string GetModelPath()
    {
        return Model.GetConnectionStatus() ? Model.GetInfo().ModelPath : string.Empty;
    }

    /// <summary>
    /// Gets all currently selected objects from the Tekla Structures model that match the specified type.
    /// </summary>
    /// <typeparam name="T">The type of model objects to retrieve. Must be derived from TSM.ModelObject.</typeparam>
    /// <returns>An enumerable collection of selected model objects of type <typeparamref name="T"/>.</returns>
    /// <remarks>
    /// If no objects of the specified type are selected in the model, an empty collection will be returned.
    /// </remarks>
    public IEnumerable<T> GetSelectedObjects<T>() where T : TSM.ModelObject
    {
        var selectedObjects = ModelObjectSelector.GetSelectedObjects();

        return selectedObjects.OfType<T>();
    }
}