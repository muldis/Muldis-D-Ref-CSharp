using System;
using System.Collections.Generic;
using System.Numerics;

[assembly: CLSCompliant(true)]

namespace Muldis.DBP
{
    public interface IInfo
    {
        Boolean Provides_Dot_Net_Muldis_DBP();

        IMachine Want_VM_API(Object requested_version);
    }

    public interface IMachine
    {
        IImporter Importer();
    }

    public interface IImporter
    {
        IMachine Machine();

        IMD_Any MD_Any(Object value);

        IMD_Boolean MD_Boolean(Boolean value);

        IMD_Integer MD_Integer(BigInteger value);

        IMD_Integer MD_Integer(Int32 value);

        IMD_Tuple MD_Tuple(
            Object a0 = null, Object a1 = null, Object a2 = null,
            Nullable<KeyValuePair<String,Object>> attr = null,
            Dictionary<String,Object> attrs = null);

        IMD_Capsule MD_Capsule(IMD_Any label, IMD_Tuple attrs);

        IMD_Capsule MD_Capsule(String label, IMD_Tuple attrs);

        IMD_Capsule MD_Capsule(String[] label, IMD_Tuple attrs);

        IMD_Excuse MD_Excuse(IMD_Tuple attrs);

        IMD_Excuse MD_Excuse(String value);

        IMD_Excuse_No_Reason MD_Excuse_No_Reason();
    }

    public interface IMD_Any
    {
        IMachine Machine();
    }

    public interface IMD_Boolean : IMD_Any
    {
        Boolean Export_Boolean();
    }

    public interface IMD_Integer : IMD_Any
    {
        BigInteger Export_BigInteger();

        Int32 Export_Int32();
    }

    public interface IMD_Tuple : IMD_Any
    {
    }

    public interface IMD_Capsule : IMD_Any
    {
    }

    public interface IMD_Excuse : IMD_Capsule
    {
    }

    public interface IMD_Excuse_No_Reason : IMD_Excuse
    {
    }
}
