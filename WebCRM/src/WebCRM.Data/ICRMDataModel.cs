namespace WebCRM.Data
{
    /// <summary>
    /// Interface to ensure base functionality of models
    /// </summary>
    /// <author>Daniel Lee Graf</author>
    public interface ICRMDataModel<DataModel>
    {
        int Id { get; set; }

        void RestrictedModelUpdate(DataModel model);
    }
}