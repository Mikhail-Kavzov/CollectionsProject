using System.ComponentModel.DataAnnotations;

namespace CollectionsProject.Models.CollectionModels
{
    public enum CollectionFieldType
    {
        [Display(Name = "int")]
        intField,
        [Display(Name = "string")]
        stringField,
        [Display(Name = "multiline")]
        multilineField,
        [Display(Name = "bool")]
        booleanField,
        [Display(Name = "date")]
        dateField,
    }
}
