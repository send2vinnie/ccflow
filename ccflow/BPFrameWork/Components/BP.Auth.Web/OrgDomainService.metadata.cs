
namespace BP.Auth.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // The MetadataTypeAttribute identifies Port_DeptMetadata as the class
    // that carries additional metadata for the Port_Dept class.
    [MetadataTypeAttribute(typeof(Port_Dept.Port_DeptMetadata))]
    public partial class Port_Dept
    {

        // This class allows you to attach custom attributes to properties
        // of the Port_Dept class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class Port_DeptMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private Port_DeptMetadata()
            {
            }

            public string Name { get; set; }

            public string No { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies Port_EmpMetadata as the class
    // that carries additional metadata for the Port_Emp class.
    [MetadataTypeAttribute(typeof(Port_Emp.Port_EmpMetadata))]
    public partial class Port_Emp
    {

        // This class allows you to attach custom attributes to properties
        // of the Port_Emp class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class Port_EmpMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private Port_EmpMetadata()
            {
            }

            public string FK_Dept { get; set; }

            public string Name { get; set; }

            public string No { get; set; }

            public string Pass { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies Port_EmpDeptMetadata as the class
    // that carries additional metadata for the Port_EmpDept class.
    [MetadataTypeAttribute(typeof(Port_EmpDept.Port_EmpDeptMetadata))]
    public partial class Port_EmpDept
    {

        // This class allows you to attach custom attributes to properties
        // of the Port_EmpDept class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class Port_EmpDeptMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private Port_EmpDeptMetadata()
            {
            }

            public string FK_Dept { get; set; }

            public string FK_Emp { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies Port_EmpStationMetadata as the class
    // that carries additional metadata for the Port_EmpStation class.
    [MetadataTypeAttribute(typeof(Port_EmpStation.Port_EmpStationMetadata))]
    public partial class Port_EmpStation
    {

        // This class allows you to attach custom attributes to properties
        // of the Port_EmpStation class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class Port_EmpStationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private Port_EmpStationMetadata()
            {
            }

            public string FK_Emp { get; set; }

            public string FK_Station { get; set; }
        }
    }

    // The MetadataTypeAttribute identifies Port_StationMetadata as the class
    // that carries additional metadata for the Port_Station class.
    [MetadataTypeAttribute(typeof(Port_Station.Port_StationMetadata))]
    public partial class Port_Station
    {

        // This class allows you to attach custom attributes to properties
        // of the Port_Station class.
        //
        // For example, the following marks the Xyz property as a
        // required property and specifies the format for valid values:
        //    [Required]
        //    [RegularExpression("[A-Z][A-Za-z0-9]*")]
        //    [StringLength(32)]
        //    public string Xyz { get; set; }
        internal sealed class Port_StationMetadata
        {

            // Metadata classes are not meant to be instantiated.
            private Port_StationMetadata()
            {
            }

            public string Name { get; set; }

            public string No { get; set; }

            public Nullable<int> StaGrade { get; set; }
        }
    }
}
