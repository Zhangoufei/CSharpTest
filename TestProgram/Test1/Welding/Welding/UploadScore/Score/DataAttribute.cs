using System;

[AttributeUsage(AttributeTargets.Property)]
public class DataAttribute : Attribute
{
    private FieldType m_oFieldType;

    public DataAttribute(FieldType fieldType)
    {
        this.m_oFieldType = fieldType;
    }

    public FieldType Type
    {
        get
        {
            return this.m_oFieldType;
        }
        set
        {
            this.m_oFieldType = value;
        }
    }
}

